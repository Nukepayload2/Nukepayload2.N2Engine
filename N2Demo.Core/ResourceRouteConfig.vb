Imports Nukepayload2.N2Engine.Resources
Imports System.Reflection

Module ResourceRouteConfig
    ''' <summary>
    ''' 注册此程序集嵌入的资源
    ''' </summary>
    Sub ApplyRoute()
        Dim resldr = ResourceLoader.GetForCurrentView
        resldr.AddRoute("Images", GetType(ResourceRouteConfig).GetTypeInfo.Assembly)
    End Sub
End Module