Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameElementRenderer(Of T As GameElement)
    Inherits RendererBase(Of T)

    Protected MustOverride Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
    Protected Overridable Sub OnGameLoopStarting(sender As Game, args As Object)

    End Sub
    Protected Overridable Sub OnGameLoopStopped(sender As Game, args As Object)

    End Sub
    Protected Overridable Sub OnUpdate(sender As Game, args As MonogameUpdateEventArgs)
        View.UpdateCommand.Execute()
    End Sub
    Protected Overridable Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)

    End Sub
End Class
