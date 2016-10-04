Imports Nukepayload2.N2Engine.Animations

Namespace UI.Elements
    ''' <summary>
    ''' 游戏的画布，是全部可见元素的父级。
    ''' </summary>
    Public Class GameCanvas
        Inherits GameVisualContainter
        ''' <summary>
        ''' 默认的场景导航动画表
        ''' </summary>
        Public Property ContentTransitions As IList(Of TransitionAnimation)
    End Class
End Namespace