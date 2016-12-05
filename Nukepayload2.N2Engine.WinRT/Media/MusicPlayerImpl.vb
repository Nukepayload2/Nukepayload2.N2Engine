Imports Nukepayload2.N2Engine.Media
#If MONO Then
Imports Nukepayload2.N2Engine.MonoOnUWP.Media
#Else
Imports Nukepayload2.N2Engine.UWP.Media
#End If
Imports Windows.Storage

Friend Class MusicPlayerImpl

    WithEvents StateTracker As New DispatcherTimer With {.Interval = TimeSpan.FromMilliseconds(100)}
    Friend ReadOnly Property Playback As MusicPlayback

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        _Playback = New MusicPlayback
        Await Playback.LoadAsync
    End Function

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Return Playback.Volume
        End Get
        Set(value As Double)
            Playback.Volume = value
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        StateTracker.Stop()
        Playback.MusicFileInput.Stop()
    End Sub

    Public Sub Play() Implements IMusicPlayer.Play
        Playback.MusicFileInput.Start()
        Playback.IsPlaying = True
        StateTracker.Start()
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        StateTracker.Stop()
        Playback.MusicFileInput.Reset()
        Playback.MusicFileInput.Stop()
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        _PlayingIndex = value
        Await Playback.LoadFileAsync(Await StorageFile.GetFileFromApplicationUriAsync(N2Engine.Resources.ResourceLoader.GetForCurrentView.GetResourceUri(Sources(value))))
    End Function

    Private Sub RemoveGlobalHandlers()
        Playback.Dispose()
    End Sub

    Private Sub StateTracker_Tick(sender As Object, e As Object) Handles StateTracker.Tick
        If Playback.MusicFileInput.Position >= Playback.MusicFileInput.Duration Then
            RaiseEvent SingleSongComplete(Me, EventArgs.Empty)
        End If
    End Sub
End Class
