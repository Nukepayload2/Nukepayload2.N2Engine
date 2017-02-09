Namespace Linq
    ''' <summary>
    ''' 关于树的 Linq 拓展
    ''' </summary>
    Public Module TreeHelper
        ''' <summary>
        ''' 按层次遍历树的节点。从树根开始，从最后的叶子节点结束。
        ''' </summary>
        ''' <typeparam name="TNode">树节点类型。</typeparam>
        ''' <param name="root">树的根节点。</param>
        ''' <param name="subNodes">获取指定节点的子节点。可以为空或者是数量为0。</param>
        ''' <returns>遍历结果</returns>
        <Extension>
        Public Iterator Function HierarchyWalk(Of TNode)(root As TNode, subNodes As Func(Of TNode, IEnumerable(Of TNode))) As IEnumerable(Of TNode)
            Dim que As New Queue(Of TNode)
            que.Enqueue(root)
            Do
                Dim cur = que.Dequeue
                Yield cur
                Dim nodes = subNodes(cur)
                If nodes IsNot Nothing Then
                    For Each node In nodes
                        que.Enqueue(node)
                    Next
                End If
            Loop While que.Count > 0
        End Function
    End Module
End Namespace