Imports Nukepayload2.N2Engine.Core

<PlatformImpl(GetType(Core.MusicPlayer))>
Friend Class MusicPlayer
    Implements IMusicPlayer

    Public ReadOnly Property PlayingIndex As Integer Implements IMusicPlayer.PlayingIndex
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property Sources As IReadOnlyList(Of Uri) Implements IMusicPlayer.Sources
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Double)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Event SingleSongComplete As EventHandler Implements IMusicPlayer.SingleSongComplete

    Public Sub Pause() Implements IMusicPlayer.Pause
        Throw New NotImplementedException()
    End Sub

    Public Sub Play() Implements IMusicPlayer.Play
        Throw New NotImplementedException()
    End Sub

    Public Sub StopMusic() Implements IMusicPlayer.StopMusic
        Throw New NotImplementedException()
    End Sub

    Public Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task Implements IMusicPlayer.SetSourcesAsync
        Throw New NotImplementedException()
    End Function
End Class
