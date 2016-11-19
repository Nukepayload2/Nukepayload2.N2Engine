Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Linq
Imports Nukepayload2.N2Engine.UI.Elements
Imports RaisingStudio.Xna.Graphics

Public Class GameCanvasRenderer
    Inherits GameCanvasContainerRenderer

    Public WithEvents Game As MonoGameHandler
    Sub New(view As GameCanvas, game As MonoGameHandler)
        MyBase.New(view)
        Me.Game = game
        view.Renderer = Me
    End Sub

    Public Overrides Sub DisposeResources()

    End Sub

    Private Sub DoCanvasOperation(act As Action(Of MonoGameRenderer))
        For Each vie In View.HierarchyForEach(Function(node) TryCast(node, GameVisualContainer)?.Children)
            act(CType(vie.Renderer, MonoGameRenderer))
        Next
    End Sub

    Private Sub Game_GameLoopEnded(sender As Game, args As Object) Handles Game.GameLoopStopped
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStopped(sender, args))
        DirectCast(View, GameCanvas).Children.Clear()
    End Sub

    Private Sub Game_CreateResources(sender As Game, args As MonogameCreateResourcesEventArgs) Handles Game.CreateResources
        DoCanvasOperation(Sub(renderer) renderer.OnCreateResources(sender, args))
    End Sub

    Private Sub Game_Drawing(sender As Game, args As MonogameDrawEventArgs) Handles Game.Drawing
        Debug.WriteLine("开始刷新画面")
        OnDraw(sender, args)
        Debug.WriteLine("完成刷新画面")
    End Sub
    ''' <summary>
    ''' 在 backbuffer 绘制
    ''' </summary>
    Protected Overrides Sub CommitRenderTargetToParent(device As GraphicsDevice, dc As DrawingContext)
        Debug.WriteLine("设置绘制目标为：backbuffer")
        device.SetRenderTarget(Nothing)
        Debug.WriteLine("清屏: 白色")
        device.Clear(Color.White)
        dc.Begin()
        Dim loc = View.Location.Value
        Debug.WriteLine("绘制画布缓存纹理")
        dc.DrawTexture(RenderTarget, New Rectangle(loc.X, loc.Y, RenderTarget.Width, RenderTarget.Height), Color.White)
        dc.End()
    End Sub

    Private Sub Game_GameLoopStarting(sender As Game, args As Object) Handles Game.GameLoopStarting
        DoCanvasOperation(Sub(renderer) renderer.OnGameLoopStarting(sender, args))
    End Sub

    Private Sub Game_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles Game.Updating
        DoCanvasOperation(Sub(renderer) renderer.OnUpdate(sender, args))
    End Sub
End Class