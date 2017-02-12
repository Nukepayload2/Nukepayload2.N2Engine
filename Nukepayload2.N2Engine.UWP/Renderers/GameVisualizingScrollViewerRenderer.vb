Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class GameVirtualizingScrollViewerRenderer
    ''' <summary>
    ''' 实现虚拟化功能
    ''' </summary>
    ''' <param name="visual"></param>
    ''' <returns>是否应该虚拟化</returns>
    Protected Overrides Function ShouldVirtualize(visual As GameVisual) As Boolean
        Return MyBase.ShouldVirtualize(visual)
    End Function

    Protected Overrides Sub DrawOnParent(ds As CanvasDrawingSession, loc As Vector2, effectedImage As ICanvasImage)
        Dim view = DirectCast(Me.View, GameVirtualizingScrollViewer)
        If view.Offset.CanRead Then
            loc += view.Offset.Value
        End If
        MyBase.DrawOnParent(ds, loc, effectedImage)
    End Sub
End Class
