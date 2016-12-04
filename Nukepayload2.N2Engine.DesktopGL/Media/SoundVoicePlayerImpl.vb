Imports Nukepayload2.N2Engine.Media

Friend Class SoundVoicePlayerImpl

    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Double)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property VoiceVolume As Double Implements ISoundVoicePlayer.VoiceVolume
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Double)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Throw New NotImplementedException()
    End Function

    Public Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Throw New NotImplementedException()
    End Function
End Class
