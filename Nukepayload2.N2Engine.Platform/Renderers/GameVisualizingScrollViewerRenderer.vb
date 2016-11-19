Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(GameVisualizingScrollViewer))>
Partial Friend Class GameVisualizingScrollViewerRenderer
    Inherits GameCanvasContainerRenderer

    Sub New(panel As GameVisualizingScrollViewer)
        MyBase.New(panel)
    End Sub
End Class
