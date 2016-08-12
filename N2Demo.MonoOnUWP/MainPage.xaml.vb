'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Input.Touch
Imports N2Demo.Core
Imports Nukepayload2.N2Engine.MonoOnUWP
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Dim sparks As New SparksView
    Dim sparksRenderer As GameCanvasRenderer
    WithEvents game As MonoGameHandler

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        game = MonoGame.Framework.XamlGame(Of MonoGameHandler).Create(String.Empty,
                                                                      Window.Current.CoreWindow,
                                                                      GamePanel)
        game.IsMouseVisible = True
        sparksRenderer = New GameCanvasRenderer(sparks, game)
    End Sub

    Private Sub MainPage_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        game.Exit()
        game.Dispose()
    End Sub

    Private Sub Game_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles game.Updating
        Dim mouseState = Mouse.GetState(game.Window)
        Dim touchState = TouchPanel.GetState
        Dim touchPoint As New Numerics.Vector2?
        For Each t In touchState
            If t.State = TouchLocationState.Pressed Then
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
End Class