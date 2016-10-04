Imports System.Collections.Specialized

Namespace UI.Elements
    ''' <summary>
    ''' 提供游戏可见对象树的帮助
    ''' </summary>
    Public Class GameVisualTreeHelper
        ''' <summary>
        ''' 订阅子元素变更的通知, 让子元素可以主动从父元素移除。
        ''' </summary>
        Public Shared Sub AddChildrenChangedHandler(Of TVisual As GameVisual, TCollection As {ICollection(Of TVisual), INotifyCollectionChanged})(Children As TCollection)
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