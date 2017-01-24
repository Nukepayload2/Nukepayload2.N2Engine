Imports System.Threading
Imports System.Windows.Forms
Imports DirectShowLib
Imports Nukepayload2.N2Engine.Media

Friend Class MusicPlayerImpl
    Dim mainThreadContext As System.Windows.Threading.DispatcherSynchronizationContext
    Dim gameResourceLoadContext As SynchronizationContext

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        graphBuilder.AddFilter(baseFilter, "Base filter for N2Engine")
        mainThreadContext = New System.Windows.Threading.DispatcherSynchronizationContext
        gameResourceLoadContext = SynchronizationContext.Current
        Await Task.Delay(0)
    End Function

    Dim volumeOverride As Double = 1
    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Dim audio = DirectCast(filterGraph, IBasicAudio)
            Dim vol = 0
            audio.get_Volume(vol)
            Return vol / 10000 + 1
        End Get
        Set(value As Double)
            Dim audio = DirectCast(filterGraph, IBasicAudio)
            audio.put_Volume((value - 1) * 10000)
            volumeOverride = value
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        mediaCtrl.Pause()
    End Sub

    Dim filterGraph As New FilterGraph
    Dim baseFilter As IBaseFilter = DirectCast(New VideoMixingRenderer, IBaseFilter)
    Dim vmrCfg As IVMRFilterConfig = DirectCast(baseFilter, IVMRFilterConfig)
    Dim graphBuilder As IGraphBuilder = DirectCast(filterGraph, IGraphBuilder)

    Dim mediaCtrl As IMediaControl = DirectCast(filterGraph, IMediaControl)
    Dim mediaEvent As IMediaEvent = DirectCast(filterGraph, IMediaEvent)
    Dim mediaWindow As IVideoWindow = DirectCast(filterGraph, IVideoWindow)

    Dim lastEventCode As EventCode
    Dim isPlaying As New AsyncLocal(Of Boolean)

    Public Sub Play() Implements IMusicPlayer.Play
        If Not isPlaying.Value Then
            mediaCtrl.Run()
            isPlaying.Value = True
            Task.Run(Sub()
                         mediaEvent.WaitForCompletion(10000000, lastEventCode)
                         Me.Stop()
                         mainThreadContext.Send(
                         Sub(obj)
                             RaiseEvent SingleSongComplete(Me, EventArgs.Empty)
                         End Sub, Nothing)
                     End Sub)
        End If
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        mediaCtrl.Stop()
        isPlaying.Value = False
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Dim mtid = Thread.CurrentThread.ManagedThreadId
        SynchronizationContext.SetSynchronizationContext(gameResourceLoadContext)
        If isPlaying.Value Then
            Me.Stop()
            mediaCtrl.StopWhenReady()
        End If
        Dim musicUri = Sources(value)
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(musicUri)
        Dim absolutePath = CurDir() + cur.AbsolutePath.Replace("/"c, "\"c)
        vmrCfg.SetRenderingMode(VMRMode.Windowless)
        Await Task.Run(Sub()
                           mediaCtrl.RenderFile(absolutePath)
                       End Sub)
        Volume = volumeOverride
    End Function

End Class
