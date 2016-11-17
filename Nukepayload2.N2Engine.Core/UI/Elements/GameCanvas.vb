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
    End Class
End Namespace