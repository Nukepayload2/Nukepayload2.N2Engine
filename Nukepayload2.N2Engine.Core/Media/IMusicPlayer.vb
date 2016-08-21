''' <summary>
''' 背景音乐的播放器。
''' </summary>
Public Interface IMusicPlayer
    ''' <summary>
    ''' 音频文件名源
    ''' </summary>
    ReadOnly Property Sources As IReadOnlyList(Of Uri)
    Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task
    ''' <summary>
    ''' 播放的索引。设置该索引会引发音乐的重新播放。
    ''' </summary>
    ReadOnly Property PlayingIndex() As Integer
    Function SetPlayingIndexAsync(value As Integer) As Task
    Property Volume As Double
    Sub Play()
    Sub Pause()
    Sub StopMusic()
    ''' <summary>
    ''' 单曲完成后引发这个事件
    ''' </summary>
    Event SingleSongComplete As EventHandler
End Interface