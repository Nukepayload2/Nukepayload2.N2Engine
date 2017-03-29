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
        Dim view = DirectCast(MyBase.View, GameVirtualizingScrollViewer)
        Dim viewOfs = view.Offset.Value
        Dim renderSize = view.RenderSize
        Dim renderSourceBound = GetSourceRectangle(view, New Rect(viewOfs.X, viewOfs.Y, renderSize.X, renderSize.Y))
        Dim visualBound As New Rect(loc.X, loc.Y, size.X, size.Y)
        renderSourceBound.Intersect(visualBound)
        visual.IsVirtualizing = renderSourceBound.Width <= 0.3 OrElse renderSourceBound.Height <= 0.3
        Return visual.IsVirtualizing
    End Function

    Private Function GetSourceRectangle(view As GameVirtualizingScrollViewer, ByRef destRect As Rect) As Rect
        Dim destTopLeft As New Vector2(CSng(destRect.X), CSng(destRect.Y))
        Dim destSize As New Vector2(CSng(destRect.Width), CSng(destRect.Height))
        Dim srcRectData = view.GetSourceRectangle(destTopLeft, destSize)
        With destRect
            .X = destTopLeft.X
            .Y = destTopLeft.Y
            .Width = destSize.X
            .Height = destSize.Y
        End With
        Dim srcTopLeft = srcRectData.srcTopLeft
        Dim srcSize = srcRectData.srcSize
        Dim srcRect As New Rect(srcTopLeft.X, srcTopLeft.Y, srcSize.X, srcSize.Y)
        Return srcRect
    End Function

    Protected Overrides Sub DrawOnParent(ds As CanvasDrawingSession, loc As Vector2, effectedImage As ICanvasImage)
        Dim view = DirectCast(Me.View, GameVirtualizingScrollViewer)
        If view.Offset.CanRead OrElse view.Zoom.CanRead Then
            Dim renderSize = view.RenderSize
            Dim destRect As New Rect(loc.X, loc.Y, renderSize.X, renderSize.Y)
            Dim srcRect = GetSourceRectangle(view, destRect)
            ds.DrawImage(effectedImage, destRect, srcRect)
        Else
            MyBase.DrawOnParent(ds, loc, effectedImage)
        End If
    End Sub

End Class
