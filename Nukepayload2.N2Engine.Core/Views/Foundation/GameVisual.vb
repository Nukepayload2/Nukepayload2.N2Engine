''' <summary>
''' 游戏中的基本可见元素
''' </summary>
Public MustInherit Class GameVisual
    Inherits GameObject
    ''' <summary>
    ''' 渲染时需要处理的特效
    ''' </summary>
    Public ReadOnly Property Effect As New PropertyBinder(Of GameEffect)
End Class