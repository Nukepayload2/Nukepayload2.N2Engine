Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.UWP.Media
Imports Windows.Storage

Friend Class MusicPlayerImpl

    Friend ReadOnly Property Playback As MusicPlayback

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        _Playback = New MusicPlayback
        Await Playback.LoadAsync
    End Function

    Dim _PlayingIndex As Integer
    Public ReadOnly Property PlayingIndex As Integer Implements IMusicPlayer.PlayingIndex
        Get
            Return _PlayingIndex
        End Get
    End Property

    Dim _Sources As IReadOnlyList(Of Uri)
    Public ReadOnly Property Sources As IReadOnlyList(Of Uri) Implements IMusicPlayer.Sources
        Get
            Return _Sources
        End Get
    End Property

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Return Playback.Volume
        End Get
        Set(value As Double)
            Playback.Volume = value
        End Set
    End Property

    Public Event SingleSongComplete As EventHandler Implements IMusicPlayer.SingleSongComplete

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

    Public Async Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task Implements IMusicPlayer.SetSourcesAsync
        _Sources = value
        Await SetPlayingIndexAsync(0)
    End Function
End Class
