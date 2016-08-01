Namespace Media
    ''' <summary>
    ''' 同时播放列表满了应当如何处理
    ''' </summary>
    Public Enum SoundMixListFullOperation
        ''' <summary>
        ''' 找第一个已经播放完的声音。如果找不到则不播放请求的声音
        ''' </summary>
        TryFirstFit
        ''' <summary>
        ''' 找第一个已经播放完的声音。如果找不到则抛出异常
        ''' </summary>
        FirstFit
        ''' <summary>
        ''' 找到近期最少使用的那个声音，如果还没播放完则使其强行停止，然后剥夺它的资源用于播放请求的声音
        ''' </summary>
        PreemptiveLRU
        ''' <summary>
        ''' 找到最先开始播放的那个声音，如果还没播放完则使其强行停止，然后剥夺它的资源用于播放请求的声音
        ''' </summary>
        PreemptiveFIFO
        ''' <summary>
        ''' 找到距离播放完成最近的一个声音，如果还没播放完则使其强行停止，然后剥夺它的资源用于播放请求的声音
        ''' </summary>
        PreemptiveSRT
    End Enum
End Namespace