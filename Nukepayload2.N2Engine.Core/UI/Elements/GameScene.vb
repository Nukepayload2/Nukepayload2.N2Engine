Imports Nukepayload2.N2Engine.Animations

Namespace UI.Elements
    ''' <summary>
    ''' 场景 - 层 - 元素 游戏对象树模型下的根节点。
    ''' </summary>
    Public Class GameScene
        Inherits GameVisual

        Public ReadOnly Property Children As New LinkedList(Of GameLayer(Of GameVisual))
        ''' <summary>
        ''' 场景的过渡动画，例如淡入淡出。
        ''' </summary>
        Public Property Transitions As TransitionAnimation
        Public ReadOnly Property Cameras As New Dictionary(Of String, GameCamera)
        Public Property Template As Func(Of GameScene, VisualizingScrollViewer)
    End Class

End Namespace