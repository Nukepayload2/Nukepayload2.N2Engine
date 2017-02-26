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

    Dim sparks As New MainCanvas
    Dim sparksRenderer As GameCanvasRenderer
    WithEvents game As MonoGameHandler

    Private Async Sub MainPage_LoadedAsync(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        game = MonoGame.Framework.XamlGame(Of MonoGameHandler).Create(String.Empty,
                                                                      Window.Current.CoreWindow,
                                                                      GamePanel)
        game.IsMouseVisible = True
        Await sparks.LoadSceneAsync
        sparksRenderer = New GameCanvasRenderer(sparks, game)
    End Sub

    Private Sub MainPage_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        game.Exit()
        game.Dispose()
    End Sub

End Class