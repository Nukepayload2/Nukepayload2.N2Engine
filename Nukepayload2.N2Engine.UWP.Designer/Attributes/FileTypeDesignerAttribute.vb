Imports Nukepayload2.N2Engine.UWP.Designer.Models

Namespace Attributes
    ''' <summary>
    ''' 表示这种控件可以编辑特定的资源。
    ''' </summary>
    <AttributeUsage(AttributeTargets.Class)>
    Public Class FileTypeDesignerAttribute
        Inherits Attribute
        ''' <summary>
        ''' 初始化默认代码文件的编辑器声明。
        ''' </summary>
        Sub New()

        End Sub
        ''' <summary>
        ''' 初始化指定类型的代码文件编辑器声明。
        ''' </summary>
        ''' <param name="supportedCodeType"></param>
        ''' <param name="menuDataTemplateNames"></param>
        Sub New(supportedCodeType As CodeTypes, menuDataTemplateNames() As String)
            Me.SupportedCodeType = supportedCodeType
            Me.MenuDataTemplateNames = menuDataTemplateNames
        End Sub
        ''' <summary>
        ''' 表示这是哪个类型的代码编辑器。
        ''' </summary>
        Public Property SupportedCodeType As CodeTypes
        ''' <summary>
        ''' 控件关联的菜单的模板。
        ''' </summary>
        Public Property MenuDataTemplateNames As String()
        ''' <summary>
        ''' 设计器的 ms-appx 协议图标。
        ''' </summary>
        Public Property Icon As Uri
        ''' <summary>
        ''' 设计器的显示名称
        ''' </summary>
        Public Property Name As String
        ''' <summary>
        ''' 描述这个设计器的功能。
        ''' </summary>
        Public Property Description As String
    End Class

End Namespace