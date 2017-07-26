Namespace Platform
    ''' <summary>
    ''' 表示基础类库支持的平台
    ''' </summary>
    <Flags>
    Public Enum Platforms
        ''' <summary>
        ''' 不要使用这个值
        ''' </summary>
        Unknown
        ''' <summary>
        ''' Windows 10 通用平台
        ''' </summary>
        UniversalWindows
        ''' <summary>
        ''' 安卓
        ''' </summary>
        Android = 1 << 1
        ''' <summary>
        ''' 苹果的移动端
        ''' </summary>
        iOS = 1 << 2
        ''' <summary>
        ''' Windows 桌面 （使用 DirectX）
        ''' </summary>
        WindowsDesktop = 1 << 3
        ''' <summary>
        ''' Windows 8.1
        ''' </summary>
        WindowsRT81 = 1 << 4
        ''' <summary>
        ''' Windows Phone 8.1
        ''' </summary>
        WindowsPhone81 = 1 << 5
        ''' <summary>
        ''' Linux, Mac 和 Windows 桌面 （使用 OpenGL）
        ''' </summary>
        DesktopGL = 1 << 6
    End Enum
End Namespace