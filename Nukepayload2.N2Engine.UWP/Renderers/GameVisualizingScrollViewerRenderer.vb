Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class GameVirtualizingScrollViewerRenderer
    ''' <summary>
    ''' 实现虚拟化功能
    ''' </summary>
    ''' <param name="visual">要检查的可见对象</param>
    ''' <returns>是否应该虚拟化</returns>
    Protected Overrides Function ShouldVirtualize(visual As GameVisual) As Boolean
        Dim rtSize = RenderTarget.Size
        Dim loc = visual.Location.Value
        If Not visual.Size.CanRead Then
            Return False
        End If
        Dim size = visual.Size.Value * 2
        Dim renderBound As New Rect(0, 0, rtSize.Width, rtSize.Height)
        Dim visualBound As New Rect(loc.X, loc.Y, size.X, size.Y)
        renderBound.Intersect(visualBound)
        visual.IsVirtualizing = renderBound.Width <= 0.3 OrElse renderBound.Height <= 0.3
        Return visual.IsVirtualizing
    End Function

    Protected Overrides Sub DrawOnParent(ds As CanvasDrawingSession, loc As Vector2, effectedImage As ICanvasImage)
        Dim view = DirectCast(Me.View, GameVirtualizingScrollViewer)
        If view.Offset.CanRead Then
            loc += view.Offset.Value
        End If
        If view.Zoom.CanRead Then
            Dim zoom = view.Zoom.Value
            If Math.Abs(zoom - 1.0F) > 0.1 Then
                Dim rtSize = RenderTarget.Size
                ds.DrawImage(effectedImage, New Rect(loc.X, loc.Y, rtSize.Width * zoom, rtSize.Height * zoom), New Rect(0, 0, rtSize.Width, rtSize.Height))
                Return
            End If
        End If
        MyBase.DrawOnParent(ds, loc, effectedImage)
    End Sub
End Class
