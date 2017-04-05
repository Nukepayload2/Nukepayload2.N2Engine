Imports System.Collections.Specialized

Namespace Specialized
    ''' <summary>
    ''' 为 CollectionChanged2D 事件提供数据。
    ''' </summary>
    Public Class NotifyCollectionChanged2DEventArgs(Of T)
        Inherits EventArgs

        Public ReadOnly Property Action As NotifyCollectionChangedAction
        Public ReadOnly Property NewItems As T(,)
        Public ReadOnly Property NewStartingIndex As (RowIndex As Integer, ColIndex As Integer) = (-1, -1)
        Public ReadOnly Property OldItems As T(,)
        Public ReadOnly Property OldStartingIndex As (RowIndex As Integer, ColIndex As Integer) = (-1, -1)

        Sub New(action As NotifyCollectionChangedAction)
            If action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("操作应当为 Reset。", NameOf(action))
            End If
            InitializeAdd(action, Nothing, (-1, -1))
        End Sub

        Sub New(action As NotifyCollectionChangedAction, changedItem As T)
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("操作应当为 Reset，Add 或 Remove。", NameOf(action))
            End If
            If action <> NotifyCollectionChangedAction.Reset Then
                InitializeAddOrRemove(action, {{changedItem}}, (-1, -1))
                Return
            End If
            If changedItem IsNot Nothing Then
                Throw New ArgumentException("Reset 操作需要空的 changedItem。", NameOf(action))
            End If
            InitializeAdd(action, Nothing, (-1, -1))
        End Sub

        Private Function HasNotNegativeOne(value As (RowIndex As Integer, ColIndex As Integer)) As Boolean
            Return value.Item1 <> -1 OrElse value.Item2 <> -1
        End Function

        Private Function HasLessThanNegativeOne(value As (RowIndex As Integer, ColIndex As Integer)) As Boolean
            Return value.Item1 < -1 OrElse value.Item2 < -1
        End Function

        Private Function HasLessThanZero(value As (RowIndex As Integer, ColIndex As Integer)) As Boolean
            Return value.Item1 < 0 OrElse value.Item2 < 0
        End Function

        Sub New(action As NotifyCollectionChangedAction, changedItem As T, index As (RowIndex As Integer, ColIndex As Integer))
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("操作应当为 Reset，Add 或 Remove。", NameOf(action))
            End If
            If action <> NotifyCollectionChangedAction.Reset Then
                InitializeAddOrRemove(action, {{changedItem}}, index)
                Return
            End If
            If changedItem IsNot Nothing Then
                Throw New ArgumentException("Reset 操作需要空的 changedItem。", NameOf(action))
            End If
            If HasNotNegativeOne(index) Then
                Throw New ArgumentException("Reset 操作需要 (-1, -1) 下标。", NameOf(action))
            End If
            InitializeAdd(action, Nothing, (-1, -1))
        End Sub

        Sub New(action As NotifyCollectionChangedAction, changedItems As T(,))
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("操作应当为 Reset，Add 或 Remove。", NameOf(action))
            End If
            If action <> NotifyCollectionChangedAction.Reset Then
                If changedItems Is Nothing Then
                    Throw New ArgumentNullException(NameOf(changedItems))
                End If
                InitializeAddOrRemove(action, changedItems, (-1, -1))
                Return
            End If
            If changedItems IsNot Nothing Then
                Throw New ArgumentException("Reset 操作需要空的 changedItem。", NameOf(action))
            End If
            InitializeAdd(action, Nothing, (-1, -1))
        End Sub

        Sub New(action As NotifyCollectionChangedAction, changedItems As T(,), startingIndex As (RowIndex As Integer, ColIndex As Integer))
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("操作应当为 Reset，Add 或 Remove。", NameOf(action))
            End If
            If action <> NotifyCollectionChangedAction.Reset Then
                If changedItems Is Nothing Then
                    Throw New ArgumentNullException(NameOf(changedItems))
                End If
                If HasLessThanNegativeOne(startingIndex) Then
                    Throw New ArgumentException("下标不得为负数。", NameOf(startingIndex))
                End If
                InitializeAddOrRemove(action, changedItems, startingIndex)
                Return
            End If
            If changedItems IsNot Nothing Then
                Throw New ArgumentException("Reset 操作需要空的 changedItem。", NameOf(action))
            End If
            If HasNotNegativeOne(startingIndex) Then
                Throw New ArgumentException("Reset 操作需要 (-1, -1) 下标。", NameOf(action))
            End If
            InitializeAdd(action, Nothing, (-1, -1))
        End Sub

        Sub New(action As NotifyCollectionChangedAction, newItem As T, oldItem As T)
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("必须是替换", NameOf(action))
            End If
            Dim objArray(,) As T = {{newItem}}
            Dim objArray1(,) As T = {{oldItem}}
            InitializeMoveOrReplace(action, objArray, objArray1, (-1, -1), (-1, -1))
        End Sub
        ''' <summary>
        ''' 在指定位置替换数据的步骤
        ''' </summary>
        Sub New(action As NotifyCollectionChangedAction, newItem As T, oldItem As T, index As (RowIndex As Integer, ColIndex As Integer))
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("必须是替换", NameOf(action))
            End If
            Dim num As (RowIndex As Integer, ColIndex As Integer) = index
            Dim objArray(,) As T = {{newItem}}
            Dim objArray1(,) As T = {{oldItem}}
            InitializeMoveOrReplace(action, objArray, objArray1, index, num)
        End Sub

        Sub New(action As NotifyCollectionChangedAction, newItems As T(,), oldItems As T(,))
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("必须是替换", NameOf(action))
            End If
            If newItems Is Nothing Then
                Throw New ArgumentNullException(NameOf(newItems))
            End If
            If oldItems Is Nothing Then
                Throw New ArgumentNullException(NameOf(oldItems))
            End If
            InitializeMoveOrReplace(action, newItems, oldItems, (-1, -1), (-1, -1))
        End Sub

        Sub New(action As NotifyCollectionChangedAction, newItems As T(,), oldItems As T(,), startingIndex As (RowIndex As Integer, ColIndex As Integer))
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("必须是替换", NameOf(action))
            End If
            If newItems Is Nothing Then
                Throw New ArgumentNullException(NameOf(newItems))
            End If
            If oldItems Is Nothing Then
                Throw New ArgumentNullException(NameOf(oldItems))
            End If
            InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex)
        End Sub

        Sub New(action As NotifyCollectionChangedAction, changedItem As T, index As (RowIndex As Integer, ColIndex As Integer), oldIndex As (RowIndex As Integer, ColIndex As Integer))
            If action <> NotifyCollectionChangedAction.Move Then
                Throw New ArgumentException("必须是移动", NameOf(action))
            End If
            If HasLessThanZero(index) Then
                Throw New ArgumentException("下标不得为负数。", NameOf(index))
            End If
            Dim objArray(,) As T = {{changedItem}}
            InitializeMoveOrReplace(action, objArray, objArray, index, oldIndex)
        End Sub

        Sub New(action As NotifyCollectionChangedAction, changedItems As T(,), index As (RowIndex As Integer, ColIndex As Integer), oldIndex As (RowIndex As Integer, ColIndex As Integer))
            If action <> NotifyCollectionChangedAction.Move Then
                Throw New ArgumentException("必须是移动", NameOf(action))
            End If
            If HasLessThanZero(index) Then
                Throw New ArgumentException("下标不得为负数。", NameOf(index))
            End If
            InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex)
        End Sub

        Friend Sub New(action As NotifyCollectionChangedAction, newItems As T(,), oldItems As T(,), newIndex As (RowIndex As Integer, ColIndex As Integer), oldIndex As (RowIndex As Integer, ColIndex As Integer))
            Dim lists As T(,)
            Dim lists1 As T(,)
            _Action = action
            If newItems Is Nothing Then
                lists = Nothing
            Else
                lists = (newItems)
            End If
            _NewItems = lists
            If oldItems Is Nothing Then
                lists1 = Nothing
            Else
                lists1 = (oldItems)
            End If
            _OldItems = lists1
            _NewStartingIndex = newIndex
            _OldStartingIndex = oldIndex
        End Sub

        Private Sub InitializeAdd(action As NotifyCollectionChangedAction, newItems As T(,), newStartingIndex As (RowIndex As Integer, ColIndex As Integer))
            Dim lists As T(,)
            _Action = action
            If newItems Is Nothing Then
                lists = Nothing
            Else
                lists = (newItems)
            End If
            _NewItems = lists
            _NewStartingIndex = newStartingIndex
        End Sub

        Private Sub InitializeAddOrRemove(action As NotifyCollectionChangedAction, changedItems As T(,), startingIndex As (RowIndex As Integer, ColIndex As Integer))
            If action = NotifyCollectionChangedAction.Add Then
                InitializeAdd(action, changedItems, startingIndex)
                Return
            End If
            If action = NotifyCollectionChangedAction.Remove Then
                InitializeRemove(action, changedItems, startingIndex)
            End If
        End Sub

        Private Sub InitializeMoveOrReplace(action As NotifyCollectionChangedAction, newItems As T(,), oldItems As T(,), startingIndex As (RowIndex As Integer, ColIndex As Integer), oldStartingIndex As (RowIndex As Integer, ColIndex As Integer))
            InitializeAdd(action, newItems, startingIndex)
            InitializeRemove(action, oldItems, oldStartingIndex)
        End Sub

        Private Sub InitializeRemove(action As NotifyCollectionChangedAction, oldItems As T(,), oldStartingIndex As (RowIndex As Integer, ColIndex As Integer))
            Dim lists As T(,)
            _Action = action
            If oldItems Is Nothing Then
                lists = Nothing
            Else
                lists = oldItems
            End If
            _OldItems = lists
            _OldStartingIndex = oldStartingIndex
        End Sub
    End Class
End Namespace
