Imports Nukepayload2.N2Engine.Input

Namespace UI.Elements
    ''' <summary>
    ''' 为键盘事件提供数据
    ''' </summary>
    Public Class GameKeyboardEventArgs
        Inherits GameRoutedEventArgs

        Sub New(keyCode As Key, keyStatus As PhysicalKeyStatus)
            Me.KeyCode = keyCode
            Me.KeyStatus = keyStatus
        End Sub

        ''' <summary>
        ''' 代表哪个按键与这个事件相关
        ''' </summary>
        Public ReadOnly Property KeyCode As Key
        ''' <summary>
        ''' 系统检测到的各种按键特征。
        ''' </summary>
        Public ReadOnly Property KeyStatus As PhysicalKeyStatus
    End Class
End Namespace