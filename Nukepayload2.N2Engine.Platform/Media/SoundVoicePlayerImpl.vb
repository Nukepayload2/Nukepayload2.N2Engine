Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.Platform
''' <summary>
''' 声音播放器
''' </summary>
<PlatformImpl(GetType(ISoundVoicePlayer))>
Partial Friend Class SoundVoicePlayerImpl
    Implements ISoundVoicePlayer

    Sub New(musicPlayer As IMusicPlayer)
        Prepare(musicPlayer)
    End Sub

    Partial Private Sub Prepare(musicPlayer As IMusicPlayer)

    End Sub
End Class
