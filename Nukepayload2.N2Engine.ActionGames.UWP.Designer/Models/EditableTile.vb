Namespace Models
    ''' <summary>
    ''' 可编辑的图块
    ''' </summary>
    Public Class EditableTile
        Implements INotifyPropertyChanged

        Dim _Name As String
        ''' <summary>
        ''' 名称。会影响生成的代码。
        ''' </summary>
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Name)))
            End Set
        End Property

        Dim _Description As String
        ''' <summary>
        ''' 描述。会影响生成的代码中的注释。
        ''' </summary>
        Public Property Description As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Description)))
            End Set
        End Property

        Dim _Collider As ColliderKinds
        ''' <summary>
        ''' 碰撞器的类型。
        ''' </summary>
        Public Property Collider As ColliderKinds
            Get
                Return _Collider
            End Get
            Set(value As ColliderKinds)
                _Collider = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Collider)))
            End Set
        End Property

        Dim _Sprite As ImageSource
        ''' <summary>
        ''' 从图块表生成的贴图。
        ''' </summary>
        Public Property Sprite As ImageSource
            Get
                Return _Sprite
            End Get
            Set(value As ImageSource)
                _Sprite = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Sprite)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace