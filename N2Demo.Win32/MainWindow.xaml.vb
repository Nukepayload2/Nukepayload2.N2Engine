Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports N2Demo.Core
Imports Nukepayload2.N2Engine.Win32

Class MainWindow
    WithEvents gameHandler As MonoGameHandler
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        MonoImplRegistration.Register()
        gameHandler = New MonoGameHandler(Sub(ctl) winformHost.Child = ctl, Sub()
                                                                                Focus()
                                                                                Width += 1
                                                                            End Sub)
        gameHandler.IsMouseVisible = True
        sparks = New SparksView
        sparksRenderer = New GameCanvasRenderer(sparks, gameHandler)
        gameHandler.Run()
    End Sub

    Private Sub MainWindow_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        gameHandler.Exit()
        gameHandler.Dispose()
        End
    End Sub

    Dim sparks As SparksView
    Dim sparksRenderer As GameCanvasRenderer

    Private Sub BtnQuickSave_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnQuickLoad_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnBreakSave_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs)
        Close()
    End Sub

    Private Sub Rectangle_PreviewMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub

    Private Sub gameHandler_Updating(sender As Game, args As MonogameUpdateEventArgs) Handles gameHandler.Updating
        Dim state = Mouse.GetState(sender.Window)
        If state.LeftButton = ButtonState.Pressed Then
            sparks.OnTapped(New Numerics.Vector2(state.Position.X, state.Position.Y))
        End If
    End Sub
End Class