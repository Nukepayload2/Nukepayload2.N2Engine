﻿Imports Nukepayload2.N2Engine.Media
#If MONO Then
Imports Nukepayload2.N2Engine.MonoOnUWP.Media
#Else
Imports Nukepayload2.N2Engine.UWP.Media
#End If
Imports Windows.Storage

Friend Class SoundVoicePlayerImpl

    Public Property Playback As SoundPlayback

    Public Property SoundVolume As Double = 1 Implements ISoundVoicePlayer.SoundVolume

    Public Property VoiceVolume As Double = 1 Implements ISoundVoicePlayer.VoiceVolume

    Private Sub Prepare(musicPlayer As IMusicPlayer)
        Playback = New SoundPlayback(DirectCast(musicPlayer, MusicPlayerImpl).Playback)
    End Sub

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Dim msappxUri = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(soundUri)
        Await Playback.PlayAsync(Await StorageFile.GetFileFromApplicationUriAsync(msappxUri), SoundVolume)
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Dim msappxUri = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(voiceUri)
        Await Playback.PlayAsync(Await StorageFile.GetFileFromApplicationUriAsync(msappxUri), VoiceVolume)
    End Function
End Class
