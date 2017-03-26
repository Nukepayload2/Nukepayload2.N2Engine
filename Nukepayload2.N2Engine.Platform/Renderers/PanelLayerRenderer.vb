Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(PanelLayer))>
Friend Class PanelLayerRenderer
    Inherits GameVisualContainerRenderer

    Sub New(panel As PanelLayer)
        MyBase.New(panel)
    End Sub
End Class
