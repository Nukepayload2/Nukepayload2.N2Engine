Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Resources

Namespace UI.Elements
    ''' <summary>
    ''' 表示 2D 贴图
    ''' </summary>
    Public Class SpriteElement
        Inherits GameElement
        ''' <summary>
        ''' 表示贴图的路径
        ''' </summary>
        Public Property Sprite As New PropertyBinder(Of BitmapResource)
        ''' <summary>
        ''' 指定大于0的值可以延迟加载。当加载被延迟时，将不会在Loading界面加载这些内容，而是在游戏开始后才加载。(未实施：值越大，加载优先级越低)。
        ''' </summary>
        Public ReadOnly Property DefferedLoadLevel As New PropertyBinder(Of Integer)
        ''' <summary>
        ''' 如果使用延迟加载，尚未加载时的应该显示成什么颜色。
        ''' </summary>
        Public ReadOnly Property LoadingColor As New PropertyBinder(Of Color)
    End Class
End Namespace