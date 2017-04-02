Namespace Models
    ''' <summary>
    ''' 菜单数据模板信息
    ''' </summary>
    Public Class MenuDataTemplateInformation
        ''' <summary>
        ''' 初始化菜单数据模板信息。
        ''' </summary>
        Sub New()

        End Sub
        ''' <summary>
        ''' 初始化菜单数据模板信息。
        ''' </summary>
        ''' <param name="name">名称或 Uid</param>
        ''' <param name="description">描述这个菜单的功能</param>
        ''' <param name="key">数据模板的键。</param>
        ''' <param name="resourceDictionary">模板在哪个资源字典里</param>
        Sub New(name As String, description As String, key As String, resourceDictionary As String)
            Me.Name = name
            Me.Description = description
            Me.Key = key
            Me.ResourceDictionary = New Uri(resourceDictionary)
        End Sub

        ''' <summary>
        ''' 名称或 Uid
        ''' </summary>
        Public Property Name As String
        ''' <summary>
        ''' 描述这个菜单的功能
        ''' </summary>
        Public Property Description As String
        ''' <summary>
        ''' 数据模板的键。
        ''' </summary>
        Public Property Key As String
        ''' <summary>
        ''' 模板在哪个资源字典里
        ''' </summary>
        Public Property ResourceDictionary As Uri
    End Class
End Namespace
