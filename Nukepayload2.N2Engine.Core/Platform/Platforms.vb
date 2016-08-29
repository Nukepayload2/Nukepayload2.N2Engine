Namespace Platform
    <Flags>
    Public Enum Platforms
        Unknown
        UniversalWindows
        Android = 1 << 1
        iOS = 1 << 2
        WindowsDesktop = 1 << 3
        WindowsRT81 = 1 << 4
        WindowsPhone81 = 1 << 5
    End Enum
End Namespace