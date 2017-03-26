Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(IGameContentControlRenderer))>
Partial Friend Class GameContentControlRenderer
    Inherits GameElementRenderer
    Implements IGameContentControlRenderer

    Sub New(visual As GameElement)
        MyBase.New(visual)
    End Sub

End Class
