Namespace Concurrent
    Public Class ConcurrentLinkedList(Of T)
        Implements ICollection(Of T), IEnumerable(Of T), IEnumerable, ICollection, IReadOnlyCollection(Of T)
        Friend _head As ConcurrentLinkedListNode(Of T)

        Friend _count As Integer

        Friend _version As Integer

        Private _syncRoot As New Object

        Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count, ICollection.Count, IReadOnlyCollection(Of T).Count
            Get
                SyncLock _syncRoot
                    Return _count
                End SyncLock
            End Get
        End Property

        Public ReadOnly Property First As ConcurrentLinkedListNode(Of T)
            Get
                SyncLock _syncRoot
                    Return Me._head
                End SyncLock
            End Get
        End Property

        Public ReadOnly Property Last As ConcurrentLinkedListNode(Of T)
            Get
                SyncLock _syncRoot
                    If (Me._head Is Nothing) Then
                        Return Nothing
                    End If
                    Return Me._head._prev
                End SyncLock
            End Get
        End Property

        ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly
            Get
                SyncLock _syncRoot
                    Return False
                End SyncLock
            End Get
        End Property

        ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                SyncLock _syncRoot
                    Return False
                End SyncLock
            End Get
        End Property

        ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                SyncLock _syncRoot
                    Return Me._syncRoot
                End SyncLock
            End Get
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal collection As IEnumerable(Of T))
            MyBase.New()
            If (collection Is Nothing) Then
                Throw New ArgumentNullException("collection")
            End If
            For Each t As T In collection
                Me.AddLast(t)
            Next
        End Sub

        Public Function AddAfter(ByVal node As ConcurrentLinkedListNode(Of T), ByVal value As T) As ConcurrentLinkedListNode(Of T)
            SyncLock _syncRoot
                Me.ValidateNode(node)
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = New ConcurrentLinkedListNode(Of T)(node.List, value)
                Me.InternalInsertNodeBefore(node._next, linkedListNode)
                Return linkedListNode
            End SyncLock
        End Function

        Public Sub AddAfter(ByVal node As ConcurrentLinkedListNode(Of T), ByVal newNode As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                Me.ValidateNode(node)
                Me.ValidateNewNode(newNode)
                Me.InternalInsertNodeBefore(node._next, newNode)
                newNode._list = Me
            End SyncLock
        End Sub

        Public Function AddBefore(ByVal node As ConcurrentLinkedListNode(Of T), ByVal value As T) As ConcurrentLinkedListNode(Of T)
            SyncLock _syncRoot
                Me.ValidateNode(node)
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = New ConcurrentLinkedListNode(Of T)(node.List, value)
                Me.InternalInsertNodeBefore(node, linkedListNode)
                If ((node) Is (Me._head)) Then
                    Me._head = linkedListNode
                End If
                Return linkedListNode
            End SyncLock
        End Function

        Public Sub AddBefore(ByVal node As ConcurrentLinkedListNode(Of T), ByVal newNode As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                Me.ValidateNode(node)
                Me.ValidateNewNode(newNode)
                Me.InternalInsertNodeBefore(node, newNode)
                newNode._list = Me
                If ((node) Is (Me._head)) Then
                    Me._head = newNode
                End If
            End SyncLock
        End Sub

        Public Function AddFirst(ByVal value As T) As ConcurrentLinkedListNode(Of T)
            SyncLock _syncRoot
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = New ConcurrentLinkedListNode(Of T)(Me, value)
                If (Me._head IsNot Nothing) Then
                    Me.InternalInsertNodeBefore(Me._head, linkedListNode)
                    Me._head = linkedListNode
                Else
                    Me.InternalInsertNodeToEmptyList(linkedListNode)
                End If
                Return linkedListNode
            End SyncLock
        End Function

        Public Sub AddFirst(ByVal node As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                Me.ValidateNewNode(node)
                If (Me._head IsNot Nothing) Then
                    Me.InternalInsertNodeBefore(Me._head, node)
                    Me._head = node
                Else
                    Me.InternalInsertNodeToEmptyList(node)
                End If
                node._list = Me
            End SyncLock
        End Sub

        Public Function AddLast(ByVal value As T) As ConcurrentLinkedListNode(Of T)
            SyncLock _syncRoot
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = New ConcurrentLinkedListNode(Of T)(Me, value)
                If (Me._head IsNot Nothing) Then
                    Me.InternalInsertNodeBefore(Me._head, linkedListNode)
                Else
                    Me.InternalInsertNodeToEmptyList(linkedListNode)
                End If
                Return linkedListNode
            End SyncLock
        End Function

        Public Sub AddLast(ByVal node As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                Me.ValidateNewNode(node)
                If (Me._head IsNot Nothing) Then
                    Me.InternalInsertNodeBefore(Me._head, node)
                Else
                    Me.InternalInsertNodeToEmptyList(node)
                End If
                node._list = Me
            End SyncLock
        End Sub

        Public Sub Clear() Implements ICollection(Of T).Clear
            SyncLock _syncRoot
                Dim [next] As ConcurrentLinkedListNode(Of T) = Me._head
                While [next] IsNot Nothing
                    Dim linkedListNode As ConcurrentLinkedListNode(Of T) = [next]
                    [next] = [next]._next
                    linkedListNode.Invalidate()
                End While
                Me._head = Nothing
                _count = 0
                Me._version = Me._version + 1
            End SyncLock
        End Sub

        Public Function Contains(ByVal value As T) As Boolean Implements ICollection(Of T).Contains
            SyncLock _syncRoot
                Return (Me.Find(value)) IsNot (Nothing)
            End SyncLock
        End Function

        Public Sub CopyTo(ByVal array As T(), ByVal index As Integer) Implements ICollection(Of T).CopyTo
            SyncLock _syncRoot
                If (array Is Nothing) Then
                    Throw New ArgumentNullException("array")
                End If
                If (index < 0 OrElse index > CInt(array.Length)) Then
                    Throw New ArgumentOutOfRangeException("index")
                End If
                If (CInt(array.Length) - index < _count) Then
                    Throw New ArgumentException("InsufficientSpace")
                End If
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = Me._head
                If (linkedListNode IsNot Nothing) Then
                    Do
                        Dim num As Integer = index
                        index = num + 1
                        array(num) = linkedListNode._item
                        linkedListNode = linkedListNode._next
                    Loop While (linkedListNode) IsNot (Me._head)
                End If
            End SyncLock
        End Sub

        Public Function Find(ByVal value As T) As ConcurrentLinkedListNode(Of T)
            SyncLock _syncRoot
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = Me._head
                Dim [default] As EqualityComparer(Of T) = EqualityComparer(Of T).Default()
                If (linkedListNode IsNot Nothing) Then
                    If (value Is Nothing) Then
                        While linkedListNode._item IsNot Nothing
                            linkedListNode = linkedListNode._next
                            If ((linkedListNode) Is (Me._head)) Then
                                Return Nothing
                            End If
                        End While
                        Return linkedListNode
                    Else
                        Do
                            If ([default].Equals(linkedListNode._item, value)) Then
                                Return linkedListNode
                            End If
                            linkedListNode = linkedListNode._next
                        Loop While (linkedListNode) IsNot (Me._head)
                    End If
                End If
                Return Nothing
            End SyncLock
        End Function

        Public Function FindLast(ByVal value As T) As ConcurrentLinkedListNode(Of T)
            SyncLock _syncRoot
                If (Me._head Is Nothing) Then
                    Return Nothing
                End If
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = Me._head._prev
                Dim linkedListNode1 As ConcurrentLinkedListNode(Of T) = linkedListNode
                Dim [default] As EqualityComparer(Of T) = EqualityComparer(Of T).Default()
                If (linkedListNode1 IsNot Nothing) Then
                    If (value Is Nothing) Then
                        While linkedListNode1._item IsNot Nothing
                            linkedListNode1 = linkedListNode1._prev
                            If ((linkedListNode1) Is (linkedListNode)) Then
                                Return Nothing
                            End If
                        End While
                        Return linkedListNode1
                    Else
                        Do
                            If ([default].Equals(linkedListNode1._item, value)) Then
                                Return linkedListNode1
                            End If
                            linkedListNode1 = linkedListNode1._prev
                        Loop While (linkedListNode1) IsNot (linkedListNode)
                    End If
                End If
                Return Nothing
            End SyncLock
        End Function

        Private Sub InternalInsertNodeBefore(ByVal node As ConcurrentLinkedListNode(Of T), ByVal newNode As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                newNode._next = node
                newNode._prev = node._prev
                node._prev._next = newNode
                node._prev = newNode
                Me._version = Me._version + 1
                _count = _count + 1
            End SyncLock
        End Sub

        Private Sub InternalInsertNodeToEmptyList(ByVal newNode As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = newNode
                linkedListNode._next = linkedListNode
                Dim linkedListNode1 As ConcurrentLinkedListNode(Of T) = newNode
                linkedListNode1._prev = linkedListNode1
                Me._head = newNode
                Me._version = Me._version + 1
                _count = _count + 1
            End SyncLock
        End Sub

        Friend Sub InternalRemoveNode(ByVal node As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                If ((node._next) IsNot (node)) Then
                    node._next._prev = node._prev
                    node._prev._next = node._next
                    If ((Me._head) Is (node)) Then
                        Me._head = node._next
                    End If
                Else
                    Me._head = Nothing
                End If
                node.Invalidate()
                _count = _count - 1
                Me._version = Me._version + 1
            End SyncLock
        End Sub

        Public Function Remove(ByVal value As T) As Boolean Implements ICollection(Of T).Remove
            SyncLock _syncRoot
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = Me.Find(value)
                If (linkedListNode Is Nothing) Then
                    Return False
                End If
                Me.InternalRemoveNode(linkedListNode)
                Return True
            End SyncLock
        End Function

        Public Sub Remove(ByVal node As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                Me.ValidateNode(node)
                Me.InternalRemoveNode(node)
            End SyncLock
        End Sub

        Public Sub RemoveFirst()
            SyncLock _syncRoot
                If (Me._head Is Nothing) Then
                    Throw New InvalidOperationException("LinkedListEmpty")
                End If
                Me.InternalRemoveNode(Me._head)
            End SyncLock
        End Sub

        Public Sub RemoveLast()
            SyncLock _syncRoot
                If (Me._head Is Nothing) Then
                    Throw New InvalidOperationException("LinkedListEmpty")
                End If
                Me.InternalRemoveNode(Me._head._prev)
            End SyncLock
        End Sub

        Private Sub Add(ByVal value As T) Implements ICollection(Of T).Add
            SyncLock _syncRoot
                Me.AddLast(value)
            End SyncLock
        End Sub

        Private Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            SyncLock _syncRoot
                Return New Enumerator(Me)
            End SyncLock
        End Function

        Private Sub ICollection_CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
            SyncLock _syncRoot
                If (array Is Nothing) Then
                    Throw New ArgumentNullException("array")
                End If
                If (array.Rank() <> 1) Then
                    Throw New ArgumentException("MultiRank")
                End If
                If (array.GetLowerBound(0) <> 0) Then
                    Throw New ArgumentException("NonZeroLowerBound")
                End If
                If (index < 0) Then
                    Throw New ArgumentOutOfRangeException("index")
                End If
                If (array.Length() - index < _count) Then
                    Throw New ArgumentException("InsufficientSpace")
                End If
                Dim tArray As T() = TryCast(array, T())
                If (tArray IsNot Nothing) Then
                    Me.CopyTo(tArray, index)
                    Return
                End If
                Dim objArray As Object() = TryCast(array, Object())
                If (objArray Is Nothing) Then
                    Throw New ArgumentException("ArrayTypeMismatch")
                End If
                Dim linkedListNode As ConcurrentLinkedListNode(Of T) = Me._head
                Try
                    If (linkedListNode IsNot Nothing) Then
                        Do
                            Dim num As Integer = index
                            index = num + 1
                            objArray(num) = linkedListNode._item
                            linkedListNode = linkedListNode._next
                        Loop While (linkedListNode) IsNot (Me._head)
                    End If
                Catch arrayTypeMismatchException As ArrayTypeMismatchException
                    Throw New ArgumentException("ArrayTypeMismatch")
                End Try
            End SyncLock
        End Sub

        Private Function IEnumerator_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            SyncLock _syncRoot
                Return GetEnumerator()
            End SyncLock
        End Function

        Friend Sub ValidateNewNode(ByVal node As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                If (node Is Nothing) Then
                    Throw New ArgumentNullException("node")
                End If
                If (node.List IsNot Nothing) Then
                    Throw New InvalidOperationException("LinkedListNodeIsAttached")
                End If
            End SyncLock
        End Sub

        Friend Sub ValidateNode(ByVal node As ConcurrentLinkedListNode(Of T))
            SyncLock _syncRoot
                If (node Is Nothing) Then
                    Throw New ArgumentNullException("node")
                End If
                If (node.List IsNot Me) Then
                    Throw New InvalidOperationException("ExternalLinkedListNode")
                End If
            End SyncLock
        End Sub

        Public Structure Enumerator
            Implements IEnumerator(Of T), IDisposable, IEnumerator
            Private _list As ConcurrentLinkedList(Of T)

            Private _node As ConcurrentLinkedListNode(Of T)

            Private _version As Integer

            Private _current As T

            Private _index As Integer

            Private Const ConcurrentLinkedListName As String = "LinkedList"

            Private Const CurrentValueName As String = "Current"

            Private Const VersionName As String = "Version"

            Private Const IndexName As String = "Index"

            Public ReadOnly Property Current As T Implements IEnumerator(Of T).Current
                Get
                    Return _current
                End Get
            End Property

            ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
                Get
                    SyncLock _list._syncRoot
                        If (_index = 0 OrElse _index = _list.Count + 1) Then
                            Throw New InvalidOperationException("EnumOpCantHappen")
                        End If
                        Return _current
                    End SyncLock
                End Get
            End Property

            Friend Sub New(ByVal list As ConcurrentLinkedList(Of T))
                SyncLock _list._syncRoot
                    _list = list
                    Me._version = list._version
                    Me._node = list._head
                    _current = Nothing
                    _index = 0
                End SyncLock
            End Sub

            Public Sub Dispose() Implements IDisposable.Dispose
            End Sub

            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
                SyncLock _list._syncRoot
                    If (Me._version <> _list._version) Then
                        Throw New InvalidOperationException("EnumFailed")
                    End If
                    If (Me._node Is Nothing) Then
                        _index = _list.Count + 1
                        Return False
                    End If
                    _index = _index + 1
                    _current = Me._node._item
                    Me._node = Me._node._next
                    If ((Me._node) Is (_list._head)) Then
                        Me._node = Nothing
                    End If
                    Return True
                End SyncLock
            End Function

            Private Sub Reset() Implements IEnumerator.Reset
                SyncLock _list._syncRoot
                    If (Me._version <> _list._version) Then
                        Throw New InvalidOperationException("EnumFailed")
                    End If
                    _current = Nothing
                    Me._node = _list._head
                    _index = 0
                End SyncLock
            End Sub
        End Structure
    End Class
End Namespace