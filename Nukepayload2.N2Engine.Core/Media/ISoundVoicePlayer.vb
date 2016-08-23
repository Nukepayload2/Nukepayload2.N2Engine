''' <summary>
''' 用于播放短暂但要求时效性强的声音
''' </summary>
Public Interface ISoundVoicePlayer
    ''' <summary>
    ''' 0到100的音效音量
    ''' </summary>
    Property SoundVolume() As Double
    ''' <summary>
    ''' 0到100的语音音量
    ''' </summary>
    Property VoiceVolume() As Double

    Function PlaySoundAsync(soundUri As Uri) As Task
    Function PlayVoiceAsync(voiceUri As Uri) As Task
End Interface