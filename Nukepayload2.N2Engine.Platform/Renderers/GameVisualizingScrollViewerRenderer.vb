Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(GameVirtualizingScrollViewer))>
Partial Friend Class GameVirtualizingScrollViewerRenderer
    Inherits GameVisualContainerRenderer

    Sub New(panel As GameVirtualizingScrollViewer)
        MyBase.New(panel)
    End Sub
End Class
