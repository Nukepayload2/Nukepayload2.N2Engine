Namespace UI.Elements

    Public Class GameVisualContainter(Of T As GameVisual)
        Inherits GameVisual
        ''' <summary>
        ''' 画板的子元素
        ''' </summary>
        Public ReadOnly Property Children As New ObservableCollection(Of T)
        Sub New()
            AddChildrenChangedHandler(Children)
        End Sub
        Protected Sub AddChildrenChangedHandler(Children As ObservableCollection(Of T))
            AddHandler Children.CollectionChanged,
            Sub(sender, e)
                Dim removeFromChildren As EventHandler = Sub(ele, args) Children.Remove(DirectCast(ele, T))
                If e.NewItems IsNot Nothing Then
                    For Each newItem As GameVisual In e.NewItems
                        AddHandler newItem.RemoveFromGameCanvasReuqested, removeFromChildren
                    Next
                End If
                If e.OldItems IsNot Nothing Then
                    For Each oldItem As GameVisual In e.OldItems
                        RemoveHandler oldItem.RemoveFromGameCanvasReuqested, removeFromChildren
                    Next
                End If
            End Sub
        End Sub
    End Class
End Namespace