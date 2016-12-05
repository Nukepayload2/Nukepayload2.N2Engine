Imports Nukepayload2.N2Engine.Media

Friend Class SoundVoicePlayerImpl

    Dim _SoundVolume As Double = 1.0
    Public Property SoundVolume As Double Implements ISoundVoicePlayer.SoundVolume
        Get
            Return _SoundVolume
        End Get
        Set(value As Double)
            _SoundVolume = value
            For Each se In SEPlayer
                Dim state As DirectShowLib.FilterState
                DirectCast(se, DirectShowLib.IMediaControl).GetState(1000, state)
                If state <> DirectShowLib.FilterState.Stopped Then
                    DirectCast(se, DirectShowLib.IBasicAudio).put_Volume(VolumeToComVolume(value))
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
            For Each voice In VoicePlayer
                Dim state As DirectShowLib.FilterState
                DirectCast(voice, DirectShowLib.IMediaControl).GetState(1000, state)
                If state <> DirectShowLib.FilterState.Stopped Then
                    DirectCast(voice, DirectShowLib.IBasicAudio).put_Volume(VolumeToComVolume(value))
                End If
            Next
        End Set
    End Property

    Private Shared Function VolumeToComVolume(value As Double) As Integer
        Return (value - 1) * 10000
    End Function

    Dim SEPlayer As New Queue(Of DirectShowLib.FilterGraph)(8)
    Dim VoicePlayer As New Queue(Of DirectShowLib.FilterGraph)(8)

    Public Async Function PlaySoundAsync(soundUri As Uri) As Task Implements ISoundVoicePlayer.PlaySoundAsync
        Await PlayAsync(soundUri, SEPlayer, SoundVolume)
    End Function

    Private Async Function PlayAsync(soundUri As Uri, players As Queue(Of DirectShowLib.FilterGraph), volume As Double) As Task
        Dim cur = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(soundUri)
        Dim absolutePath = cur.AbsolutePath
        Await Task.Run(Sub()
                           Do While players.Count >= 8
                               players.Dequeue()
                           Loop
                           Dim filterGraph As New DirectShowLib.FilterGraph
                           Dim mediaControl = DirectCast(filterGraph, DirectShowLib.IMediaControl)
                           Dim basicAudio = DirectCast(filterGraph, DirectShowLib.IBasicAudio)
                           SEPlayer.Enqueue(mediaControl)
                           mediaControl.RenderFile(absolutePath)
                           basicAudio.put_Volume(VolumeToComVolume(volume))
                           mediaControl.Run()
                       End Sub)
    End Function

    Public Async Function PlayVoiceAsync(voiceUri As Uri) As Task Implements ISoundVoicePlayer.PlayVoiceAsync
        Await PlayAsync(voiceUri, VoicePlayer, VoiceVolume)
    End Function
End Class
