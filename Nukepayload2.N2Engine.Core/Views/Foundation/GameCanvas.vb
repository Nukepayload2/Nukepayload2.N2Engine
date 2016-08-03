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
    Sub New()
        AddHandler Children.CollectionChanged,
            Sub(sender, e)
                Dim removeFromChildren As EventHandler = Sub(ele, args) Children.Remove(DirectCast(ele, GameElement))
                For Each newItem As GameElement In e.NewItems
                    AddHandler newItem.RemoveFromGameCanvasReuqested, removeFromChildren
                Next
                For Each oldItem As GameElement In e.OldItems
                    RemoveHandler oldItem.RemoveFromGameCanvasReuqested, removeFromChildren
                Next
            End Sub
    End Sub
End Class