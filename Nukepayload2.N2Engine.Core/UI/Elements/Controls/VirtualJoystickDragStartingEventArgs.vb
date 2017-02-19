Namespace UI.Controls
    ''' <summary>
    ''' 虚拟摇杆拖动正在开始事件的数据
    ''' </summary>
    Public Class VirtualJoystickDragStartingEventArgs
        Inherits EventArgs

        Sub New(startPoint As Vector2)
            Me.StartPoint = startPoint
        End Sub

        ''' <summary>
        ''' 虚拟摇杆的起始点。也就是触摸滑动的起点。
        ''' </summary>
        Public ReadOnly Property StartPoint As Vector2
    End Class

End Namespace