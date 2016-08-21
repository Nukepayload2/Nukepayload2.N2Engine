''' <summary>
''' 实现一个类型时, 声明与之对应的平台无关类型。用这个标记的类如果会被平台无关类型自动创建，则需要使用 Friend 修饰符 (Visual Basic) 或 internal 修饰符 (Visual C#)。
''' </summary>
<AttributeUsage(AttributeTargets.Class)>
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