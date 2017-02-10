Namespace UI.Elements
    ''' <summary>
    ''' 包含上一条触摸事件消息返回的参数。
    ''' </summary>
    Public Class GameTouchEventArgs
        Inherits GameRoutedEventArgs
        ''' <summary>
        ''' 初始化触摸按下事件的数据
        ''' </summary>
        Public Sub New(position As Vector2, pointerId As UInteger, pressure As Single)
            Me.Position = position
            Me.PointerId = pointerId
            Me.Pressure = pressure
        End Sub
        ''' <summary>
        ''' 初始化触摸滑动事件的数据
        ''' </summary>
        Public Sub New(position As Vector2, lastPosition As Vector2, pointerId As UInteger, pressure As Single)
            Me.Position = position
            Me.PointerId = pointerId
            Me.Pressure = pressure
            Me.LastPosition = lastPosition
        End Sub
        ''' <summary>
        ''' 初始化触摸松开事件的数据
        ''' </summary>
        Public Sub New(pointerId As UInteger, position As Vector2)
            Me.Position = position
            Me.PointerId = pointerId
        End Sub
        ''' <summary>
        ''' 触摸点的位置
        ''' </summary>
        Public ReadOnly Property Position As Vector2
        ''' <summary>
        ''' 触摸点系统分配的编号
        ''' </summary>
        Public ReadOnly Property PointerId As UInteger
        ''' <summary>
        ''' 按压的力度。这需要相关的硬件支持。
        ''' </summary>
        Public ReadOnly Property Pressure As Single
        ''' <summary>
        ''' 移动之前的位置。如果没移动则为原点。
        ''' </summary>
        Public ReadOnly Property LastPosition As Vector2
    End Class
End Namespace
