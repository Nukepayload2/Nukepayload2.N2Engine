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

    Protected Overrides Function ApplyEffect(source As CanvasRenderTarget) As ICanvasImage
        Return MyBase.ApplyEffect(source)
    End Function
End Class
