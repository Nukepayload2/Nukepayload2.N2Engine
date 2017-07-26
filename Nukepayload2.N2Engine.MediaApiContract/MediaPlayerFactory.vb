Namespace Media

    Public Module MediaPlayerFactory
        ''' <summary>
        ''' 创建游戏所需的 BGM 和 SE+语音 播放器
        ''' </summary>
        Async Function CreateAudioPlayersAsync() As Task(Of Tuple(Of IMusicPlayer, ISoundVoicePlayer))
            Dim music = Platform.PlatformActivator.CreateBaseInstance(Of IMusicPlayer)
            Await music.LoadAsync
            Dim voice = Platform.PlatformActivator.CreateBaseInstance(Of ISoundVoicePlayer)(music)
            Return New Tuple(Of IMusicPlayer, ISoundVoicePlayer)(music, voice)
        End Function
    End Module

End Namespace