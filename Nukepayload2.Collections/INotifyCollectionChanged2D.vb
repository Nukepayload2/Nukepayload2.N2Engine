Namespace Specialized
    ''' <summary>
    ''' 描述一个二维的集合发生变更
    ''' </summary>
    Public Interface INotifyCollectionChanged2D(Of T)
        Event CollectionChanged2D As EventHandler(Of NotifyCollectionChanged2DEventArgs(Of T))
    End Interface

End Namespace