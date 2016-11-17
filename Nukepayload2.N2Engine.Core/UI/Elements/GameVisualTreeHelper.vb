Imports System.Collections.Specialized

Namespace UI.Elements
    ''' <summary>
    ''' 提供游戏可见对象树的帮助
    ''' </summary>
    Public Class GameVisualTreeHelper
        ''' <summary>
        ''' 订阅子元素变更的通知, 让子元素可以主动从父元素移除。
        ''' </summary>
        Public Shared Sub AddChildrenChangedHandler(Container As GameVisualContainer)
            Container.RegisterOnChildrenChanged(
            Sub(sender, e)
                Dim removeFromChildren As EventHandler = Sub(ele, args) Container.Children.Remove(DirectCast(ele, GameVisual))
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
            End Sub)
        End Sub

        ''' <summary>
        ''' 在容器的集合变更时自动修改子元素对父容器的引用
        ''' </summary>
        Public Shared Sub AutoHandleParent(Container As GameVisualContainer)
            Container.RegisterOnChildrenChanged(
                Sub(sender, e)
                    If e.NewItems IsNot Nothing Then
                        For Each newItem As GameVisual In e.NewItems
                            newItem.Parent = Container
                        Next
                    End If
                    If e.OldItems IsNot Nothing Then
                        For Each oldItem As GameVisual In e.OldItems
                            oldItem.Parent = Nothing
                        Next
                    End If
                End Sub)
        End Sub
    End Class

End Namespace