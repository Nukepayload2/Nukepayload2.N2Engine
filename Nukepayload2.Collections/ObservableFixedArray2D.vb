Imports System.Collections.Specialized

Namespace Specialized
    ''' <summary>
    ''' 对内容进行替换会引发通知的定长二维数组。
    ''' </summary>
    Public Class ObservableFixedArray2D(Of T)
        Implements INotifyCollectionChanged2D(Of T), ICollection, IStructuralComparable, IStructuralEquatable

        Friend _data As T(,)

        Public ReadOnly Property Count As Integer Implements ICollection.Count
            Get
                Return DirectCast(_data, ICollection).Count
            End Get
        End Property

        Public ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                Return DirectCast(_data, ICollection).IsSynchronized
            End Get
        End Property

        Public ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Return DirectCast(_data, ICollection).SyncRoot
            End Get
        End Property

        Default Public Property Item(rowIndex As Integer, colIndex As Integer) As T
            Get
                Return _data(rowIndex, colIndex)
            End Get
            Set(value As T)
                Dim old = _data(rowIndex, colIndex)
                _data(rowIndex, colIndex) = value
                RaiseEvent CollectionChanged2D(Me, New NotifyCollectionChanged2DEventArgs(Of T)(
                                               NotifyCollectionChangedAction.Replace,
                                               value, old, (rowIndex, colIndex)))
            End Set
        End Property

        Public ReadOnly Property RawData As T(,)
            Get
                Return _data
            End Get
        End Property

        Public Function InRange(rowIndex As Integer, colIndex As Integer) As Boolean
            If _data Is Nothing Then
                Return False
            End If
            If rowIndex < 0 OrElse colIndex < 0 Then
                Return False
            End If
            If rowIndex < RowCount AndAlso colIndex < ColumnCount Then
                Return True
            End If
            Return False
        End Function

        Sub New()

        End Sub

        Sub New(rowUpperBound As Integer, columnUpperBound As Integer)
            ReDim _data(rowUpperBound, columnUpperBound)
        End Sub

        Sub New(data As T(,))
            _data = data
        End Sub

        Public ReadOnly Property RowCount As Integer
            Get
                If _data Is Nothing Then
                    Return 0
                End If
                Return _data.GetLength(0)
            End Get
        End Property

        Public ReadOnly Property ColumnCount As Integer
            Get
                If _data Is Nothing Then
                    Return 0
                End If
                Return _data.GetLength(1)
            End Get
        End Property

        Public Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
            DirectCast(_data, ICollection).CopyTo(array, index)
        End Sub

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _data.GetEnumerator()
        End Function

        Public Function CompareTo(other As Object, comparer As IComparer) As Integer Implements IStructuralComparable.CompareTo
            Return DirectCast(_data, IStructuralComparable).CompareTo(other, comparer)
        End Function

        Public Overloads Function Equals(other As Object, comparer As IEqualityComparer) As Boolean Implements IStructuralEquatable.Equals
            Return DirectCast(_data, IStructuralEquatable).Equals(other, comparer)
        End Function

        Public Overloads Function GetHashCode(comparer As IEqualityComparer) As Integer Implements IStructuralEquatable.GetHashCode
            Return DirectCast(_data, IStructuralEquatable).GetHashCode(comparer)
        End Function

        Public Event CollectionChanged2D As EventHandler(Of NotifyCollectionChanged2DEventArgs(Of T)) Implements INotifyCollectionChanged2D(Of T).CollectionChanged2D
    End Class

    Public Module ObservableFixedArray2DExtensions

        ''' <summary>
        ''' 更改数组的大小, 丢弃原有数据。
        ''' </summary>
        <Extension>
        Public Sub [ReDim](Of T)(ByRef arr As ObservableFixedArray2D(Of T), rowIndex As Integer, colIndex As Integer)
            If arr Is Nothing Then
                arr = New ObservableFixedArray2D(Of T)
            End If
            ReDim arr._data(rowIndex, colIndex)
        End Sub
        ''' <summary>
        ''' 更改数组的大小, 然后将原有数据尽可能地复制到新的数组。
        ''' </summary>
        <Extension>
        Public Sub ReDimPreserve(Of T)(ByRef arr As ObservableFixedArray2D(Of T), rowIndex As Integer, colIndex As Integer)
            If arr Is Nothing Then
                arr = New ObservableFixedArray2D(Of T)
            End If
            ReDim Preserve arr._data(rowIndex, colIndex)
        End Sub

    End Module
End Namespace