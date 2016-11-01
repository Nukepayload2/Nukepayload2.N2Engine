Imports Nukepayload2.N2Engine.Resources
Imports System.Reflection

Module ResourceRouteConfig
    Sub ApplyRoute()
        Dim resldr = ResourceLoader.GetForCurrentView
        resldr.AddRoute("Images", GetType(ResourceRouteConfig).GetTypeInfo.Assembly)
    End Sub
End Module