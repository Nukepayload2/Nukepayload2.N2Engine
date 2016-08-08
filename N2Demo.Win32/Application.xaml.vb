Class Application
    Private Sub Application_LoadCompleted(sender As Object, e As NavigationEventArgs) Handles Me.LoadCompleted
        Nukepayload2.N2Engine.Win32.MonoImplRegistration.Register()
    End Sub

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。

End Class
