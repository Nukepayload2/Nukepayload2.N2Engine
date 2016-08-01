''' <summary>
''' 游戏的画布，是全部可见元素的父级。
''' </summary>
Public Class GameCanvas
    Inherits GameVisual
    ''' <summary>
    ''' 画板的子元素
    ''' </summary>
    Public ReadOnly Property Children As New ObservableCollection(Of GameElement)
    ''' <summary>
    ''' 这个画布
    ''' </summary>
    Public ReadOnly Property Paused As New PropertyBinder(Of Boolean)
End Class