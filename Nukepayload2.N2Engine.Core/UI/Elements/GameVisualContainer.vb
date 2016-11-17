Imports System.Collections.Specialized
Imports Nukepayload2.Collections.Concurrent

Namespace UI.Elements
    Public Class GameVisualContainer
        Inherits GameVisual

        Dim _Children As New ConcurrentObservableCollection(Of GameVisual)
        ''' <summary>
        ''' 画板的子元素。添加或删除元素时会在此画板引发一个通知。
        ''' </summary>
        Public ReadOnly Property Children As IList(Of GameVisual)
            Get
                Return _Children
            End Get
        End Property
        Sub New()
            GameVisualTreeHelper.AddChildrenChangedHandler(Me)
            GameVisualTreeHelper.AutoHandleParent(Me)
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