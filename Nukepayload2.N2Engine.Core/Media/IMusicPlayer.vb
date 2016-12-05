Namespace Media
    ''' <summary>
    ''' 背景音乐的播放器。
    ''' </summary>
    Public Interface IMusicPlayer
        Inherits IDisposable
        ''' <summary>
        ''' 加载播放器所需资源
        ''' </summary>
        Function LoadAsync() As Task
        ''' <summary>
        ''' 音频文件名源
        ''' </summary>
        ReadOnly Property Sources As IReadOnlyList(Of Uri)
        ''' <summary>
        ''' 设置播放数据源
        ''' </summary>
        ''' <param name="value">播放列表</param>
        Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task
        ''' <summary>
        ''' 播放的索引。设置该索引会引发音乐的重新播放。
        ''' </summary>
        ReadOnly Property PlayingIndex() As Integer
        ''' <summary>
        ''' 获取播放的索引
        ''' </summary>
        ''' <param name="value">要播放的音乐的索引</param>
        Function SetPlayingIndexAsync(value As Integer) As Task
        ''' <summary>
        ''' 表示声音的输出增益
        ''' </summary>
        Property Volume As Double
        ''' <summary>
        ''' 播放音乐
        ''' </summary>
        Sub Play()
        ''' <summary>
        ''' 暂停音乐
        ''' </summary>
        Sub Pause()
        ''' <summary>
        ''' 停止音乐
        ''' </summary>
        Sub [Stop]()
        ''' <summary>
        ''' 单曲完成后引发这个事件
        ''' </summary>
        Event SingleSongComplete As EventHandler
    End Interface
End Namespace