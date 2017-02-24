Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Controls
Imports Nukepayload2.N2Engine.UI.Elements

<PlatformImpl(GetType(IGameTemplatedContentRenderer))>
Friend Class GameTemplatedContentRenderer
    Inherits GameVisualContainerRenderer
    Implements IGameTemplatedContentRenderer

    Public Sub New(view As GameVisualContainer)
        MyBase.New(view)
    End Sub
End Class