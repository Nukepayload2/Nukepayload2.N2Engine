Imports System.Reflection
Imports Nukepayload2.N2Engine.Platform

Namespace Information
    ''' <summary>
    ''' 关于游戏的信息
    ''' </summary>
    Public Class Environment
        ''' <summary>
        ''' 共享的游戏逻辑所在的程序集
        ''' </summary>
        Public Shared Property SharedLogicAssembly As Assembly

        Shared _Platform As Platforms
        ''' <summary>
        ''' 当前游戏运行在哪个平台
        ''' </summary>
        Public Shared Property Platform As Platforms
            Get
                Return _Platform
            End Get
            Friend Set(value As Platforms)
                _Platform = value
            End Set
        End Property

        Shared _Renderer As Platform.Renderers
        ''' <summary>
        ''' 当前游戏使用哪种渲染器
        ''' </summary>
        Public Shared Property Renderer As Platform.Renderers
            Get
                Return _Renderer
            End Get
            Set(value As Platform.Renderers)
                _Renderer = value
            End Set
        End Property
    End Class
End Namespace