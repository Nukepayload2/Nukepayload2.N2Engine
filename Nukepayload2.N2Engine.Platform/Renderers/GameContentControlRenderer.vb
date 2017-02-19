Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Controls

<PlatformImpl(GetType(IGameContentControlRenderer))>
Partial Friend Class GameContentControlRenderer
    Inherits GameElementRenderer

    Sub New(visual As GameControl)
        MyBase.New(visual)
    End Sub

End Class
