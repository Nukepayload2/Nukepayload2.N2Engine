Namespace Platform
    ''' <summary>
    ''' 当前游戏使用哪个渲染器
    ''' </summary>
    Public Enum Renderers
        ''' <summary>
        ''' 不要使用这个值
        ''' </summary>
        None
        ''' <summary>
        ''' 使用 Win2D 渲染。
        ''' </summary>
        Win2D
        ''' <summary>
        ''' 使用 Mono Game 渲染。
        ''' </summary>
        MonoGame
    End Enum

End Namespace