''' <summary>
''' 实现一个类型时, 声明与之对应的平台无关类型。用这个标记的类如果会被平台无关类型自动创建，则需要使用 Friend 修饰符 (Visual Basic) 或 internal 修饰符 (Visual C#)。
''' </summary>
<AttributeUsage(AttributeTargets.Class)>
Public Class PlatformImplAttribute
    Inherits Attribute
    Sub New(declType As Type)
        Me.DeclType = declType
    End Sub

    ''' <summary>
    ''' 表示一个不包含实现的类型
    ''' </summary>
    Public Property DeclType As Type
End Class