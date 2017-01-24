Imports Nukepayload2.N2Engine.Resources
Imports System.Reflection
Imports Nukepayload2.N2Engine.Platform

Module ResourceRouteConfig
    ''' <summary>
    ''' 注册此程序集嵌入的资源
    ''' </summary>
    Sub ApplyRoute()
        Dim resldr = ResourceLoader.GetForCurrentView
        resldr.AddRoute("Images", GetType(ResourceRouteConfig).GetTypeInfo.Assembly)
        resldr.AddRoute("ProgramDirectory", Platforms.WindowsDesktop Or Platforms.DesktopGL Or Platforms.Android Or Platforms.iOS, "")
        resldr.AddRoute("ProgramDirectory", Platforms.UniversalWindows Or Platforms.WindowsRT81 Or Platforms.WindowsPhone81, "ms-appx://")
    End Sub
End Module