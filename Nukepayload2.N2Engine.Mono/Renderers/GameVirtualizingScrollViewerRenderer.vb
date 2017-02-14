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
        Dim size = visual.Size.Value
        Dim renderBound As New Rectangle(0, 0, RenderTarget.Width, RenderTarget.Height)
        Dim visualBound As New Rectangle(loc.X, loc.Y, size.X, size.Y)
        visual.IsVirtualizing = Not renderBound.Contains(visualBound)
        Return visual.IsVirtualizing
    End Function

    Protected Overrides Sub DrawOnParent(dc As SpriteBatch, drawSize As Rectangle, effectedImage As Texture2D)
        Dim view = DirectCast(Me.View, GameVirtualizingScrollViewer)
        If view.Offset.CanRead Then
            Dim ofs = view.Offset.Value
            drawSize.X += ofs.X
            drawSize.Y += ofs.Y
        End If
        If view.Zoom.CanRead Then
            Dim zoom = view.Zoom.Value
            If Math.Abs(zoom - 1.0F) > 0.1 Then
                dc.Draw(effectedImage, destinationRectangle:=New Rectangle(drawSize.X, drawSize.Y, RenderTarget.Width * zoom, RenderTarget.Height * zoom),
                       sourceRectangle:=New Rectangle(0, 0, RenderTarget.Width, RenderTarget.Height))
                Return
            End If
        End If
        MyBase.DrawOnParent(dc, drawSize, effectedImage)
    End Sub
End Class
