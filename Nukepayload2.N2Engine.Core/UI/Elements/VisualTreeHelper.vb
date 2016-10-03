Namespace UI.Elements

    Public Class VisualTreeHelper
        Public Shared Sub AddChildrenChangedHandler(Of TVisual As GameVisual)(Children As ObservableCollection(Of TVisual))
            AddHandler Children.CollectionChanged,
            Sub(sender, e)
                Dim removeFromChildren As EventHandler = Sub(ele, args) Children.Remove(DirectCast(ele, TVisual))
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