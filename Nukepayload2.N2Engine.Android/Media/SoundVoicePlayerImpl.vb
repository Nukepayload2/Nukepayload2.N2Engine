Imports Android.Media
Imports Nukepayload2.N2Engine.Media

Friend Class SoundVoicePlayerImpl

    Dim _soundPlayer As New Queue(Of MusicPlayerImpl)(8)
    Dim _voicePlayer As New Queue(Of MusicPlayerImpl)(8)

    Dim _SoundVolume As Double = 1.0
    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Return _SoundVolume
        End Get
        Set(value As Double)
            For Each p In _soundPlayer
                p.Volume = value
            Next
            _SoundVolume = value
        End Set
    End Property

    Dim _VoiceVolume As Double = 1.0
    Public Property VoiceVolume As Double Implements ISoundVoicePlayer.VoiceVolume
        Get
            Return _VoiceVolume
        End Get
        Set(value As Double)
            _VoiceVolume = value
            For Each p In _voicePlayer
                p.Volume = value
            Next
        End Set
    End Property

    Private Sub Prepare(musicPlayer As IMusicPlayer)

    End Sub

    Private Async Function PlayUriAsync(soundUri As Uri, players As Queue(Of MusicPlayerImpl), volume As Double) As Task
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(soundUri)
        Dim absolutePath As String = cur.AbsolutePath
        Do While players.Count >= 8
            players.Dequeue()
        Loop
        Dim player As New MusicPlayerImpl
        Await player.LoadAsync
        Await player.SetSourcesAsync({soundUri})
        player.Volume = volume
        players.Enqueue(player)
        player.Play()
    End Function

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await PlayUriAsync(soundUri, _soundPlayer, SoundVolume)
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await PlayUriAsync(voiceUri, _voicePlayer, VoiceVolume)
    End Function

    Private Sub DisposePlayers()
        DisposePlayers(Sub(p) p.Dispose(), _soundPlayer, _voicePlayer)
    End Sub
End Class
