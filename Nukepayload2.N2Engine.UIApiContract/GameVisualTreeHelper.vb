Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI

    Public Class GameVisualTreeHelper
        ''' <summary>
        ''' 获取一个可见对象的绝对位置
        ''' </summary>
        ''' <param name="visual"></param>
        ''' <returns></returns>
        Public Shared Function GetAbsolutePosition(visual As GameVisual) As Vector2
            Dim loc As New Vector2
            Do
                If visual.Location.CanRead Then
                    loc += visual.Location.Value
                End If
                visual = visual.Parent
            Loop While visual IsNot Nothing
            Return loc
        End Function
        ''' <summary>
        ''' 订阅子元素变更的通知, 让子元素可以主动从父元素移除。
        ''' </summary>
        Public Shared Sub RemoveChildrenWhenChildRequested(Container As GameVisualContainer)
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
        Public Shared Sub ModifyParentOnChildrenChanged(Container As GameVisualContainer)
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
        ''' <summary>
        ''' 判断点击的点是否落在矩形元素的范围内
        ''' </summary>
        ''' <param name="visual"></param>
        ''' <param name="point">要判断的点</param>
        ''' <returns>点击的点是否落在矩形的范围内</returns>
        Public Shared Function HitTest(visual As GameVisual, point As Vector2) As Boolean
            Dim absolutePosition = GetAbsolutePosition(visual)
            If point.X >= absolutePosition.X AndAlso point.Y >= absolutePosition.Y Then
                absolutePosition += visual.Size.Value
                If point.X <= absolutePosition.X AndAlso point.Y <= absolutePosition.Y Then
                    Return True
                End If
            End If
            Return False
        End Function
    End Class

End Namespace