Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports N2Demo.Core
Imports Nukepayload2.N2Engine.DesktopGL
Imports Gtk

Public Module Program
    WithEvents gameHandler As MonoGameHandler

    Dim sparks As SparksView
    Dim sparksRenderer As GameCanvasRenderer

    Sub Main(args$())
        '初始化 N2Engine 和 Monogame
        MonoImplRegistration.Register()
        gameHandler = New MonoGameHandler
        gameHandler.IsMouseVisible = True
        sparks = New SparksView
        sparksRenderer = New GameCanvasRenderer(sparks, gameHandler)

        Application.Init()
        Dim wnd As New Window("游戏控制台")

        wnd.Add(New Button() With {.Label = "这绝对是gtk窗口，不信看这个按钮", .WidthRequest = 300, .HeightRequest = 120, .Visible = True})
        wnd.Show()

        gameHandler.Run()
    End Sub
    Private Sub GameHandler_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles gameHandler.Updating
        Dim mouseState = Mouse.GetState(sender.Window)
        Dim touchState = Touch.TouchPanel.GetState
        Dim touchPoint As New Numerics.Vector2?
        For Each t In touchState
            If t.State = Touch.TouchLocationState.Pressed Then
                touchPoint = New Numerics.Vector2(t.Position.X, t.Position.Y)
                Exit For
            End If
        Next
        If Not touchPoint.HasValue AndAlso mouseState.LeftButton = ButtonState.Pressed Then
            touchPoint = New Numerics.Vector2(mouseState.Position.X, mouseState.Position.Y)
        End If
        If touchPoint.HasValue Then
            sparks.OnTapped(touchPoint.Value)
        End If
    End Sub
End Module
