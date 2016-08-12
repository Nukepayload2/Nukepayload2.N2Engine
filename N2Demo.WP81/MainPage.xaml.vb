' “空白应用程序”模板在 http://go.microsoft.com/fwlink/?LinkID=391641 上有介绍

Imports N2Demo.Core
Imports Nukepayload2.N2Engine.Wp81
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Dim sparks As New SparksView
    Dim sparksRenderer As GameCanvasRenderer
    Dim game As MonoGameHandler

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        game = MonoGame.Framework.XamlGame(Of MonoGameHandler).Create(String.Empty,
                                                                      Window.Current.CoreWindow,
                                                                      GamePanel)
        sparksRenderer = New GameCanvasRenderer(sparks, game)
    End Sub

    Private Sub MainPage_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles Me.Tapped
        Dim touchPoint = e.GetPosition(Me)
        sparks.OnTapped(New Numerics.Vector2(touchPoint.X, touchPoint.Y))
    End Sub

    Private Sub MainPage_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        game.Exit()
        game.Dispose()
    End Sub
End Class
