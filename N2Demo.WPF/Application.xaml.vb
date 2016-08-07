Class Application
    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        ' 注册 Mono 平台上的实现
        Nukepayload2.N2Engine.Win32.MonoImplRegistration.Register()
    End Sub

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。

End Class
