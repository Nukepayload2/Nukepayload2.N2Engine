Namespace Platform
    ''' <summary>
    ''' 用于创建已注册的平台特定类型
    ''' </summary>
    Public Class PlatformActivator
        ''' <summary>
        ''' 根据参数指定的可移植类型的类型创建平台实现类型 (使用平台实现类型的公共无参数构造函数)
        ''' </summary>
        Public Shared Function CreateInstance(portableType As Type) As Object
            Return Activator.CreateInstance(PlatformImplRegistration.Registered(portableType))
        End Function
        ''' <summary>
        ''' 根据参数指定的可移植类型的类型创建平台实现类型 (参数直接传递到平台实现类型的构造函数)
        ''' </summary>
        Public Shared Function CreateInstance(portableType As Type, ParamArray parameters As Object()) As Object
            Return Activator.CreateInstance(PlatformImplRegistration.Registered(portableType), parameters)
        End Function
        ''' <summary>
        ''' 根据泛型指定的可移植类型的类型创建平台实现类型, 然后强制转换为平台实现的基类型 (使用平台实现类型的公共无参数构造函数)
        ''' </summary>
        ''' <typeparam name="TPortable">可移植类型</typeparam>
        ''' <typeparam name="TPlatformBase">平台类型的基类</typeparam>
        Public Shared Function CreateBaseInstance(Of TPortable, TPlatformBase)() As TPlatformBase
            Return DirectCast(CreateInstance(GetType(TPortable)), TPlatformBase)
        End Function
        ''' <summary>
        ''' 根据泛型指定的可移植类型的类型创建平台实现类型, 然后强制转换为平台实现的基类型 (使用平台实现类型的公共无参数构造函数)
        ''' </summary>
        ''' <typeparam name="TPortable">可移植类型</typeparam>
        Public Shared Function CreateBaseInstance(Of TPortable, TPlatformBase)(ParamArray parameters As Object()) As TPlatformBase
            Return DirectCast(CreateInstance(GetType(TPortable), parameters), TPlatformBase)
        End Function
        ''' <summary>
        ''' 根据泛型指定的可移植类型的类型创建平台实现基类型 (使用平台实现类型的公共无参数构造函数)
        ''' </summary>
        ''' <typeparam name="TPlatformBase">可移植类型</typeparam>
        Public Shared Function CreateBaseInstance(Of TPlatformBase)() As TPlatformBase
            Return DirectCast(CreateInstance(GetType(TPlatformBase)), TPlatformBase)
        End Function
        ''' <summary>
        ''' 根据泛型指定的可移植类型的类型创建平台实现基类型 (使用平台实现类型的公共无参数构造函数)
        ''' </summary>
        ''' <typeparam name="TPlatformBase">可移植类型</typeparam>
        Public Shared Function CreateBaseInstance(Of TPlatformBase)(ParamArray parameters As Object()) As TPlatformBase
            Return DirectCast(CreateInstance(GetType(TPlatformBase), parameters), TPlatformBase)
        End Function
        ''' <summary>
        ''' 根据泛型指定的可移植类型的类型创建平台实现类型 (使用平台实现类型的公共无参数构造函数)
        ''' </summary>
        ''' <typeparam name="TPortable">可移植类型</typeparam>
        Public Shared Function CreateInstance(Of TPortable)() As Object
            Return CreateInstance(GetType(TPortable))
        End Function
        ''' <summary>
        ''' 根据泛型指定的可移植类型的类型创建平台实现类型 (参数直接传递到平台实现类型的构造函数)
        ''' </summary>
        ''' <typeparam name="TPortable">可移植类型</typeparam>
        Public Shared Function CreateInstance(Of TPortable)(ParamArray parameters As Object()) As Object
            Return CreateInstance(GetType(TPortable), parameters)
        End Function
        ''' <summary>
        ''' 根据可移植类型的实例创建平台实现类型 (将可移植类型作为参数传递到平台实现类型的构造函数)
        ''' </summary>
        ''' <param name="portableObject">可移植的对象</param>
        Public Shared Function CreateInstance(portableObject As Object) As Object
            Return Activator.CreateInstance(PlatformImplRegistration.Registered(portableObject.GetType), portableObject)
        End Function
        ''' <summary>
        ''' 根据可移植类型的实例创建平台实现类型 (将可移植类型作为第一个参数，剩余的参数在其后 传递到平台实现类型的构造函数)
        ''' </summary>
        ''' <param name="portableObject">可移植的对象</param>
        Public Shared Function CreateInstance(portableObject As Object, ParamArray parameters As Object()) As Object
            Return Activator.CreateInstance(PlatformImplRegistration.Registered(portableObject.GetType), {portableObject}.Concat(parameters).ToArray())
        End Function
    End Class
End Namespace