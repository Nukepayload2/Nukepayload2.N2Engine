Imports AVFoundation
Imports Foundation
Imports Nukepayload2.N2Engine.Media

Friend Class SoundVoicePlayerImpl
    Dim soundPlayers As New Queue(Of AVAudioPlayer)(8)
    Dim voicePlayers As New Queue(Of AVAudioPlayer)(8)

    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Return soundPlayers.Peek.Volume
        End Get
        Set(value As Double)
            For Each p In soundPlayers
                If p.Playing Then
                    p.Volume = value
                End If
            Next
        End Set
    End Property

    Public Property VoiceVolume As Double Implements ISoundVoicePlayer.VoiceVolume
        Get
            Return voicePlayers.Peek.Volume
        End Get
        Set(value As Double)
            For Each p In voicePlayers
                If p.Playing Then
                    p.Volume = value
                End If
            Next
        End Set
    End Property

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await PlayUriAsync(soundUri, soundPlayers, SoundVolume)
    End Function

    Private Async Function PlayUriAsync(soundUri As Uri, players As Queue(Of AVAudioPlayer), volume As Double) As Task
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(soundUri)
        Dim absolutePath As String = cur.AbsolutePath
        Await Task.Run(Sub()
                           Do While players.Count >= 8
                               players.Dequeue.Dispose()
                           Loop
                           Dim aVAudioPlayer1 As New AVAudioPlayer(New NSUrl(absolutePath),
                                           absolutePath.Substring(absolutePath.LastIndexOf(".") + 1),
                                           Nothing) With {.Volume = volume}
                           players.Enqueue(aVAudioPlayer1)
                           aVAudioPlayer1.Play()
                       End Sub)
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await PlayUriAsync(voiceUri, soundPlayers, VoiceVolume)
    End Function
End Class
