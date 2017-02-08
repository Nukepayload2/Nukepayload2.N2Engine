Imports Nukepayload2.N2Engine.Input

Namespace UI.Elements
    ''' <summary>
    ''' 包含上一条鼠标事件消息返回的参数。
    ''' </summary>
    Public Class GameMouseEventArgs
        Inherits GameRoutedEventArgs

        ''' <summary>
        ''' 鼠标位置变化时初始化
        ''' </summary>
        Sub New(keyModifiers As VirtualKeyModifiers, position As Vector2)
            Me.KeyModifiers = keyModifiers
            Me.Position = position
        End Sub
        ''' <summary>
        ''' 鼠标按键变化时初始化
        ''' </summary>
        Sub New(keyModifiers As VirtualKeyModifiers, position As Vector2, mouseButtons As MouseKeys)
            Me.KeyModifiers = keyModifiers
            Me.Position = position
            Me.MouseButtons = mouseButtons
        End Sub
        ''' <summary>
        ''' 鼠标滚轮变化时初始化
        ''' </summary>
        Sub New(keyModifiers As VirtualKeyModifiers, position As Vector2, wheelDelta As Single)
            Me.KeyModifiers = keyModifiers
            Me.Position = position
            Me.WheelDelta = wheelDelta
        End Sub
        ''' <summary>
        ''' 哪些键修饰符当前处于活动状态
        ''' </summary>
        ''' <returns>一个或多个键对应的值</returns>
        Public ReadOnly Property KeyModifiers As VirtualKeyModifiers
        ''' <summary>
        ''' 当前鼠标指针对于当前游戏对象的坐标系的位置。
        ''' </summary>
        ''' <returns>当前鼠标指针的位置</returns>
        Public ReadOnly Property Position As Vector2
        ''' <summary>
        ''' 与此事件相关的鼠标按键。
        ''' </summary>
        Public ReadOnly Property MouseButtons As MouseKeys
        ''' <summary>
        ''' 鼠标滚轮的变化
        ''' </summary>
        Public ReadOnly Property WheelDelta As Single
    End Class
End Namespace
