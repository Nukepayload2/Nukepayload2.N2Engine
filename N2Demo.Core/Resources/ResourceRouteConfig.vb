Imports Nukepayload2.N2Engine.Resources
Imports System.Reflection
Imports Nukepayload2.N2Engine.Platform

Module ResourceRouteConfig
    Dim routeAdded As Boolean
    ''' <summary>
    ''' 注册此程序集嵌入的资源
    ''' </summary>
    Sub ApplyRoute()
        If Not routeAdded Then
            Dim resldr = ResourceLoader.GetForCurrentView
            resldr.AddRoute("Images", GetType(ResourceRouteConfig).GetTypeInfo.Assembly)
            Const ProgramDir = "ProgramDirectory"
            resldr.AddRoute(ProgramDir, Platforms.WindowsDesktop Or Platforms.DesktopGL Or Platforms.iOS, "")
            resldr.AddRoute(ProgramDir, Platforms.Android, "android.resource://com.nukepayload2.n2demo/raw")
            resldr.AddUriPathMapping(ProgramDir, Platforms.Android,
                                     Function(path)
                                         ' Android 中使用包内部的音频文件要直接放到 raw 文件夹里面，保持使用小写，并且不支持 wma 格式的声音。
                                         Dim fn = path.Substring(path.LastIndexOf("/") + 1)
                                         If fn.EndsWith(".wma") Then
                                             fn = fn.Substring(0, fn.Length - 4) + ".ogg"
                                             Return "/" & ResourceLoader.GetAndroidRawResource(fn.ToLower)
                                         Else
                                             Return path
                                         End If
                                     End Function)
            resldr.AddRoute(ProgramDir, Platforms.UniversalWindows Or Platforms.WindowsRT81 Or Platforms.WindowsPhone81, "ms-appx://")
            routeAdded = True
        End If
    End Sub
End Module