''' <summary>
''' 表示角色数值、物品或技能的可用量计数器
''' </summary>
Public Class RemainingCounter
    ''' <summary>
    ''' 可用的数量计数,若无效表示数量无限
    ''' </summary>
    Public Property Quantity As AvaliableValue(Of Integer)
    ''' <summary>
    ''' 可用的时间计数,若无效表示时间永久
    ''' </summary>
    Public Property Time As AvaliableValue(Of Date)
End Class
