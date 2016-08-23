Public Class SoundVoicePlayer
    Implements ISoundVoicePlayer

    Dim platformPlayer As ISoundVoicePlayer

    Sub New()
        platformPlayer = PlatformActivator.CreateBaseInstance(Of SoundPlayer, ISoundVoicePlayer)
    End Sub

    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Return platformPlayer.SoundVolume
        End Get
        Set(value As Double)
            platformPlayer.SoundVolume = value
        End Set
    End Property

    Public Property VoiceVolume As Double Implements ISoundVoicePlayer.VoiceVolume
        Get
            Return platformPlayer.VoiceVolume
        End Get
        Set(value As Double)
            platformPlayer.VoiceVolume = value
        End Set
    End Property

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await platformPlayer.PlaySoundAsync(soundUri)
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await platformPlayer.PlayVoiceAsync(voiceUri)
    End Function
End Class
