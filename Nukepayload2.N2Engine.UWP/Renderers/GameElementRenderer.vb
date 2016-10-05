Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' 游戏中的非布局可见元素的渲染器。会被相应的游戏元素自动创建。
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class GameElementRenderer(Of T As GameElement)
    Inherits UWPRenderer(Of T)
    ''' <summary>
    ''' 警告：如果你的渲染器使用了 <see cref="PlatformImplAttribute"/> 标注它属于哪个 <see cref="GameVisual"/>, 则不要显式调用这个构造函数。因为游戏元素渲染器会被相应的游戏元素自动创建。
    ''' </summary>
    Sub New(view As T)
        MyBase.New(view)
        AddHandler view.HandleRendererRequested, AddressOf OnHandleRendererRequested
        AddHandler view.RendererUnloadRequested, AddressOf OnRendererUnhandleRequested
    End Sub

    Private Sub OnRendererUnhandleRequested(sender As Object, e As RendererRegistrationRequestedEventArgs)
        With DirectCast(e.ParentRenderer, GameCanvasRenderer)
            RemoveHandler .CreateResources, AddressOf OnCreateResources
            RemoveHandler .Draw, AddressOf OnDraw
            RemoveHandler .GameLoopStarting, AddressOf OnGameLoopStarting
            RemoveHandler .GameLoopStopped, AddressOf OnGameLoopStopped
            RemoveHandler .Update, AddressOf OnUpdate
        End With
    End Sub

    Private Sub OnHandleRendererRequested(sender As Object, e As RendererRegistrationRequestedEventArgs)
        With DirectCast(e.ParentRenderer, GameCanvasRenderer)
            AddHandler .CreateResources, AddressOf OnCreateResources
            AddHandler .Draw, AddressOf OnDraw
            AddHandler .GameLoopStarting, AddressOf OnGameLoopStarting
            AddHandler .GameLoopStopped, AddressOf OnGameLoopStopped
            AddHandler .Update, AddressOf OnUpdate
        End With
    End Sub

    Public Overrides Sub DisposeResources()
        RemoveHandler View.HandleRendererRequested, AddressOf OnHandleRendererRequested
        RemoveHandler View.RendererUnloadRequested, AddressOf OnRendererUnhandleRequested
    End Sub
End Class
