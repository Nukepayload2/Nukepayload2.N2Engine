Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math

Namespace UI.Elements
    ''' <summary>
    ''' 用于滚动查看游戏对象。如果不使用场景，则建议使用此元素作为游戏对象树的根。
    ''' </summary>
    Public Class GameVirtualizingScrollViewer
        Inherits GameVisualContainer
        ''' <summary>
        ''' 内部元素的位置偏移。如果不绑定值则不偏移对待。
        ''' </summary>
        Public ReadOnly Property Offset As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 缩放比例。如果不绑定值则按 1 对待。
        ''' </summary>
        Public ReadOnly Property Zoom As New PropertyBinder(Of Single)
        ''' <summary>
        ''' 被通知正在卷动时引发此事件。
        ''' </summary>
        Public Event Scrolling As GameObjectEventHandler(Of GameVirtualizingScrollViewer, EventArgs)
        ''' <summary>
        ''' 显式通知此视图正在卷动
        ''' </summary>
        Public Sub RaiseScrolling(e As EventArgs)
            RaiseEvent Scrolling(Me, e)
        End Sub
        ''' <summary>
        ''' 获取内容被显示出来的矩形。
        ''' </summary>
        Public Function GetSourceRectangle(ByRef destTopLeft As Vector2, ByRef destSize As Vector2) As (srcTopLeft As Vector2, srcSize As Vector2)
            Dim srcTransform = Matrix3x2.Identity
            Dim srcTopLeft = Offset.Value
            Dim size = Vector2.Min(RenderSize, destSize)
            If Zoom.CanRead Then
                srcTransform *= Matrix3x2.CreateScale(1 / Me.Zoom.Value, size / 2)
            End If
            Dim srcSize = (size + srcTopLeft).ApplyTransform(srcTransform) - srcTopLeft.ApplyTransform(srcTransform)
            If srcTopLeft.X < 0 Then
                destTopLeft.X -= srcTopLeft.X
                srcTopLeft.X = 0
            End If
            If srcTopLeft.Y < 0 Then
                destTopLeft.Y -= srcTopLeft.Y
                srcTopLeft.Y = 0
            End If
            If srcTopLeft.X + srcSize.X > destTopLeft.X + destSize.X Then
                destSize.X -= srcTopLeft.X
                srcSize.X -= srcTopLeft.X
            End If
            If srcTopLeft.Y + srcSize.Y > destTopLeft.Y + destSize.Y Then
                destSize.Y -= srcTopLeft.Y
                srcSize.Y -= srcTopLeft.Y
            End If
            Return (srcTopLeft, srcSize)
        End Function
    End Class

End Namespace
