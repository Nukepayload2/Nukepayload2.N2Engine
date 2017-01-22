Imports Microsoft.Xna.Framework.Media
Imports Nukepayload2.N2Engine.Media

Friend Class MusicPlayerImpl

    Dim curSong As Song

    Sub New()
        AddHandler MediaPlayer.MediaStateChanged, AddressOf OnStateChange
    End Sub

    Private Sub OnStateChange(sender As Object, e As EventArgs)
        If MediaPlayer.State = MediaState.Stopped Then
            RaiseEvent SingleSongComplete(Me, e)
        End If
    End Sub

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        Await Task.Delay(0)
    End Function

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Return MediaPlayer.Volume
        End Get
        Set(value As Double)
            MediaPlayer.Volume = value
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        MediaPlayer.Pause()
    End Sub

    Public Sub Play() Implements IMusicPlayer.Play
        MediaPlayer.Play(curSong)
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        MediaPlayer.Stop()
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Dim cur = Sources(value)
        Dim absolutePath = cur.AbsolutePath
        curSong?.Dispose()
        curSong = Await Task.Run(Function() Song.FromUri(absolutePath.Substring(absolutePath.LastIndexOf("/") + 1),
                                                         Resources.ResourceLoader.GetForCurrentView.GetResourceUri(cur)))
        _PlayingIndex = value
    End Function

    Private Sub RemoveGlobalHandlers()
        RemoveHandler MediaPlayer.MediaStateChanged, AddressOf OnStateChange
    End Sub
End Class
