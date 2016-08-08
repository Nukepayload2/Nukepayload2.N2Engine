Imports Nukepayload2.N2Engine.Win32

Class MainWindow
    Dim gameWindow As New Window
    Dim gameHandler As MonoGameHandler
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        gameWindow.Show()
        Dim interop As New Interop.WindowInteropHelper(gameWindow)
        gameHandler = New MonoGameHandler(interop.Handle)
        gameWindow.SetBinding(TitleProperty, New Binding(NameOf(Title)))

    End Sub
End Class
