Imports Nukepayload2.N2Engine.UI

Namespace Animations

    Public MustInherit Class TimelineAnimation
        Inherits TimeAction
        ''' <summary>
        ''' 持续的时间
        ''' </summary>
        Public Property Duration As TimeSpan
        ''' <summary>
        ''' 循环次数。设置为 -1 会无限循环。
        ''' </summary>
        Public Property LoopCount As Integer
        ''' <summary>
        ''' 是否播放完了之后倒着播放回去
        ''' </summary>
        Public Property AutoReverse As Boolean
        ''' <summary>
        ''' 用于调整时间进度的函数表达式。输入和输出值由0开始，1结束。默认情况下是不调整进度。
        ''' </summary>
        Public Property ProgressExpression As Func(Of Single, Single) = Function(f) f
        ''' <summary>
        ''' 暂停此动画
        ''' </summary>
        Public MustOverride Sub Pause()
        ''' <summary>
        ''' 停止此动画
        ''' </summary>
        Public MustOverride Sub [Stop]()

    End Class
End Namespace
