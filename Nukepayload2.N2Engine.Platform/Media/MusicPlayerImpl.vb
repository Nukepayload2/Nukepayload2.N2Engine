Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.Platform
''' <summary>
''' 音乐播放器
''' </summary>
<PlatformImpl(GetType(IMusicPlayer))>
Partial Friend Class MusicPlayerImpl
    Implements IMusicPlayer

    Dim _PlayingIndex As Integer
    Public ReadOnly Property PlayingIndex As Integer Implements IMusicPlayer.PlayingIndex
        Get
            Return _PlayingIndex
        End Get
    End Property

    Dim _Sources As IReadOnlyList(Of Uri)
    Public ReadOnly Property Sources As IReadOnlyList(Of Uri) Implements IMusicPlayer.Sources
        Get
            Return _Sources
        End Get
    End Property

    Public Event SingleSongComplete As EventHandler Implements IMusicPlayer.SingleSongComplete

    Public Async Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task Implements IMusicPlayer.SetSourcesAsync
        _Sources = value
        Await SetPlayingIndexAsync(0)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                RemoveGlobalHandlers
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    Partial Private Sub RemoveGlobalHandlers()

    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
