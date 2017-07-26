Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' 游戏中的内容控件。渲染任务会传递给内容。默认情况下，此类型的渲染器是 <see cref="IGameContentControlRenderer"/>。
    ''' </summary>
    ''' <typeparam name="TContent">内容的类型</typeparam>
    Public MustInherit Class GameContentControl(Of TContent As GameTemplatedContent)
        Inherits GameControl
        Implements IGameContentControl

        Sub New(content As TContent)
            Me.Content = content
            content.Parent = Me
        End Sub

        Protected Overrides Sub CreateRenderer()
            Renderer = Platform.PlatformActivator.CreateBaseInstance(Of IGameContentControlRenderer, RendererBase)(Me)
        End Sub
        ''' <summary>
        ''' 此控件实际显示出的内容。
        ''' </summary>
        Public ReadOnly Property Content As TContent

        Public Function GetContent() As GameVisual Implements IGameContentControl.GetContent
            Return Content
        End Function

        Public Overrides Function GetSubNodes() As IEnumerable(Of GameVisual)
            Return {Content}
        End Function

    End Class

End Namespace