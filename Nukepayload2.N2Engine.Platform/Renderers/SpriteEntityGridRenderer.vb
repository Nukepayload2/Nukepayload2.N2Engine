Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(SpriteEntityGrid))>
Partial Friend Class SpriteEntityGridRenderer
    Inherits GameVisualContainerRenderer

    Sub New(panel As SpriteEntityGrid)
        MyBase.New(panel)
    End Sub
End Class