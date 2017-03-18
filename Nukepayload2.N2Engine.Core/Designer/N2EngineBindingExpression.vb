Namespace Designer

    Public Class N2EngineBindingExpression
        Implements IN2EngineBindingExpression
        Implements INotifyPropertyChanged

        Dim _GetCode As String
        Public Property GetCode As String Implements IN2EngineBindingExpression.GetCode
            Get
                Return _GetCode
            End Get
            Set(value As String)
                _GetCode = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(GetCode)))
            End Set
        End Property

        Dim _SetCode As String
        Public Property SetCode As String Implements IN2EngineBindingExpression.SetCode
            Get
                Return _SetCode
            End Get
            Set(value As String)
                _SetCode = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SetCode)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace