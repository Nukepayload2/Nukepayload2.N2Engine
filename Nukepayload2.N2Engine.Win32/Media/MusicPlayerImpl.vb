Imports System.Windows.Threading
Imports Nukepayload2.N2Engine.Media

Friend Class MusicPlayerImpl

    WithEvents StateTracker As New DispatcherTimer With {.Interval = TimeSpan.FromMilliseconds(100), .IsEnabled = False}

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        Dim ipin = DirectCast(MediaCtrl, DirectShowLib.IPin)
        ipin.EndOfStream()
        Await Task.Delay(0)
    End Function

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Dim audio = DirectCast(filterGraph, DirectShowLib.IBasicAudio)
            Dim vol = 0
            audio.get_Volume(vol)
            Return vol / 10000 + 1
        End Get
        Set(value As Double)
            Dim audio = DirectCast(filterGraph, DirectShowLib.IBasicAudio)
            audio.put_Volume((value - 1) * 10000)
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        MediaCtrl.Pause()
        StateTracker.Stop()
    End Sub

    Dim filterGraph As New DirectShowLib.FilterGraph
    Dim MediaCtrl As DirectShowLib.IMediaControl = DirectCast(filterGraph, DirectShowLib.IMediaControl)

    Public Sub Play() Implements IMusicPlayer.Play
        MediaCtrl.Run()
        StateTracker.Start()
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        MediaCtrl.Stop()
        StateTracker.Stop()
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Dim musicUri = Sources(value)
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(musicUri)
        Dim absolutePath = cur.AbsolutePath
        Await Task.Run(Sub() MediaCtrl.RenderFile(absolutePath))
        Volume = Volume
    End Function

    Private Sub StateTracker_Tick(sender As Object, e As EventArgs) Handles StateTracker.Tick
        Dim state As DirectShowLib.FilterState
        MediaCtrl.GetState(100, state)
        If state = DirectShowLib.FilterState.Stopped Then
            RaiseEvent SingleSongComplete(Me, EventArgs.Empty)
        End If
    End Sub
End Class
