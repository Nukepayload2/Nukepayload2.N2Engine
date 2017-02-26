Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports N2Demo.Core
Imports Nukepayload2.N2Engine.DesktopGL
Imports Gtk

Public Module Program
    WithEvents gameHandler As MonoGameHandler

    Dim sparks As MainCanvas
    Dim sparksRenderer As GameCanvasRenderer

    Sub Main(args$())
        '初始化 N2Engine 和 Monogame
        MonoImplRegistration.Register()
        RunGameAsync()
    End Sub

    Private Async Sub RunGameAsync()
        gameHandler = New MonoGameHandler With {
            .IsMouseVisible = True
        }
        sparks = New MainCanvas
        Await sparks.LoadSceneAsync
        sparksRenderer = New GameCanvasRenderer(sparks, gameHandler)

        'GTK
        Call New Threading.Thread(Sub()
                                      Application.Init()
                                      Dim wnd As New Window("游戏控制台")
                                      wnd.Add(New Button() With {.Label = "这绝对是gtk窗口，不信看这个按钮", .WidthRequest = 300, .HeightRequest = 120, .Visible = True})
                                      wnd.Show()
                                      Application.Run()
                                  End Sub).Start()
        '运行游戏
        gameHandler.Run()
    End Sub

    Private Sub gameHandler_Exiting(sender As Object, e As EventArgs) Handles gameHandler.Exiting
        End
    End Sub

End Module
