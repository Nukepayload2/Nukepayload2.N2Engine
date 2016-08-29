Imports System.Reflection
Imports System.Resources
Namespace Resources
    ''' <summary>
    ''' 处理资源加载或键映射
    ''' </summary>
    Public Class ResourceLoader
        ''' <summary>
        ''' 在特定平台按照 "内容" 生成的资源。
        ''' </summary>
        Public Const PlatformContentScheme = "n2-res"
        ''' <summary>
        ''' 在某个程序集按照嵌入的资源生成的
        ''' </summary>
        Public Const EmbeddedScheme = "n2-res-emb"
        ''' <summary>
        ''' 在某个程序集按照资源字典存储的字符串
        ''' </summary>
        Public Const StringScheme = "n2-res-str"
        ''' <summary>
        ''' 自定义的资源
        ''' </summary>
        Public Const CustomScheme = "n2-res-cus"
        ''' <summary>
        ''' 从n2引擎前缀映射到平台前缀
        ''' </summary>
        Dim prefixRoutes As New Dictionary(Of String, String)
        ''' <summary>
        ''' 从n2引擎前缀映射到存放资源的程序集
        ''' </summary>
        Dim assemblyRoutes As New Dictionary(Of String, Assembly)
        ''' <summary>
        ''' 从n2引擎前缀映射到存放资源的程序集
        ''' </summary>
        Dim strMgrRoutes As New Dictionary(Of String, ResourceManager)
        ''' <summary>
        ''' 从自定义字符串映射到自定义加载委托
        ''' </summary>
        Dim customRoutes As New Dictionary(Of String, Func(Of String, Object))
        Sub New()

        End Sub
        Public Property UICulture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentUICulture
        ''' <summary>
        ''' 添加嵌入的资源加载Uri路由。示例：资源包名是StringPack, 字符串资源加载器是 resMgr, 当前语言标识为 CurrentCulture，那么映射是：n2-res-str:///StringPack/Welcome -> resMgr.GetString("Welcome", UICulture)
        ''' </summary>
        ''' <param name="resPackName">指定资源包的名字</param>
        Public Sub AddRoute(resPackName As String, resMgr As ResourceManager)
            If strMgrRoutes.ContainsKey(resPackName) Then
                Throw New InvalidOperationException($"前缀{resPackName}已经被注册过了")
            End If
            strMgrRoutes.Add(resPackName, resMgr)
        End Sub
        ''' <summary>
        ''' 添加嵌入的资源加载Uri路由。示例：资源包名是Images, 程序集是N2Demo.Core，那么映射是：n2-res-emb:///Images/GameLogo.png -> assembly.GetManifestResourceStream("N2Demo.Core.GameLogo.png")
        ''' </summary>
        ''' <param name="resPackName">指定资源包的名字</param>
        ''' <param name="assembly">资源所在程序集</param>
        Public Sub AddRoute(resPackName As String, assembly As Assembly)
            If assemblyRoutes.ContainsKey(resPackName) Then
                Throw New InvalidOperationException($"前缀{resPackName}已经被注册过了")
            End If
            assemblyRoutes.Add(resPackName, assembly)
        End Sub
        ''' <summary>
        ''' 添加内容资源加载Uri路由。示例：应用名是UWPApp, 平台特定的资源前缀是 ms-appx://，那么映射是：n2-res:///UWPApp/Assets/StoreLogo.png -> ms-appx:///Assets/StoreLogo.png
        ''' </summary>
        ''' <param name="appName">指定平台目标应用的名称</param>
        ''' <param name="platformPrefix">平台资源前缀</param>
        Public Sub AddRoute(appName As String, platformPrefix As String)
            If prefixRoutes.ContainsKey(appName) Then
                Throw New InvalidOperationException($"前缀{appName}已经被注册过了")
            End If
            prefixRoutes.Add(appName, platformPrefix)
        End Sub
        ''' <summary>
        ''' 添加自定义资源加载动作的路由。示例：资源名是platformName, 那么映射是 n2-res-cus:///platformName -> load("platformName")
        ''' </summary>
        ''' <param name="resKey">指定平台目标应用的名称</param>
        Public Sub AddRoute(resKey As String, load As Func(Of String, Object))
            If customRoutes.ContainsKey(resKey) Then
                Throw New InvalidOperationException("这个前缀已经被注册过了")
            End If
            customRoutes.Add(resKey, load)
        End Sub
        ''' <summary>
        ''' 从Uri解析出相应的资源或方案映射。如果是平台内容，则返回相应的映射字符串。如果是嵌入资源，则打开资源流。如果是自定义资源，则返回注册的资源加载器的执行结果。
        ''' </summary>
        Public Function GetResource(resourceKey As Uri) As Object
            Dim scheme = resourceKey.Scheme
            Dim path = resourceKey.AbsolutePath
            Try
                Select Case scheme
                    Case PlatformContentScheme
                        Return GetPlatformContentKey(path)
                    Case EmbeddedScheme
                        Return GetEmbeddedResource(path)
                    Case CustomScheme
                        Return GetCustomResource(path)
                    Case StringScheme
                        Return GetString(path)
                    Case Else
                        Throw New ArgumentException($"未识别的方案{scheme}")
                End Select
            Catch ex As ArgumentException
                Throw New ArgumentException("无法检索相应的资源, 由于提供了错误的Uri。", NameOf(resourceKey), ex)
            End Try
        End Function

        Public Function GetString(path As String) As String
            Dim paths = path.Split({"/"c}, StringSplitOptions.RemoveEmptyEntries)
            If path.Length <> 2 Then
                Throw New ArgumentException("提供了无效的路径", NameOf(path))
            End If
            Dim resMgr = strMgrRoutes(paths(0))
            Return resMgr.GetString(path, UICulture)
        End Function

        ''' <summary>
        ''' 从已解析的 Uri 的 Path(以 / 开始) 获取自定义资源
        ''' </summary>
        Public Function GetCustomResource(path As String) As Object
            If path.StartsWith("/") Then
                Dim pathRight = path.Length - 1
                Do While path.Chars(pathRight) = "/"c
                    pathRight -= 1
                Loop
                If pathRight > 0 Then
                    Dim clearPath = path.Substring(1, pathRight)
                    If clearPath.Length > 0 AndAlso customRoutes.ContainsKey(clearPath) Then
                        Return customRoutes(clearPath).Invoke(clearPath)
                    End If
                End If
            End If
            Throw New ArgumentException($"Uri表示的自定义资源不存在", NameOf(path))
        End Function
        ''' <summary>
        ''' 从已解析的 Uri 的 Path(以 / 开始) 获取平台内容资源键
        ''' </summary>
        Public Function GetPlatformContentKey(path As String) As String
            Dim second As Integer = 0
            Dim platformName = GetUriPathRoot(path, second)
            If Not String.IsNullOrEmpty(platformName) AndAlso prefixRoutes.ContainsKey(platformName) Then
                Dim routePrefix = prefixRoutes(platformName)
                Return routePrefix + path.Substring(second)
            End If
            Throw New ArgumentException($"Uri平台名称未映射", NameOf(path))
        End Function
        ''' <summary>
        ''' 从已经解析的 Uri 的 Path(以 / 开始) 读取嵌入资源
        ''' </summary>
        Public Function GetEmbeddedResource(path As String) As Stream
            Dim second As Integer = 0
            If path.EndsWith("/") Then
                Throw New ArgumentException("结尾不应出现 '/'", NameOf(path))
            End If
            If path.Contains("\") Then
                Throw New ArgumentException("代表嵌入资源的Uri不应该出现 '\'", NameOf(path))
            End If
            If path.LastIndexOf("/") > 0 Then
                '带文件夹名字的嵌入资源
                Dim asmName = GetUriPathRoot(path, second)
                If Not String.IsNullOrEmpty(asmName) AndAlso assemblyRoutes.ContainsKey(asmName) Then
                    Dim asm = assemblyRoutes(asmName)
                    Dim asmFullName = asm.FullName
                    Dim resKey = asmFullName
                    Return asm.GetManifestResourceStream(resKey)
                End If
            Else
                Throw New ArgumentException("这个Uri不包含资源包的名称", NameOf(path))
            End If
            Throw New ArgumentException($"Uri表示的嵌入资源不存在", NameOf(path))
        End Function

        ''' <summary>
        ''' 从Uri中包含的路径提取根，并且按引用返回根的结尾后的/索引。
        ''' </summary>
        Private Shared Function GetUriPathRoot(path As String, ByRef second As Integer) As String
            If path.StartsWith("/") Then
                second = path.IndexOf("/"c, 1)
                If second > 0 Then
                    Return path.Substring(1, second - 1)
                End If
            End If
            Return Nothing
        End Function
    End Class
End Namespace