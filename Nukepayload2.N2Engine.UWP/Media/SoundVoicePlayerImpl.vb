Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.UWP.Media
Imports Windows.Storage

Friend Class SoundVoicePlayerImpl

    Sub New(musicPlayer As IMusicPlayer)
        Playback = New SoundPlayback(DirectCast(musicPlayer, MusicPlayerImpl).Playback)
    End Sub

    Public Property Playback As SoundPlayback

    Public Property SoundVolume As Double = 1 Implements ISoundVoicePlayer.SoundVolume

    Public Property VoiceVolume As Double = 1 Implements ISoundVoicePlayer.VoiceVolume

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await Playback.PlayAsync(Await StorageFile.GetFileFromApplicationUriAsync(soundUri), SoundVolume)
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await Playback.PlayAsync(Await StorageFile.GetFileFromApplicationUriAsync(voiceUri), VoiceVolume)
    End Function
End Class
