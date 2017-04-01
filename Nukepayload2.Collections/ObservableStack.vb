Imports System.Collections.Specialized

Namespace Specialized
    ''' <summary>
    ''' 表示可变大小的后进先出 (LIFO) 集合（对于相同指定类型的实例）。其中的元素发生变更时将引发通知。
    ''' </summary>
    ''' <typeparam name="T">指定堆栈中元素的类型。</typeparam>
    Public Class ObservableStack(Of T)
        Implements INotifyCollectionChanged, INotifyPropertyChanged, ICollection, IReadOnlyCollection(Of T)
        ''' <summary>
        ''' 堆栈的成员发生变化时引发此事件
        ''' </summary>
        Public Event CollectionChanged As NotifyCollectionChangedEventHandler Implements INotifyCollectionChanged.CollectionChanged
        ''' <summary>
        ''' 堆栈的属性变化时引发此事件
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Dim _stack As Stack(Of T)

        Sub New()
            _stack = New Stack(Of T)
        End Sub

        ''' <param name="capacity">可包含的初始元素数。</param>
        '''<exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> 小于零。</exception>
        Sub New(capacity As Integer)
            _stack = New Stack(Of T)(capacity)
        End Sub

        ''' <param name="collection">从中复制元素的集合。</param>
        '''<exception cref="ArgumentNullException"><paramref name="collection"/> 为空引用。</exception>
        Sub New(collection As IEnumerable(Of T))
            _stack = New Stack(Of T)(collection)
        End Sub
        ''' <summary>
        ''' 获取包含的元素数。
        ''' </summary>
        ''' <returns>包含的元素数。</returns>
        Public ReadOnly Property Count As Integer Implements IReadOnlyCollection(Of T).Count, ICollection.Count
            Get
                Return _stack.Count
            End Get
        End Property
        ''' <summary>
        ''' 获取同步锁对象
        ''' </summary>
        Protected ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Return DirectCast(_stack, ICollection).SyncRoot
            End Get
        End Property
        ''' <summary>
        ''' 当前集合是否使用同步锁
        ''' </summary>
        Protected ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                Return DirectCast(_stack, ICollection).IsSynchronized
            End Get
        End Property
        ''' <summary>
        ''' 移除所有对象。
        ''' </summary>
        Public Sub Clear()
            _stack.Clear()
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(
                                     NotifyCollectionChangedAction.Reset))
            CountChanged()
        End Sub
        ''' <summary>
        ''' 引发数量属性变化事件
        ''' </summary>
        Private Sub CountChanged()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Count)))
        End Sub
        ''' <summary>
        ''' 将堆栈中的内容复制到到现有一维 <see cref="Array"/>, 指定的数组索引处开始。
        ''' </summary>
        ''' <param name="array">复制的目标</param>
        ''' <param name="arrayIndex">从此处开始复制</param>
        '''<exception cref="ArgumentNullException">数组为空引用。</exception>
        '''<exception cref="ArgumentOutOfRangeException">数组下标小于 0。</exception>
        '''<exception cref="ArgumentException">数组太小。</exception>
        Public Sub CopyTo(array As T(), arrayIndex As Integer)
            _stack.CopyTo(array, arrayIndex)
        End Sub
        ''' <summary>
        ''' 如果元素数小于当前容量的 90%，将容量设置为堆栈中的实际元素数。
        ''' </summary>
        Public Sub TrimExcess()
            _stack.TrimExcess()
        End Sub
        ''' <summary>
        ''' (标准操作) 在顶部插入一个对象。
        ''' </summary>
        ''' <param name="item">要推入到堆栈中的对象。 对于引用类型，该值可以为空引用。</param>
        Public Sub Push(item As T)
            _stack.Push(item)
            CountChanged()
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(
                                     NotifyCollectionChangedAction.Add, item, _stack.Count - 1))
        End Sub
        ''' <summary>
        ''' 返回枚举数。
        ''' </summary>
        Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            Return _stack.GetEnumerator
        End Function
        ''' <summary>
        ''' 返回枚举数。
        ''' </summary>
        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _stack.GetEnumerator
        End Function

        ''' <summary>
        ''' 将堆栈中的内容复制到到现有一维 <see cref="Array"/>, 指定的数组索引处开始。
        ''' </summary>
        ''' <param name="array">复制的目标</param>
        ''' <param name="index">从此处开始复制</param>
        '''<exception cref="ArgumentNullException">数组为空引用。</exception>
        '''<exception cref="ArgumentOutOfRangeException">数组下标小于 0。</exception>
        '''<exception cref="ArgumentException">数组太小。</exception>
        Public Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
            DirectCast(_stack, ICollection).CopyTo(array, index)
        End Sub
        ''' <summary>
        ''' 确定某元素是否在堆栈中。
        ''' </summary>
        ''' <param name="item">要在堆栈中定位的对象。 对于引用类型，该值可以为 null。</param>
        ''' <returns>某元素在堆栈中则返回真，否则返回假。</returns>
        Public Function Contains(item As T) As Boolean
            Return _stack.Contains(item)
        End Function
        ''' <summary>
        ''' (标准操作) 返回堆栈顶部的对象而不删除它。
        ''' </summary>
        ''' <returns>在顶部的对象</returns>
        ''' <exception cref="InvalidOperationException">堆栈为空</exception>
        Public Function Peek() As T
            Return _stack.Peek
        End Function
        ''' <summary>
        ''' (标准操作) 移除并返回堆栈顶部的对象。
        ''' </summary>
        ''' <returns>在顶部的对象</returns>
        ''' <exception cref="InvalidOperationException">堆栈为空</exception>
        Public Function Pop() As T
            Dim value = _stack.Pop
            CountChanged()
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(
                                     NotifyCollectionChangedAction.Remove, value, _stack.Count))
            Return value
        End Function
        ''' <summary>
        ''' 将堆栈的内容复制到新数组
        ''' </summary>
        ''' <returns>包含元素的副本的新数组</returns>
        Public Function ToArray() As T()
            Return _stack.ToArray
        End Function
    End Class
End Namespace