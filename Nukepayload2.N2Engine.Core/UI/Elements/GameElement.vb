Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Renderers

Namespace UI.Elements
    ''' <summary>
    ''' 在<see cref="GameCanvas"/>中的元素 
    ''' </summary>
    Public MustInherit Class GameElement
        Inherits GameVisual

        ''' <summary>
        ''' Z序越高越靠外，反之靠里
        ''' </summary>
        Public ReadOnly Property ZIndex As New PropertyBinder(Of Integer)
        ''' <summary>
        ''' 视图的位置。这通常是物体的左上角的坐标。
        ''' </summary>
        Public Overridable ReadOnly Property Location As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 需要让渲染器开始处理平台渲染控件的事件
        ''' </summary>
        Public Event HandleRendererRequested As EventHandler(Of RendererRegistrationRequestedEventArgs)
        ''' <summary>
        ''' 发出注册渲染器注册请求
        ''' </summary>
        Public Sub HandleRenderer(parentRenderer As RendererBase)
            RaiseEvent HandleRendererRequested(Me, New RendererRegistrationRequestedEventArgs(parentRenderer))
        End Sub
        ''' <summary>
        ''' 需要让渲染器结束处理平台渲染控件的事件, 并且释放渲染器占用的全部资源
        ''' </summary>
        Public Event RendererUnloadRequested As EventHandler(Of RendererRegistrationRequestedEventArgs)
        ''' <summary>
        ''' 发出注册渲染器注册请求
        ''' </summary>
        Public Sub UnloadRenderer(parentRenderer As RendererBase)
            RaiseEvent RendererUnloadRequested(Me, New RendererRegistrationRequestedEventArgs(parentRenderer))
        End Sub
        ''' <summary>
        ''' 主动请求从游戏画布移除
        ''' </summary>
        Public Event RemoveFromGameCanvasReuqested As EventHandler
        ''' <summary>
        ''' 从游戏画布移除
        ''' </summary>
        Public Sub RemoveFromGameCanvas()
            RaiseEvent RemoveFromGameCanvasReuqested(Me, EventArgs.Empty)
        End Sub
        Sub New()
            RendererBase.CreateElementRenderer(Me)
        End Sub
        Public Overridable Property UpdateCommand As IGameCommand = New SimpleCommand(Sub() RaiseEvent Updating())

        Public Event Updating()
    End Class
End Namespace