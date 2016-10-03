Namespace UI.Elements
    Public Class GameVisualContainter(Of TVisual As GameVisual)
        Inherits GameVisual
        ''' <summary>
        ''' 画板的子元素
        ''' </summary>
        Public ReadOnly Property Children As New ObservableCollection(Of TVisual)
        Sub New()
            VisualTreeHelper.AddChildrenChangedHandler(Children)
        End Sub
    End Class
End Namespace