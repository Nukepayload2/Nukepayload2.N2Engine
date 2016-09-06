''' <summary>
''' 可设置数据有效性的数据类型
''' </summary>
Public Structure AvaliableValue(Of T)
    Public Property Value As T
    Public Property Enabled As Boolean
End Structure
