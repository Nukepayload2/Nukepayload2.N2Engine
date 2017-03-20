Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class MonoGameRenderer
    Inherits RendererBase

    Sub New(view As GameVisual)
        MyBase.New(view)
    End Sub

    Friend MustOverride Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
    Friend Overridable Sub OnGameLoopStarting(sender As Game, args As Object)

    End Sub
    Friend Overridable Sub OnGameLoopStopped(sender As Game, args As Object)

    End Sub
    Friend Overridable Sub OnUpdate(sender As Game, args As MonogameUpdateEventArgs)
        Dim tim = args.Timing
        View.UpdateAction.Invoke(New UpdatingEventArgs(tim.ElapsedGameTime, tim.TotalGameTime, tim.IsRunningSlowly))
    End Sub
    Friend Overridable Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)

    End Sub
End Class
