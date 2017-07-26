Imports Nukepayload2.N2Engine.PhysicsIntegration
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Controls

Namespace UI.Elements
    ''' <summary>
    ''' 包含自定义内容的实体。使用与 GameContentControl 相同的渲染逻辑。
    ''' </summary>
    ''' <typeparam name="TContent">要呈现的内容</typeparam>
    Public Class GameContentEntity(Of TContent As GameVisual)
        Inherits GameEntity
        Implements IGameContentControl

        Sub New(content As TContent, collider As ICollider)
            MyBase.New(collider)
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