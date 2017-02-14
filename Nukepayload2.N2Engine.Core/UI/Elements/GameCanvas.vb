Imports Nukepayload2.N2Engine.Animations

Namespace UI.Elements
    ''' <summary>
    ''' 游戏的画布，是全部可见元素的父级。
    ''' </summary>
    Public Class GameCanvas
        Inherits GameVisualContainer
        ''' <summary>
        ''' 默认的场景导航动画表
        ''' </summary>
        Public Property ContentTransitions As IList(Of TransitionAnimation)
        ''' <summary>
        ''' 这个类型不自动创建渲染器
        ''' </summary>
        Protected Overrides Sub CreateRenderer()

        End Sub
        ''' <summary>
        ''' 事件路由的方向
        ''' </summary>
        Public Property EventRouteDirection As GameEventRouteDirections
        ''' <summary>
        ''' 事件路由的模式
        ''' </summary>
        Public Property EventRouteMode As GameEventRouteModes
        ''' <summary>
        ''' 在画布大小变更时引发此事件
        ''' </summary>
        Public Event SizeChanged As GameObjectEventHandler(Of GameCanvas, EventArgs)
    End Class
End Namespace