'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

Imports System.Numerics
Imports N2Demo.Core
Imports Nukepayload2.N2Engine.UWP
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Dim sparks As New SparksView
    Dim sparksRenderer As GameCanvasRenderer

    Private Async Sub MainPage_LoadedAsync(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Await sparks.LoadSceneAsync
        sparksRenderer = New GameCanvasRenderer(sparks, animContent)
    End Sub

    Private Sub MainPage_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        sparksRenderer?.Dispose()
        animContent.RemoveFromVisualTree()
        animContent = Nothing
    End Sub
End Class