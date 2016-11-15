Imports System.Collections.Specialized
Imports Nukepayload2.Collections.Concurrent

Namespace UI.Elements
    Public Class GameVisualContainer
        Inherits GameVisual

        Private _Children As New ConcurrentObservableCollection(Of GameVisual)
        ''' <summary>
        ''' 画板的子元素。添加或删除元素时会在此画板引发一个通知。
        ''' </summary>
        Public ReadOnly Property Children As IEnumerable(Of GameVisual)
            Get
                Return _Children
            End Get
        End Property
        Sub New()
            GameVisualTreeHelper.AddChildrenChangedHandler(Of GameVisual, ConcurrentObservableCollection(Of GameVisual))(_Children)
        End Sub
        ''' <summary>
        ''' 添加一个新的 <see cref="GameVisual"/> 
        ''' </summary>
        Public Sub AddVisual(visual As GameVisual)
            _Children.Add(visual)
        End Sub
        ''' <summary>
        ''' 添加一组新的 <see cref="GameVisual"/> 
        ''' </summary>
        Public Sub AddRange(visuals As IEnumerable(Of GameVisual))
            For Each visual In visuals
                _Children.Add(visual)
            Next
        End Sub
        ''' <summary>
        ''' 删除指定的 <see cref="GameVisual"/> 
        ''' </summary>
        Public Sub Remove(visual As GameVisual)
            _Children.Remove(visual)
        End Sub
        ''' <summary>
        ''' 访问子元素
        ''' </summary>
        Default Property Item(index As Integer) As GameVisual
            Get
                Return _Children(index)
            End Get
            Set(value As GameVisual)
                _Children(index) = value
            End Set
        End Property
        ''' <summary>
        ''' 有多少个子元素
        ''' </summary>
        Public ReadOnly Property Count As Integer
            Get
                Return _Children.Count
            End Get
        End Property
        ''' <summary>
        ''' 移除全部子元素
        ''' </summary>
        Public Sub Clear()
            _Children.Clear()
        End Sub
        ''' <summary>
        ''' 订阅子元素变更的通知
        ''' </summary>
        Public Sub RegisterOnChildrenChanged(collectionChanged As NotifyCollectionChangedEventHandler)
            AddHandler _Children.CollectionChanged, collectionChanged
        End Sub
        ''' <summary>
        ''' 取消订阅子元素变更的通知
        ''' </summary>
        Public Sub UnregisterOnChildrenChanged(collectionChanged As NotifyCollectionChangedEventHandler)
            RemoveHandler _Children.CollectionChanged, collectionChanged
        End Sub
    End Class
End Namespace