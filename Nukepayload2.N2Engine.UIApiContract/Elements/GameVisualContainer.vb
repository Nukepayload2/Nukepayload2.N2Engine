Imports System.Collections.Specialized
Imports Nukepayload2.Collections.Concurrent
Imports Nukepayload2.N2Engine.UI.Effects

Namespace UI.Elements
    Public Class GameVisualContainer
        Inherits GameVisual

        Private _Children As New ConcurrentObservableCollection(Of GameVisual)
        ''' <summary>
        ''' 画板的子元素。添加或删除元素时会在此画板引发一个通知。
        ''' </summary>
        Public ReadOnly Property Children As IList(Of GameVisual)
            Get
                Return _Children
            End Get
        End Property

        ''' <summary>
        ''' 获取渲染时的大小
        ''' </summary>
        Public ReadOnly Property RenderSize As Vector2
            Get
                Dim size As Vector2
                If Me.Size.CanRead AndAlso Me.Size.Value.X > 1 Then
                    size = Me.Size.Value
                Else
                    size = Information.BackBufferInformation.ScreenSize.ToVector2
                End If
                Return size
            End Get
        End Property

        Public Overrides Function GetSubNodes() As IEnumerable(Of GameVisual)
            Return Children
        End Function

        Sub New()
            GameVisualTreeHelper.RemoveChildrenWhenChildRequested(Me)
            GameVisualTreeHelper.ModifyParentOnChildrenChanged(Me)
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

        Public Overrides Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
            Return Children
        End Function
    End Class
End Namespace