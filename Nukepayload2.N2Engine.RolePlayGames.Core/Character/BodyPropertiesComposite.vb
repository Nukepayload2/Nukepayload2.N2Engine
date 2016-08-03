Public Class BodyPropertiesComposite
    ''' <summary>
    ''' 体力
    ''' </summary>
    Public Property Stamina As Composite(Of Integer)
    ''' <summary>
    ''' 力量
    ''' </summary>
    Public Property Strength As Composite(Of Integer)
    ''' <summary>
    ''' 魔法
    ''' </summary>
    Public Property Magic As Composite(Of Integer)
    ''' <summary>
    ''' 速度
    ''' </summary>
    Public Property Speed As Composite(Of Integer)
    ''' <summary>
    ''' 幸运
    ''' </summary>
    Public Property Luck As Composite(Of Integer)

    ''' <summary>
    ''' 生命值
    ''' </summary>
    Public Property HP As ValueRange(Of Composite(Of Integer))
    ''' <summary>
    ''' 法力值
    ''' </summary>
    Public Property MP As ValueRange(Of Composite(Of Integer))
    ''' <summary>
    ''' 气值
    ''' </summary>
    Public Property DP As ValueRange(Of Composite(Of Integer))
    ''' <summary>
    ''' 等级
    ''' </summary>
    Public Property Level As ValueRange(Of Composite(Of Integer))
    ''' <summary>
    ''' 经验
    ''' </summary>
    Public Property Experience As ValueRange(Of Composite(Of Integer))

End Class
