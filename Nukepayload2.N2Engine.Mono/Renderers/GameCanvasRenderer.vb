Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Linq
Imports Nukepayload2.N2Engine.UI.Elements

Public Class GameCanvasRenderer
    Inherits GameVisualContainerRenderer

    Public WithEvents Game As MonoGameHandler
    Sub New(view As GameCanvas, game As MonoGameHandler)
        MyBase.New(view)
        Me.Game = game
        view.Renderer = Me
    End Sub

    Public Overrides Sub DisposeResources()
        DirectCast(View, GameCanvas).Children.Clear()
    End Sub

    Private Sub DoCanvasOperation(act As Action(Of MonoGameRenderer))
        For Each vie In View.HierarchyForEach(Function(node) TryCast(node, GameVisualContainer)?.Children)
            act(DirectCast(vie.Renderer, MonoGameRenderer))
        Next
    End Sub

    Private Sub Game_GameLoopEnded(sender As Game, args As Object) Handles Game.GameLoopStopped
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStopped(sender, args))
    End Sub

    Private Sub Game_CreateResources(sender As Game, args As MonogameCreateResourcesEventArgs) Handles Game.CreateResources
        DoCanvasOperation(Sub(renderer) renderer.OnCreateResources(sender, args))
    End Sub

    Private Sub Game_Drawing(sender As Game, args As MonogameDrawEventArgs) Handles Game.Drawing
        OnDraw(sender, args)
    End Sub
    ''' <summary>
    ''' 在 backbuffer 绘制
    ''' </summary>
    Protected Overrides Sub CommitRenderTargetToParent(device As GraphicsDevice, dc As SpriteBatch)
        device.SetRenderTarget(Nothing)
        device.Clear(Color.White)
        dc.Begin()
        Dim loc = View.Location.Value
        dc.Draw(RenderTarget, New Rectangle(loc.X, loc.Y, RenderTarget.Width, RenderTarget.Height), Color.White)
        dc.End()
    End Sub

    Private Sub Game_GameLoopStarting(sender As Game, args As Object) Handles Game.GameLoopStarting
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStarting(sender, args))
    End Sub

    Private Sub Game_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles Game.Updating
        Dim isFrozen = View.IsFrozen
        If isFrozen.CanRead AndAlso isFrozen.Value Then
            Return
        End If
        DoCanvasOperation(Sub(renderer)
                              Dim frz = renderer.View.IsFrozen
                              If frz.CanRead AndAlso frz.Value Then
                                  Return
                              End If
                              renderer.OnUpdate(sender, args)
                          End Sub)
    End Sub
End Class