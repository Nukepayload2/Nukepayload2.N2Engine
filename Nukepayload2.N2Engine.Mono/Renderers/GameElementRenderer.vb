Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameElementRenderer(Of T As GameElement)
    Inherits RendererBase(Of T)

    Protected MustOverride Sub OnGameLoopStarting(sender As Game, args As Object)
    Protected MustOverride Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
    Protected MustOverride Sub OnUpdate(sender As Game, args As MonogameUpdateEventArgs)
    Protected MustOverride Sub OnGameLoopStopped(sender As Game, args As Object)
    Protected MustOverride Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)

End Class
