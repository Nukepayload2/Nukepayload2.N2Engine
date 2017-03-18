' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports Nukepayload2.N2Engine.Designer

Public NotInheritable Class PropertyBinderEditor
    Inherits UserControl

    Public Property N2BindingExpression As IN2EngineBindingExpression
        Get
            Return GetValue(N2BindingExpressionProperty)
        End Get
        Set
            SetValue(N2BindingExpressionProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly N2BindingExpressionProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(N2BindingExpression),
                           GetType(IN2EngineBindingExpression), GetType(PropertyBinderEditor),
                           New PropertyMetadata(Nothing, Sub(s, e) DirectCast(s, PropertyBinderEditor).DataContext = e.NewValue))

End Class
