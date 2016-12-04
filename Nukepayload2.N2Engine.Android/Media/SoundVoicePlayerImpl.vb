Imports Nukepayload2.N2Engine.Media

Friend Class SoundVoicePlayerImpl

    Dim _soundPlayer As New MusicPlayerImpl
    Dim _voicePlayer As New MusicPlayerImpl

    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Return _soundPlayer.Volume
        End Get
        Set(value As Double)
            _soundPlayer.Volume = value
        End Set
    End Property

    Public Property VoiceVolume As Double Implements ISoundVoicePlayer.VoiceVolume
        Get
            Return _voicePlayer.Volume
        End Get
        Set(value As Double)
            _voicePlayer.Volume = value
        End Set
    End Property

    Private Sub Prepare(musicPlayer As IMusicPlayer)

    End Sub

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await _soundPlayer.SetSourcesAsync({soundUri})
        _soundPlayer.Play()
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await _voicePlayer.SetSourcesAsync({voiceUri})
        _voicePlayer.Play()
    End Function
End Class
