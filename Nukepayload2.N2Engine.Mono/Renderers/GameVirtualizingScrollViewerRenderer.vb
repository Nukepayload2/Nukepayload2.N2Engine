Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class GameVirtualizingScrollViewerRenderer
    ''' <summary>
    ''' 实现虚拟化功能
    ''' </summary>
    ''' <param name="visual"></param>
    ''' <returns>是否应该虚拟化</returns>
    Protected Overrides Function ShouldVirtualize(visual As GameVisual) As Boolean
        Dim loc = visual.Location.Value
        If Not visual.Size.CanRead Then
            ' 不确定大小
            Return False
        End If
        Dim size = visual.Size.Value * 2
        Dim view = DirectCast(Me.View, GameVirtualizingScrollViewer)
        Dim viewOfs = view.Offset.Value
        Dim renderSize = view.RenderSize
        Dim renderBound = GetSourceRectangle(view, New Rectangle(viewOfs.X, viewOfs.Y, renderSize.X, renderSize.Y))
        Dim visualBound As New Rectangle(loc.X, loc.Y, size.X, size.Y)
        visual.IsVirtualizing = Not (renderBound.Contains(visualBound) OrElse renderBound.Intersects(visualBound))
        Return visual.IsVirtualizing
    End Function

    Protected Overrides Sub DrawOnParent(dc As SpriteBatch, destRect As Rectangle, effectedImage As Texture2D)
        Dim view = DirectCast(Me.View, GameVirtualizingScrollViewer)
        If view.Offset.CanRead OrElse view.Zoom.CanRead Then
            Dim srcRect = GetSourceRectangle(view, destRect)
            dc.Draw(effectedImage, destRect, srcRect, Color.White)
        Else
            MyBase.DrawOnParent(dc, destRect, effectedImage)
        End If
    End Sub

    Private Shared Function GetSourceRectangle(view As GameVirtualizingScrollViewer, ByRef destRect As Rectangle) As Rectangle
        Dim destTopLeft As New Numerics.Vector2(CSng(destRect.X), CSng(destRect.Y))
        Dim destSize As New Numerics.Vector2(CSng(destRect.Width), CSng(destRect.Height))
        Dim srcRectData = view.GetSourceRectangle(destTopLeft, destSize)
        With destRect
            .X = destTopLeft.X
            .Y = destTopLeft.Y
            .Width = destSize.X
            .Height = destSize.Y
        End With
        Dim srcTopLeft = srcRectData.srcTopLeft
        Dim srcSize = srcRectData.srcSize
        Dim srcRect As New Rectangle(srcTopLeft.X, srcTopLeft.Y, srcSize.X, srcSize.Y)
        Return srcRect
    End Function
End Class
