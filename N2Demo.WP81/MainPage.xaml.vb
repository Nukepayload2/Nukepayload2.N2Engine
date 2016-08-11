' “空白应用程序”模板在 http://go.microsoft.com/fwlink/?LinkID=391641 上有介绍

Imports N2Demo.Core
Imports Nukepayload2.N2Engine.Wp81
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    ''' <summary>
    ''' 在此页将要在 Frame 中显示时进行调用。
    ''' </summary>
    ''' <param name="e">描述如何访问此页的事件数据。
    ''' 此参数通常用于配置页。</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)
        ' TODO: 准备此处显示的页面。

        ' TODO: 如果您的应用程序包含多个页面，请确保
        ' 通过注册以下事件来处理硬件“后退”按钮:
        ' Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
        ' 如果使用由某些模板提供的 NavigationHelper，
        ' 则系统会为您处理该事件。
    End Sub

    Dim sparks As New SparksView
    Dim sparksRenderer As GameCanvasRenderer
    Dim game As New MonoGameHandler

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        sparksRenderer = New GameCanvasRenderer(sparks, game)
        LayoutRoot.Children.Add(game.SwapChainPanel)
    End Sub

    Private Sub MainPage_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles Me.Tapped
        Dim touchPoint = e.GetPosition(Me)
        sparks.OnTapped(New Numerics.Vector2(touchPoint.X, touchPoint.Y))
    End Sub

    Private Sub MainPage_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        VisualTreeHelper.DisconnectChildrenRecursive(game.SwapChainPanel)
        game.Exit()
        game.Dispose()
    End Sub
End Class
