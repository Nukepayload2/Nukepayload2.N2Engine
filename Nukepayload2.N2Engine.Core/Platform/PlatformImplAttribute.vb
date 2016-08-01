''' <summary>
''' 实现一个类型时, 声明与之对应的需要实现的类型
''' </summary>
<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Interface)>
Public Class PlatformImplAttribute
    Inherits Attribute
    Sub New(viewType As Type)
        Me.ViewType = viewType
    End Sub

    ''' <summary>
    ''' 表示一个联系类型
    ''' </summary>
    Public Property ViewType As Type
End Class