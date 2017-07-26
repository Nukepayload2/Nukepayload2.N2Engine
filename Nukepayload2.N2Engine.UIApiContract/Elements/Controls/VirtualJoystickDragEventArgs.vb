Namespace UI.Controls
    ''' <summary>
    ''' 虚拟摇杆拖动整个过程的事件的数据
    ''' </summary>
    Public Class VirtualJoystickDragEventArgs
        Inherits EventArgs

        Sub New(startPoint As Vector2, endPoint As Vector2)
            Me.StartPoint = startPoint
            Me.EndPoint = endPoint
        End Sub
        ''' <summary>
        ''' 虚拟摇杆的起始点。也就是触摸滑动的起点。
        ''' </summary>
        Public ReadOnly Property StartPoint As Vector2
        ''' <summary>
        ''' 当前虚拟摇杆的触摸点。
        ''' </summary>
        Public ReadOnly Property EndPoint As Vector2
    End Class
End Namespace