﻿Namespace Foundation
    ''' <summary>
    ''' 强类型的轻量级的数据绑定。功能不如弱类型版本灵活，但是性能接近编译的绑定。
    ''' </summary>
    Public Class PropertyBinder(Of T)
        ''' <summary>
        ''' 创建一个空的绑定器
        ''' </summary>
        Sub New()

        End Sub
        ''' <summary>
        ''' 创建一个一次性绑定
        ''' </summary>
        ''' <param name="value">读取时获取的固定的值</param>
        Sub New(value As T)
            Bind(value)
        End Sub
        ''' <summary>
        ''' 创建单向绑定
        ''' </summary>
        ''' <param name="getter">读取数据源</param>
        Sub New(getter As Func(Of T))
            Bind(getter)
        End Sub
        ''' <summary>
        ''' 创建反向单向绑定
        ''' </summary>
        ''' <param name="value">读取时获取的固定的值</param>
        ''' <param name="setter">写入数据源</param>
        Sub New(value As T, setter As Action(Of T))
            Bind(value, setter)
        End Sub
        ''' <summary>
        ''' 创建双向绑定
        ''' </summary>
        ''' <param name="getter">读取数据源</param>
        ''' <param name="setter">写入数据源</param>
        Sub New(getter As Func(Of T), setter As Action(Of T))
            Bind(getter, setter)
        End Sub
        ''' <summary>
        ''' 绑定到另一个绑定器
        ''' </summary>
        Sub New(another As PropertyBinder(Of T))
            Bind(another)
        End Sub

        ''' <summary>
        ''' 用于获取属性值
        ''' </summary>
        Public ReadOnly Property Getter As Func(Of T)
        ''' <summary>
        ''' 用于设定属性值
        ''' </summary>
        Public ReadOnly Property Setter As Action(Of T)
        ''' <summary>
        ''' 检查是否可以读取值
        ''' </summary>
        Public ReadOnly Property CanRead As Boolean
            Get
                Return Getter IsNot Nothing
            End Get
        End Property
        ''' <summary>
        ''' 检查是否可以写入值
        ''' </summary>
        Public ReadOnly Property CanWrite As Boolean
            Get
                Return Setter IsNot Nothing
            End Get
        End Property
        ''' <summary>
        ''' 获取或设置绑定的数据值。默认情况下未定义对应的数据绑定会引发运行时异常。如果取消定义编译常量 THROW_IF_DATA_BINDING_FAILED，此时缺少 <see cref="Getter"/> 则返回对应类型的默认值，如果缺少 <see cref="Setter"/> ，则会丢失写入更改。
        ''' </summary>
        Public Property Value As T
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
#If THROW_IF_DATA_BINDING_FAILED Then
                Return Getter.Invoke
#Else
                If Getter Is Nothing Then
                    Debug.WriteLine("尝试获取没有值的属性。属性类型为：" + GetType(T).Name)
                    Return Nothing
                Else
                    Return Getter.Invoke
                End If
#End If
            End Get
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Set(value As T)
#If THROW_IF_DATA_BINDING_FAILED Then
                Setter.Invoke(value)
#Else
                If Setter IsNot Nothing Then
                    Setter.Invoke(value)
                Else
                    Debug.WriteLine("丢失更改：" + value.ToString)
                End If
#End If
            End Set
        End Property
        ''' <summary>
        ''' 如果没有绑定数据，则返回空值（引用类型）或 默认值（值类型）。
        ''' </summary>
        Public Function GetValueOrDefault() As T
            Return If(Getter Is Nothing， Nothing， Getter.Invoke)
        End Function
        ''' <summary>
        ''' 创建单向绑定
        ''' </summary>
        Public Sub Bind(getter As Func(Of T))
            _Getter = getter
            _Setter = Nothing
        End Sub
        ''' <summary>
        ''' 创建反向单向绑定
        ''' </summary>
        Public Sub Bind(value As T, setter As Action(Of T))
            _Getter = Function() value
            _Setter = setter
        End Sub
        ''' <summary>
        ''' 创建双向绑定
        ''' </summary>
        Public Sub Bind(getter As Func(Of T), setter As Action(Of T))
            _Getter = getter
            _Setter = setter
        End Sub
        ''' <summary>
        ''' 仅获取一次数据的单项绑定
        ''' </summary>
        Public Sub Bind(value As T)
            _Getter = Function() value
            _Setter = Nothing
        End Sub

        Public Shared Narrowing Operator CType(binder As PropertyBinder(Of T)) As T
            Return binder.Value
        End Operator

        Public Shared Widening Operator CType(val As T) As PropertyBinder(Of T)
            Return New PropertyBinder(Of T)(val)
        End Operator

        ''' <summary>
        ''' 绑定到另一个绑定器
        ''' </summary>
        Public Sub Bind(another As PropertyBinder(Of T))
            Bind(Function() another.Getter.Invoke, Sub(d) another.Setter.Invoke(d))
        End Sub
        ''' <summary>
        ''' 绑定的数据有变化
        ''' </summary>
        Public Event DataChanged As EventHandler(Of PropertyBinderDataChangedEventArgs(Of T))
        ''' <summary>
        ''' 绑定的数据变动后，手动报告数据已经改动。
        ''' </summary>
        Public Sub ReportDataChanged(oldValue As T, newValue As T)
            RaiseEvent DataChanged(Me, New PropertyBinderDataChangedEventArgs(Of T)(oldValue, newValue))
        End Sub
    End Class
End Namespace