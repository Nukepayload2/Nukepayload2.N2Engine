Namespace Platform
    ''' <summary>
    ''' 表示指定的类型不会被注册为平台实现类型。
    ''' </summary>
    <AttributeUsage(AttributeTargets.Class)>
    Public Class RegistrationIgnoreAttribute
        Inherits Attribute

    End Class

End Namespace