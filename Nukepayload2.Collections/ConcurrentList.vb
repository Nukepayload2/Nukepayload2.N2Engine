Namespace Concurrent
    ''' <summary>
    ''' 线程安全的线性表。全部操作会被加锁。性能警告：使用 For Each (Visual Basic) 循环 （在 Visual C# 为 foreach）会创建此集合的副本来满足线程安全。
    ''' </summary>
    ''' <typeparam name="T">表内的数据类型</typeparam>
    Public Class ConcurrentList(Of T)
        Implements IList(Of T)
        Dim list As New List(Of T)
        Dim lock As New Object
        ''' <summary>
        ''' 获取当前 <see cref="ConcurrentList(Of T)"/> 中元素的数量
        ''' </summary>
        Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count
            Get
                SyncLock lock
                    Return DirectCast(list, IList(Of T)).Count
                End SyncLock
            End Get
        End Property
        ''' <summary>
        ''' 当前 <see cref="ConcurrentList(Of T)"/> 是否只读
        ''' </summary>
        Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly
            Get
                SyncLock lock
                    Return DirectCast(list, IList(Of T)).IsReadOnly
                End SyncLock
            End Get
        End Property
        ''' <summary>
        ''' 获取或设置当前 <see cref="ConcurrentList(Of T)"/> 中的某个元素
        ''' </summary>
        ''' <param name="index">元素索引</param>
        Default Public Property Item(index As Integer) As T Implements IList(Of T).Item
            Get
                SyncLock lock
                    Return DirectCast(list, IList(Of T))(index)
                End SyncLock
            End Get
            Set(value As T)
                SyncLock lock
                    DirectCast(list, IList(Of T))(index) = value
                End SyncLock
            End Set
        End Property
        ''' <summary>
        ''' 向当前 <see cref="ConcurrentList(Of T)"/> 末尾添加项
        ''' </summary>
        ''' <param name="item">要添加的项目</param>
        Public Sub Add(item As T) Implements ICollection(Of T).Add
            SyncLock lock
                DirectCast(list, IList(Of T)).Add(item)
            End SyncLock
        End Sub

        ''' <summary>
        ''' 清除当前 <see cref="ConcurrentList(Of T)"/> 全部项
        ''' </summary>
        Public Sub Clear() Implements ICollection(Of T).Clear
            SyncLock lock
                DirectCast(list, IList(Of T)).Clear()
            End SyncLock
        End Sub
        ''' <summary>
        ''' 将当前 <see cref="ConcurrentList(Of T)"/> 复制到数组
        ''' </summary>
        ''' <param name="array">目标数组</param>
        ''' <param name="arrayIndex">数组的下标</param>
        Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
            SyncLock lock
                DirectCast(list, IList(Of T)).CopyTo(array, arrayIndex)
            End SyncLock
        End Sub
        ''' <summary>
        ''' 向当前 <see cref="ConcurrentList(Of T)"/> 插入项
        ''' </summary>
        ''' <param name="index">插入的位置</param>
        ''' <param name="item">插入的项</param>
        Public Sub Insert(index As Integer, item As T) Implements IList(Of T).Insert
            SyncLock lock
                DirectCast(list, IList(Of T)).Insert(index, item)
            End SyncLock
        End Sub
        ''' <summary>
        ''' 删除当前 <see cref="ConcurrentList(Of T)"/> 某个位置的项
        ''' </summary>
        ''' <param name="index">插入的位置</param>
        Public Sub RemoveAt(index As Integer) Implements IList(Of T).RemoveAt
            SyncLock lock
                DirectCast(list, IList(Of T)).RemoveAt(index)
            End SyncLock
        End Sub
        ''' <summary>
        ''' 确定当前 <see cref="ConcurrentList(Of T)"/> 是否包含项
        ''' </summary>
        ''' <param name="item">要检查的项</param>
        Public Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
            SyncLock lock
                Return DirectCast(list, IList(Of T)).Contains(item)
            End SyncLock
        End Function
        ''' <summary>
        ''' 返回一个循环访问集合的的副本枚举器。创建副本的过程是互斥的。
        ''' </summary>
        Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            Return DirectCast(list, IList(Of T)).ToArray.GetEnumerator()
        End Function

        Public Function IndexOf(item As T) As Integer Implements IList(Of T).IndexOf
            SyncLock lock
                Return DirectCast(list, IList(Of T)).IndexOf(item)
            End SyncLock
        End Function
        ''' <summary>
        ''' 删除当前 <see cref="ConcurrentList(Of T)"/> 第一个匹配的项
        ''' </summary>
        ''' <param name="item">要删除的项</param>
        Public Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
            SyncLock lock
                Return DirectCast(list, IList(Of T)).Remove(item)
            End SyncLock
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            SyncLock lock
                Return DirectCast(list, IList(Of T)).ToArray.GetEnumerator()
            End SyncLock
        End Function
        ''' <summary>
        ''' 将 <see cref="ConcurrentList(Of T)"/> 全部内容复制到新的数组中
        ''' </summary>
        Public Function ToArray() As T()
            SyncLock lock
                Return DirectCast(Me, IEnumerable(Of T)).ToArray
            End SyncLock
        End Function
    End Class

End Namespace
