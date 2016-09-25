Namespace Concurrent
    Public Class ConcurrentLinkedListNode(Of T)
        Friend _list As ConcurrentLinkedList(Of T)

        Friend _next As ConcurrentLinkedListNode(Of T)

        Friend _prev As ConcurrentLinkedListNode(Of T)

        Friend _item As T

        Dim _valueLock As New Object

        Public ReadOnly Property List As ConcurrentLinkedList(Of T)
            Get
                Return _list
            End Get
        End Property

        Public ReadOnly Property [Next] As ConcurrentLinkedListNode(Of T)
            Get
                If (_list Is Nothing OrElse _next Is _list._head) Then
                    Return Nothing
                End If
                Return _next
            End Get
        End Property

        Public ReadOnly Property Previous As ConcurrentLinkedListNode(Of T)
            Get
                If (_prev Is Nothing OrElse Me Is _list._head) Then
                    Return Nothing
                End If
                Return _prev
            End Get
        End Property

        Public Property Value As T
            Get
                SyncLock _valueLock
                    Return Me._item
                End SyncLock
            End Get
            Set(ByVal value As T)
                SyncLock _valueLock
                    Me._item = value
                End SyncLock
            End Set
        End Property

        Public Sub New(ByVal value As T)
            Me._item = value
        End Sub

        Friend Sub New(ByVal list As ConcurrentLinkedList(Of T), ByVal value As T)
            _list = list
            Me._item = value
        End Sub

        Friend Sub Invalidate()
            SyncLock _valueLock
                _list = Nothing
                _next = Nothing
                _prev = Nothing
            End SyncLock
        End Sub
    End Class
End Namespace
