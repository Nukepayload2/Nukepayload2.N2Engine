Imports Nukepayload2.N2Engine.Foundation

Namespace Information
    ''' <summary>
    ''' 有关游戏的后缓冲区的信息
    ''' </summary>
    Public Class BackBufferInformation
        ''' <summary>
        ''' 游戏可见区域大小发生改变。请务必在不需要此通知时取消订阅此事件。
        ''' </summary>
        Public Shared Event ViewPortSizeChanged As EventHandler
        ''' <summary>
        ''' DPI (像素密度) 设置发生改变。请务必在不需要此通知时取消订阅此事件。
        ''' </summary>
        Public Shared Event DpiSettingChanged As EventHandler
        ''' <summary>
        ''' 整个屏幕大小。
        ''' </summary>
        Public Shared Property ScreenSize As SizeInInteger
        ''' <summary>
        ''' 游戏可见区域的大小。
        ''' </summary>
        Public Shared Property ViewPortSize As SizeInInteger
        ''' <summary>
        ''' 当前的 DPI (像素密度)。如果使用 Mono Game 渲染器，则总是返回 96。对于 Win2D, 会返回 Windows 10 系统设置的 Dpi 值。
        ''' </summary>
        Public Shared Property Dpi As Single
        ''' <summary>
        ''' 通知大小改变
        ''' </summary>
        ''' <param name="newValue">新的大小</param>
        Friend Shared Sub NotifyViewPortSizeChanged(newValue As SizeInInteger)
            _ViewPortSize = newValue
            RaiseEvent ViewPortSizeChanged(Nothing, EventArgs.Empty)
        End Sub
        ''' <summary>
        ''' 通知 DPI (像素密度)改变。
        ''' </summary>
        ''' <param name="newValue">新的大小</param>
        Friend Shared Sub NotifyDpiChanged(newValue As Single)
            _Dpi = newValue
            RaiseEvent DpiSettingChanged(Nothing, EventArgs.Empty)
        End Sub
    End Class

End Namespace