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
        Return MyBase.ShouldVirtualize(visual)
    End Function

    Protected Overrides Sub DrawOnParent(dc As SpriteBatch, drawSize As Rectangle, effectedImage As Texture2D)
        MyBase.DrawOnParent(dc, drawSize, effectedImage)
    End Sub
End Class
