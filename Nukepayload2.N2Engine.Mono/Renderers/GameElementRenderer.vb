Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameElementRenderer(Of T As GameElement)
    Inherits MonoGameRenderer(Of T)

    Public Overrides Sub DisposeResources()

    End Sub
End Class
