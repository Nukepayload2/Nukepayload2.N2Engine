Imports System.Runtime.InteropServices
Imports System.Threading
Imports DirectShowLib
Imports Nukepayload2.N2Engine.Media

Friend Class MusicPlayerImpl
    Dim mainThreadContext As System.Windows.Threading.DispatcherSynchronizationContext
    Dim gameResourceLoadContext As SynchronizationContext

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
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

    Dim filterGraph As FilterGraph
    Dim baseFilter As IBaseFilter

    Dim vmrCfg As IVMRFilterConfig
    Dim graphBuilder As IGraphBuilder
    Dim mediaCtrl As IMediaControl
    Dim mediaEvent As IMediaEvent

    Dim lastEventCode As EventCode
    Dim isPlaying As New AsyncLocal(Of Boolean)

    Public Sub Play() Implements IMusicPlayer.Play
        If Not isPlaying.Value Then
            If graphBuilder Is Nothing Then
                Throw New InvalidOperationException("播放列表尚未初始化。每次播放之前都要设置播放列表。")
            End If
            If mediaEvent Is Nothing Then
                mediaEvent = DirectCast(filterGraph, IMediaEvent)
            End If
            If mediaCtrl Is Nothing Then
                mediaCtrl = DirectCast(filterGraph, IMediaControl)
            End If
            Dim retv = mediaCtrl.Run()
            If retv <> 1 Then
                Throw New COMException("背景音乐文件播放失败。")
            End If
            isPlaying.Value = True
            Task.Run(Sub()
                         mediaEvent.WaitForCompletion(10000000, lastEventCode)
                         Me.Stop()
                         mainThreadContext.Post(
                         Sub(obj)
                             RaiseEvent SingleSongComplete(Me, EventArgs.Empty)
                         End Sub, Nothing)
                     End Sub)
        End If
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        mediaCtrl?.Stop()
        mediaCtrl = Nothing
        graphBuilder = Nothing
        mediaEvent = Nothing
        baseFilter = Nothing
        vmrCfg = Nothing
        filterGraph = Nothing
        isPlaying.Value = False
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Dim mtid = Thread.CurrentThread.ManagedThreadId
        SynchronizationContext.SetSynchronizationContext(gameResourceLoadContext)
        If isPlaying.Value Then
            Me.Stop()
        End If
        Dim musicUri = Sources(value)
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(musicUri)
        Dim absolutePath = CurDir() + cur.AbsolutePath.Replace("/"c, "\"c)
        If graphBuilder Is Nothing Then
            If filterGraph Is Nothing Then
                filterGraph = New FilterGraph
            End If
            graphBuilder = DirectCast(filterGraph, IGraphBuilder)
            If baseFilter Is Nothing Then
                baseFilter = DirectCast(New VideoMixingRenderer, IBaseFilter)
            End If
            If vmrCfg Is Nothing Then
                vmrCfg = DirectCast(baseFilter, IVMRFilterConfig)
            End If
            graphBuilder.AddFilter(baseFilter, "Base filter for N2Engine")
        End If
        vmrCfg.SetRenderingMode(VMRMode.Windowless)
        graphBuilder.RenderFile(absolutePath, value.ToString)
        Await Task.Delay(0)
        Volume = volumeOverride
    End Function

End Class
