Imports System.Reflection
Imports System.Resources
Imports System.Threading

#Const STA_MODE = True

Namespace Resources
    ''' <summary>
    ''' 处理资源加载或资源键映射
    ''' </summary>
    Public Class ResourceLoader
#If STA_MODE Then
        Shared instance As ResourceLoader
#Else
        ''' <summary>
        ''' 对于不能跨线程共享的资源提供同步
        ''' </summary>
        Dim _syncContext As SynchronizationContext

        Shared instances As New Dictionary(Of SynchronizationContext, ResourceLoader)
#End If
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
        ''' 从n2引擎前缀映射到平台前缀。这是平台相关的。
        ''' </summary>
        Dim prefixRoutes As New Dictionary(Of String, Dictionary(Of Platform.Platforms, String))
        ''' <summary>
        ''' 从n2引擎Uri路径映射到平台Uri路径。这是平台相关的。
        ''' </summary>
        Dim uriPathMappings As New Dictionary(Of String, Dictionary(Of Platform.Platforms, Func(Of String, String)))
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
        ''' <summary>
        ''' 专门用来注册安卓系统的raw文件夹的资源，从文件名查询文件编号。这些资源是线程安全的。
        ''' </summary>
        Shared androidRawResources As New Dictionary(Of String, Integer)

#If STA_MODE Then
        Protected Sub New()
            instance = Me
        End Sub
#Else
        Protected Sub New(curSync As SynchronizationContext)
            _syncContext = curSync
            instances.Add(curSync, Me)
        End Sub
#End If

        ''' <summary>
        ''' 为当前线程获取一个资源加载器。在 UI 线程进行这个操作一定会成功。
        ''' </summary>
        ''' <exception cref="InvalidOperationException"/>
        Public Shared Function GetForCurrentView() As ResourceLoader
#If STA_MODE Then
            Return If(instance, New ResourceLoader)
#Else
            Dim curSync = SynchronizationContext.Current
            If curSync Is Nothing Then
                Throw New InvalidOperationException("当前线程并没有同步上下文。请确保在UI线程调用这个方法。")
            End If
            Return If(instances.ContainsKey(curSync), instances(curSync), New ResourceLoader(curSync))
