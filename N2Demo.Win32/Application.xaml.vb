Imports System.Reflection

Class Application

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnAssemblyResolve
    End Sub

    Private Function OnAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Dim asmName As New AssemblyName(args.Name)
        Dim curDir = IO.Path.GetDirectoryName(GetType(MainWindow).Assembly.Location)
        Dim sharpDxDir = IO.Path.Combine(curDir, "SharpDX263")
        Dim reqDll = IO.Path.Combine(sharpDxDir, asmName.Name) & ".dll"
        If IO.File.Exists(reqDll) Then
            Return Assembly.LoadFile(reqDll)
        End If
        Return Nothing
    End Function
End Class
