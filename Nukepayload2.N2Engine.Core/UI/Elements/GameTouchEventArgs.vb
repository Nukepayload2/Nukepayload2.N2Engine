Namespace UI.Elements
    ''' <summary>
    ''' 包含上一条触摸事件消息返回的参数。
    ''' </summary>
    Public Class GameTouchEventArgs
        Inherits GameRoutedEventArgs

        Public Sub New(position As Vector2, pointerId As UInteger)
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
    End Class
End Namespace
