Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Public Class GameCanvasRenderer
    Inherits GameCanvasContainerRenderer

    Public WithEvents Game As MonoGameHandler
    Sub New(view As GameCanvas, game As MonoGameHandler)
        MyBase.New(view)
        Me.Game = game
    End Sub

    Public Overrides Sub DisposeResources()

    End Sub

    Private Sub DoCanvasOperation(act As Action(Of MonoGameRenderer))
        Dim view = DirectCast(Me.View, GameCanvas)
        For Each vie In view.Children
            Dim renderer = vie.Renderer
            act(CType(renderer, MonoGameRenderer))
        Next
    End Sub

    Private Sub Game_GameLoopEnded(sender As Game, args As Object) Handles Game.GameLoopStopped
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStopped(sender, args))
        OnGameLoopStopped(sender, args)
        DirectCast(View, GameCanvas).Clear()
    End Sub

    Private Sub Game_CreateResources(sender As Game, args As MonogameCreateResourcesEventArgs) Handles Game.CreateResources
        DoCanvasOperation(Sub(renderer) renderer.OnCreateResources(sender, args))
        OnCreateResources(sender, args)
    End Sub

    Private Sub Game_Drawing(sender As Game, args As MonogameDrawEventArgs) Handles Game.Drawing
        DoCanvasOperation(Sub(renderer) renderer.OnDraw(sender, args))
        OnDraw(sender, args)
    End Sub

    Private Sub Game_GameLoopStarting(sender As Game, args As Object) Handles Game.GameLoopStarting
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStarting(sender, args))
        OnGameLoopStarting(sender, args)
    End Sub

    Private Sub Game_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles Game.Updating
        DoCanvasOperation(Sub(renderer) renderer.OnUpdate(sender, args))
        OnUpdate(sender, args)
    End Sub
End Class