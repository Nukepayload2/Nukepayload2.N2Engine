Namespace UI.Elements
    Public MustInherit Class GameLayer
        Inherits GameVisual

    End Class
    Public MustInherit Class GameLayer(Of TVisual As GameVisual)
        Inherits GameLayer
        ''' <summary>
        ''' 画板的子元素
        ''' </summary>
        Public ReadOnly Property Children As New ObservableCollection(Of TVisual)
        Sub New()
            VisualTreeHelper.AddChildrenChangedHandler(Children)
        End Sub
    End Class
End Namespace