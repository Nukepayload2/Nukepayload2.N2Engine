Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Linq
Imports Nukepayload2.N2Engine.UI.Elements

Public Class GameCanvasRenderer
    Inherits GameVisualContainerRenderer

    Public WithEvents Game As MonoGameHandler
    Dim _eventMediator As MonoGameEventMediator

    Sub New(view As GameCanvas, game As MonoGameHandler)
        MyBase.New(view)
        Me.Game = game
        view.Renderer = Me
        _eventMediator = New MonoGameEventMediator(view)
    End Sub

    Public Overrides Sub DisposeResources()
        DirectCast(View, GameCanvas).Children.Clear()
    End Sub

    Private Sub DoCanvasOperation(act As Action(Of MonoGameRenderer))
        For Each vie In View.HierarchyWalk(Function(node) node.GetSubNodes)
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
        If View.Transform Is Nothing Then
            dc.Begin()
        Else
            With View.Transform.GetTransformMatrix
                dc.Begin(SpriteSortMode.Deferred, Nothing, Nothing, Nothing, Nothing, Nothing,
                         New Matrix(.M11, .M12, 0F, 0F,
                                    .M21, .M22, 0F, 0F,
                                    0F, 0F, 1.0F, 0F,
                                    .M31, .M32, 0F, 1.0F))
            End With
        End If
        Dim loc = View.Location.Value
        Dim winSize = Information.BackBufferInformation.ScreenSize
        Dim viewSize = Information.BackBufferInformation.ViewPortSize
        dc.Draw(RenderTarget, New Rectangle(loc.X, loc.Y, winSize.Width, winSize.Height), New Rectangle(0, 0, viewSize.Width, viewSize.Height), Color.White)
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
        _eventMediator?.TryRaiseEvent()
        DoCanvasOperation(Sub(renderer)
                              Dim frz = renderer.View.IsFrozen
                              If frz.CanRead AndAlso frz.Value Then
                                  Return
                              End If
                              renderer.OnUpdate(sender, args)
                          End Sub)
    End Sub
End Class