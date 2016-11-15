Imports System.Collections.Specialized
Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.Linq

Public Class GameCanvasRenderer
    Inherits RendererBase(Of GameCanvas)

    Public WithEvents Game As MonoGameHandler
    Sub New(view As GameCanvas, game As MonoGameHandler)
        MyBase.New(view)
        Me.Game = game
    End Sub

    Public Overrides Sub DisposeResources()

    End Sub

    Private Sub DoCanvasOperation(act As Action(Of MonoGameRenderer(Of GameVisual)))
        For Each vie In DirectCast(View, GameVisual).HierarchyForEach(Function(n) TryCast(n, GameVisualContainer)?.Children).Skip(1).Reverse
            Dim renderer = vie.Renderer
            act(CType(renderer, MonoGameRenderer(Of GameVisual)))
        Next
    End Sub

    Private Sub Game_GameLoopEnded(sender As Game, args As Object) Handles Game.GameLoopStopped
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStopped(sender, args))
        View.Clear()
    End Sub

    Private Sub Game_CreateResources(sender As Game, args As MonogameCreateResourcesEventArgs) Handles Game.CreateResources
        DoCanvasOperation(Sub(renderer) renderer.OnCreateResources(sender, args))
    End Sub

    Private Sub Game_Drawing(sender As Game, args As MonogameDrawEventArgs) Handles Game.Drawing
        DoCanvasOperation(Sub(renderer) renderer.OnDraw(sender, args))
    End Sub

    Private Sub Game_GameLoopStarting(sender As Game, args As Object) Handles Game.GameLoopStarting
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStarting(sender, args))
    End Sub

    Private Sub Game_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles Game.Updating
        DoCanvasOperation(Sub(renderer) renderer.OnUpdate(sender, args))
    End Sub
End Class