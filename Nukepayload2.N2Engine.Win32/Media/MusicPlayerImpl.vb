Imports Nukepayload2.N2Engine.Media

Friend Class MusicPlayerImpl

    Public Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        Throw New NotImplementedException()
    End Function

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Double)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        Throw New NotImplementedException()
    End Sub

    Dim MediaCtrl As DirectShowLib.IMediaControl = DirectCast(New DirectShowLib.FilterGraph, DirectShowLib.IMediaControl)

    Public Sub Play() Implements IMusicPlayer.Play
        MediaCtrl.Run()
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        Throw New NotImplementedException()
    End Sub

    Public Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Dim musicUri = Sources(value)
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(musicUri)
        Dim absolutePath = cur.AbsolutePath
        MediaCtrl.RenderFile(absolutePath)
    End Function
End Class
