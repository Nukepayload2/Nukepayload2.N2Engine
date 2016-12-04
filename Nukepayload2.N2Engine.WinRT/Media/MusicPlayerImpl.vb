Imports Nukepayload2.N2Engine.Media
#If MONO Then
Imports Nukepayload2.N2Engine.MonoOnUWP.Media
#Else
Imports Nukepayload2.N2Engine.UWP.Media
#End If
Imports Windows.Storage

Friend Class MusicPlayerImpl

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
        Playback.MusicFileInput.Stop()
    End Sub

    Public Sub Play() Implements IMusicPlayer.Play
        Playback.MusicFileInput.Start()
        Playback.IsPlaying = True
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        Playback.MusicFileInput.Reset()
        Playback.MusicFileInput.Stop()
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        _PlayingIndex = value
        Await Playback.LoadFileAsync(Await StorageFile.GetFileFromApplicationUriAsync(N2Engine.Resources.ResourceLoader.GetForCurrentView.GetResourceUri(Sources(value))))
    End Function
End Class
