Namespace Media
    ''' <summary>
    ''' 用于播放短暂但要求时效性强的声音
    ''' </summary>
    Public Interface ISoundVoicePlayer
        Inherits IDisposable
        ''' <summary>
        ''' 0到100的音效音量
        ''' </summary>
        Property SoundVolume As Double
        ''' <summary>
        ''' 0到100的语音音量
        ''' </summary>
        Property VoiceVolume As Double
        ''' <summary>
        ''' 播放指定的声音
        ''' </summary>
        ''' <param name="soundUri">声音资源路径</param>
        Function PlaySoundAsync(soundUri As Uri) As Task
        ''' <summary>
        ''' 播放指定的语音
        ''' </summary>
        ''' <param name="voiceUri">语音资源路径</param>
        Function PlayVoiceAsync(voiceUri As Uri) As Task
    End Interface
End Namespace