Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(GameVisualizingScrollViewer))>
Friend Class GameVisualizingScrollViewerRenderer
    Inherits GameVisualContainerRenderer

    Sub New(panel As GameVisualizingScrollViewer)
        MyBase.New(panel)
    End Sub
End Class
