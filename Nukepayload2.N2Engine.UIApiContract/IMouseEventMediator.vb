Imports Nukepayload2.N2Engine.Input

Namespace UI
    ''' <summary>
    ''' 提供当前检测到的鼠标状态
    ''' </summary>
    Public Interface IMouseStatusProvider
        ReadOnly Property MouseKeyStatus As IReadOnlyList(Of ButtonState)
        ReadOnly Property WheelValue As Integer
    End Interface

    ''' <summary>
    ''' 检测鼠标状态，引发鼠标事件。
    ''' </summary>
    Public Interface IMouseEventMediator
        Inherits IEventMediator, IMouseStatusProvider

    End Interface

End Namespace
