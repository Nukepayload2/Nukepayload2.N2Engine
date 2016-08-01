''' <summary>
''' 用于平台实现自动化
''' </summary>
<AttributeUsage(AttributeTargets.Class)>
Public Class PlatformContractAttribute
    Inherits Attribute
    ''' <summary>
    ''' 注册联系类型，辅助平台实现自动化
    ''' </summary>
    ''' <param name="contractType">对应的联系类型</param>
    Sub New(contractType As Type)
        Me.ContractType = contractType
    End Sub

    Public ReadOnly Property ContractType As Type

End Class