Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' 一种特殊的容器控件，自动使用默认的容器控件绘制方式而无需为这种容器控件编写渲染器。一般这种类型由模板创建。
    ''' </summary>
    Public Class GameTemplatedContent
        Inherits GameVisualContainer

        Protected Overrides Sub CreateRenderer()
            Renderer = Platform.PlatformActivator.CreateBaseInstance(Of IGameTemplatedContentRenderer, RendererBase)(Me)
        End Sub
    End Class

End Namespace