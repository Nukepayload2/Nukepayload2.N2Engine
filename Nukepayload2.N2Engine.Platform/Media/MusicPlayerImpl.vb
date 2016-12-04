Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.Platform
''' <summary>
''' 音乐播放器
''' </summary>
<PlatformImpl(GetType(IMusicPlayer))>
Partial Friend Class MusicPlayerImpl
    Implements IMusicPlayer

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

    Public Event SingleSongComplete As EventHandler Implements IMusicPlayer.SingleSongComplete

    Public Async Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task Implements IMusicPlayer.SetSourcesAsync
        _Sources = value
        Await SetPlayingIndexAsync(0)
    End Function
End Class
