Option Strict On

Imports System.Linq.Expressions
Imports System.Reflection

Namespace Foundation.Preview
    ''' <summary>
    ''' 表示每次值变更都会缓存值的强类型数据绑定。
    ''' 设定数据源和路径消耗最多的时间，设置值消耗的时间次之，读取值消耗的时间比 <see cref="PropertyBinder(Of T)"/> 少。
    ''' </summary>
    ''' <typeparam name="TDeclaringType"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    Public Class CachedPropertyBinder(Of TValue)
        Implements INotifyPropertyChanged

        Private _setValue As Action(Of INotifyPropertyChanged, TValue) = AddressOf DefaultSetValue

        Private Sub DefaultSetValue(arg1 As INotifyPropertyChanged, arg2 As TValue)
        End Sub

        Private _getValue As Func(Of INotifyPropertyChanged, TValue) = AddressOf DefaultGetValue

        Private Function DefaultGetValue(arg As INotifyPropertyChanged) As TValue
        End Function

        Private _DataSource As INotifyPropertyChanged
        Private _Path As String
        Private _Value As TValue

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property Path As String
            Get
                Return _Path
            End Get
            Set(value As String)
                _Path = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Path)))
                Dim ds = DataSource
                If ds Is Nothing OrElse value Is Nothing Then
                    _setValue = AddressOf DefaultSetValue
                    _getValue = AddressOf DefaultGetValue
                Else
                    CompilePropertyAccess(ds.GetType, value)
                End If
                _Value = _getValue(ds)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Me.Value)))
            End Set
        End Property

        Public Property DataSource As INotifyPropertyChanged
            Get
                Return _DataSource
            End Get
            Set(value As INotifyPropertyChanged)
                If _DataSource IsNot Nothing Then
                    RemoveHandler _DataSource.PropertyChanged, AddressOf OnValueChanged
                End If
                _DataSource = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(DataSource)))
                If value IsNot Nothing Then
                    AddHandler value.PropertyChanged, AddressOf OnValueChanged
                    If String.IsNullOrEmpty(Path) Then
                        _setValue = AddressOf DefaultSetValue
                        _getValue = AddressOf DefaultGetValue
                    Else
                        CompilePropertyAccess(value.GetType, Path)
                    End If
                End If
                _Value = _getValue(value)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Me.Value)))
            End Set
        End Property

        Public Sub SetBinding(source As INotifyPropertyChanged, path As String)
            If _DataSource IsNot Nothing Then
                RemoveHandler _DataSource.PropertyChanged, AddressOf OnValueChanged
            End If
            _DataSource = source
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(DataSource)))
            _Path = path
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Me.Path)))
            If source Is Nothing Then
                _setValue = AddressOf DefaultSetValue
                _getValue = AddressOf DefaultGetValue
            Else
                AddHandler source.PropertyChanged, AddressOf OnValueChanged
                If String.IsNullOrEmpty(path) Then
                    _setValue = AddressOf DefaultSetValue
                    _getValue = AddressOf DefaultGetValue
                Else
                    CompilePropertyAccess(source.GetType, path)
                End If
            End If
            _Value = _getValue(source)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Me.Value)))
        End Sub

        Private Sub CompilePropertyAccess(modelType As Type, propPath As String)
            Dim pThis As ParameterExpression = Expression.Parameter(GetType(INotifyPropertyChanged))
            Dim pValue As ParameterExpression = Expression.Parameter(GetType(TValue))
            Dim setBody = Expression.Call(Expression.Convert(pThis, modelType), modelType.GetRuntimeProperty(propPath).SetMethod, pValue)
            Dim setLambda = Expression.Lambda(Of Action(Of INotifyPropertyChanged, TValue))(setBody, {pThis, pValue})
            _setValue = setLambda.Compile
            Dim getBody = Expression.Property(Expression.Convert(pThis, modelType), propPath)
            Dim getLambda = Expression.Lambda(Of Func(Of INotifyPropertyChanged, TValue))(getBody, {pThis})
            _getValue = getLambda.Compile
        End Sub

        Public Property Value As TValue
            Get
                Return _Value
            End Get
            Set(value As TValue)
                _Value = value
                _setValue(DataSource, value)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Me.Value)))
            End Set
        End Property

        Private Sub OnValueChanged(sender As Object, e As PropertyChangedEventArgs)
            If e.PropertyName = Path Then
                _Value = _getValue(DataSource)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Me.Value)))
            End If
        End Sub
    End Class
End Namespace