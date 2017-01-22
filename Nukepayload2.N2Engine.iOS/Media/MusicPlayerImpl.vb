Imports AVFoundation
Imports Foundation
Imports Nukepayload2.N2Engine.Media

Friend Class MusicPlayerImpl

    WithEvents Avp As AVAudioPlayer

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        Await Task.Run(Sub（）
                           Dim session = AVAudioSession.SharedInstance
                           session.SetCategory(AVAudioSessionCategory.Ambient)
                           session.SetActive(True)
                       End Sub)
    End Function

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Return Avp.Volume
        End Get
        Set(value As Double)
            Avp.Volume = value
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        Avp.Pause()
    End Sub

    Public Sub Play() Implements IMusicPlayer.Play
        Avp.Play()
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        Avp.Stop()
    End Sub

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        _PlayingIndex = value
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(Sources(value))
        Dim absolutePath As String = cur.AbsolutePath
        Await Task.Run(Sub()
                           Dim err As NSError = Nothing
                           Avp?.Dispose()
                           Avp = New AVAudioPlayer(New NSUrl(absolutePath),
                                                   absolutePath.Substring(absolutePath.LastIndexOf(".") + 1),
                                                   err) With {.Volume = Volume}
                       End Sub)
    End Function

    Private Sub Avp_FinishedPlaying(sender As Object, e As AVStatusEventArgs) Handles Avp.FinishedPlaying
        RaiseEvent SingleSongComplete(sender, e)
    End Sub
End Class