#End If
        End Function

        Public Property UICulture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentUICulture
        ''' <summary>
        ''' 添加嵌入的资源加载Uri路由。示例：资源包名是StringPack, 字符串资源加载器是 resMgr, 当前语言标识为 CurrentCulture，那么映射是：n2-res-str:///StringPack/Welcome -> resMgr.GetString("Welcome", UICulture)
        ''' </summary>
        ''' <param name="resPackName">指定资源包的名字</param>
        Public Sub MapResourceManager(resPackName As String, resMgr As ResourceManager)
            If strMgrRoutes.ContainsKey(resPackName) Then
                Throw New InvalidOperationException($"前缀{resPackName}已经被注册过了")
            End If
            strMgrRoutes.Add(resPackName, resMgr)
        End Sub
        ''' <summary>
        ''' 添加安卓 Raw 文件夹下的资源文件名到文件编号的映射。这些资源是线程安全的。
        ''' </summary>
        ''' <param name="fileName">要映射的文件名。必须是小写的，否则文件加载不出来。</param>
        ''' <param name="fileId">文件 Id。使用 Resources.Raw.文件名 获得。</param>
        Public Shared Sub AddAndroidRawResourceMapping(fileName As String, fileId As Integer)
            If String.IsNullOrEmpty(fileName) Then
                Throw New ArgumentNullException(NameOf(fileName))
            End If
            If fileName.ToLower <> fileName Then
                Throw New ArgumentException("文件名必须是小写的")
            End If
            If Not androidRawResources.ContainsKey(fileName) Then
                androidRawResources.Add(fileName, fileId)
            Else
                Throw New InvalidOperationException($"资源文件 ""{fileName}"" 已经被注册过了")
            End If
        End Sub
        ''' <summary>
        ''' 检索安卓 Raw 文件夹下的资源的 Id。
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        Public Shared Function GetAndroidRawResource(fileName As String) As Integer
            Return androidRawResources(fileName)
        End Function
        ''' <summary>
        ''' 添加嵌入的资源加载Uri路由。示例：资源包名是Images, 程序集是N2Demo.Core，那么映射是：n2-res-emb:///Images/GameLogo.png -> assembly.GetManifestResourceStream("N2Demo.Core.GameLogo.png")
        ''' </summary>
        ''' <param name="resPackName">指定资源包的名字</param>
        ''' <param name="assembly">资源所在程序集</param>
        Public Sub AddEmbeddedResourceRoute(resPackName As String, assembly As Assembly)
            If assemblyRoutes.ContainsKey(resPackName) Then
                Throw New InvalidOperationException($"前缀{resPackName}已经被注册过了")
            End If
            assemblyRoutes.Add(resPackName, assembly)
        End Sub
        ''' <summary>
        ''' 添加平台相关的内容资源加载Uri的路径映射。
        ''' </summary>
        ''' <param name="appName">指定平台目标应用的名称</param>
        ''' <param name="platform">这个注册是针对哪些平台的。可以用按位或的方式针对多个平台。</param>
        ''' <param name="pathMapping">平台资源映射。输入: 资源 Uri 的绝对路径，输出：资源 Uri 绝对路径在平台上的映射。</param>
        Public Sub AddUriPathMapping(appName As String, platform As Platform.Platforms, pathMapping As Func(Of String, String))
            Dim values = [Enum].GetValues(GetType(Platform.Platforms))
            For Each plt As Platform.Platforms In values
                If plt > 0 AndAlso platform.HasFlag(plt) Then
                    If Not uriPathMappings.ContainsKey(appName) Then
                        uriPathMappings.Add(appName, New Dictionary(Of Platform.Platforms, Func(Of String, String)))
                    End If
                    Dim platformDic = uriPathMappings(appName)
                    If platformDic.ContainsKey(plt) Then
                        Throw New InvalidOperationException($"应用 {appName} 相关的路径映射已经被注册到 {plt} 平台。")
                    Else
                        platformDic.Add(plt, pathMapping)
                    End If
                End If
            Next
        End Sub
        ''' <summary>
        ''' 映射平台相关 Uri 前缀和路径的起始部分。示例：应用名是UWPApp, 平台特定的资源前缀是 ms-appx://，那么映射是：n2-res:///UWPApp/Assets/StoreLogo.png -> ms-appx:///Assets/StoreLogo.png
        ''' </summary>
        ''' <param name="appName">指定平台目标应用的名称</param>
        ''' <param name="platform">这个注册是针对哪些平台的。可以用按位或的方式针对多个平台。</param>
        ''' <param name="platformPrefix">平台资源前缀</param>
        Public Sub AddUriPrefixMapping(appName As String, platform As Platform.Platforms, platformPrefix As String)
            Dim values = [Enum].GetValues(GetType(Platform.Platforms))
            For Each plt As Platform.Platforms In values
                If plt > 0 AndAlso platform.HasFlag(plt) Then
                    If Not prefixRoutes.ContainsKey(appName) Then
                        prefixRoutes.Add(appName, New Dictionary(Of Platform.Platforms, String))
                    End If
                    Dim platformDic = prefixRoutes(appName)
                    If platformDic.ContainsKey(plt) Then
                        Throw New InvalidOperationException($"应用 {appName} 已经被注册到 {plt} 平台。")
                    Else
                        platformDic.Add(plt, platformPrefix)
                    End If
                End If
            Next
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
        ''' 从Uri解析出相应的动态资源或方案映射。如果是平台内容，则返回相应的映射字符串。如果是嵌入资源，则打开资源流。如果是自定义资源，则返回注册的资源加载器的执行结果。
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
        ''' <summary>
        ''' 通过 Uri 检索资源流
        ''' </summary>
        Public Function GetResourceEmbeddedStream(resourceKey As Uri) As Stream
            Dim scheme = resourceKey.Scheme
            Dim path = resourceKey.AbsolutePath
            Try
                Select Case scheme
                    Case EmbeddedScheme
                        Return GetEmbeddedResource(path)
                    Case Else
                        Throw New ArgumentException($"未识别的方案{scheme}")
                End Select
            Catch ex As ArgumentException
                Throw New ArgumentException("无法检索相应的资源, 由于提供了错误的Uri。", NameOf(resourceKey), ex)
            End Try
        End Function
        ''' <summary>
        ''' 通过 Uri 检索并执行获取自定义资源的动作
        ''' </summary>
        Public Function GetResourceObject(resourceKey As Uri) As Object
            Dim scheme = resourceKey.Scheme
            Dim path = resourceKey.AbsolutePath
            Try
                Select Case scheme
                    Case CustomScheme
                        Return GetCustomResource(path)
                    Case Else
                        Throw New ArgumentException($"未识别的方案{scheme}")
                End Select
            Catch ex As ArgumentException
                Throw New ArgumentException("无法检索相应的资源, 由于提供了错误的Uri。", NameOf(resourceKey), ex)
            End Try
        End Function
        ''' <summary>
        ''' 将 n2引擎的 Uri 转换为平台资源的 Uri。
        ''' </summary>
        Public Function GetResourceUri(resourceKey As Uri) As Uri
            Dim scheme = resourceKey.Scheme
            Dim path = resourceKey.AbsolutePath
            Try
                Select Case scheme
                    Case PlatformContentScheme
                        Return GetPlatformContentKey(path)
                    Case Else
                        Throw New ArgumentException($"未识别的方案{scheme}")
                End Select
            Catch ex As ArgumentException
                Throw New ArgumentException("无法检索相应的资源, 由于提供了错误的Uri。", NameOf(resourceKey), ex)
            End Try
        End Function
        ''' <summary>
        ''' 从 Uri 获取本地化字符串
        ''' </summary>
        Public Function GetResourceString(resourceKey As Uri) As String
            Dim scheme = resourceKey.Scheme
            Dim path = resourceKey.AbsolutePath
            Try
                Select Case scheme
                    Case StringScheme
                        Return GetString(path)
                    Case Else
                        Throw New ArgumentException($"未识别的方案{scheme}")
                End Select
            Catch ex As ArgumentException
                Throw New ArgumentException("无法检索相应的资源, 由于提供了错误的Uri。", NameOf(resourceKey), ex)
            End Try
        End Function
        ''' <summary>
        ''' 从已解析的 Uri 的 Path(以 / 开始) 获取本地化字符串
        ''' </summary>
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
        Public Function GetPlatformContentKey(path As String) As Uri
            Dim second As Integer = 0
            Dim platformName = GetUriPathRoot(path, second)
            If Not String.IsNullOrEmpty(platformName) AndAlso prefixRoutes.ContainsKey(platformName) Then
                Dim routePrefix = prefixRoutes(platformName)
                Dim primaryPlatform = Platform.PlatformImplRegistration.GetRegisteredPlatforms.First
                Dim platformPart = routePrefix(primaryPlatform)
                Dim relatedPath = path.Substring(second)
                If uriPathMappings.ContainsKey(platformName) Then
                    Dim appUriPathMap = uriPathMappings(platformName)
                    If appUriPathMap.ContainsKey(primaryPlatform) Then
                        relatedPath = appUriPathMap(primaryPlatform)(relatedPath)
                    End If
                End If
                If String.IsNullOrEmpty(platformPart) Then
                    platformPart = "n2-file://"
                End If
                Return New Uri(platformPart + relatedPath)
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
                Dim asmID = GetUriPathRoot(path, second)
                If Not String.IsNullOrEmpty(asmID) AndAlso assemblyRoutes.ContainsKey(asmID) Then
                    Dim asm = assemblyRoutes(asmID)
                    Dim asmName = asm.GetName.Name
                    Dim resKey = asmName + path.Substring(second).Replace("/", ".")
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