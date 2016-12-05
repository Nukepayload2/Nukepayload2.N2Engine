Imports Microsoft.Xna.Framework.Audio
Imports Nukepayload2.N2Engine.Media

Friend Class SoundVoicePlayerImpl

    Dim curSe As New Queue(Of SoundEffectInstance)(8)
    Dim curVo As New Queue(Of SoundEffectInstance)(8)

    Dim _SoundVolume As Double = 1.0
    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Return _SoundVolume
        End Get
        Set(value As Double)
            _SoundVolume = value
            For Each se In curSe
                If Not se.IsDisposed AndAlso se.State <> SoundState.Stopped Then
                    se.Volume = value
                End If
            Next
        End Set
    End Property

    Dim _VoiceVolume As Double = 1.0
    Public Property VoiceVolume As Double Implements ISoundVoicePlayer.VoiceVolume
        Get
            Return _VoiceVolume
        End Get
        Set(value As Double)
            _VoiceVolume = value
            For Each vo In curVo
                If Not vo.IsDisposed AndAlso vo.State <> SoundState.Stopped Then
                    vo.Volume = value
                End If
            Next
        End Set
    End Property

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await Task.Run(Sub() PlayAudio(soundUri, curSe, SoundVolume))
    End Function

    Private Sub PlayAudio(soundUri As Uri, players As Queue(Of SoundEffectInstance), volume As Double)
        Dim se = SoundEffect.FromStream(IO.File.OpenRead(Resources.ResourceLoader.GetForCurrentView.GetResourceUri(soundUri).AbsolutePath))
        Dim sei = se.CreateInstance
        sei.Volume = volume
        Do While players.Count >= 8
            players.Dequeue.Dispose()
        Loop
        players.Enqueue(sei)
        sei.Play()
    End Sub

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await Task.Run(Sub() PlayAudio(voiceUri, curVo, VoiceVolume))
    End Function

    Private Sub DisposePlayers()
        DisposePlayers(Sub(p) p.Dispose(), curSe, curVo)
    End Sub
End Class
