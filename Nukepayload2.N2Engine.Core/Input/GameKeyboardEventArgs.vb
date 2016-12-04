Namespace Input
    ''' <summary>
    ''' 为键盘事件提供数据
    ''' </summary>
    Public Class GameKeyboardEventArgs
        Inherits EventArgs

        Sub New(keyCode As Key)
            Me.KeyCode = keyCode
        End Sub

        ''' <summary>
        ''' 代表哪个按键与这个事件相关
        ''' </summary>
        Public ReadOnly Property KeyCode As Key
    End Class
End Namespace