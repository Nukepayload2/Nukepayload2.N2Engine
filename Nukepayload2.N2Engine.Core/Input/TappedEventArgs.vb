Namespace Input
    ''' <summary>
    ''' 为触摸或者鼠标按下发生的事件提供数据
    ''' </summary>
    Public Class TappedEventArgs
        Inherits EventArgs

        Sub New(location As Vector2)
            Me.Location = location
        End Sub
        ''' <summary>
        ''' 点击的位置
        ''' </summary>
        Public ReadOnly Property Location As Vector2
    End Class
End Namespace
