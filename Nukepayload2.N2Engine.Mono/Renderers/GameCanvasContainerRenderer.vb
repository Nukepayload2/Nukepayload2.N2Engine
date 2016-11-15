Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameCanvasContainerRenderer(Of T As GameVisualContainer)
    Inherits MonoGameRenderer(Of T)
    Sub New(view As T)
        MyBase.New(view)
    End Sub
    Public Overrides Sub DisposeResources()

    End Sub
End Class