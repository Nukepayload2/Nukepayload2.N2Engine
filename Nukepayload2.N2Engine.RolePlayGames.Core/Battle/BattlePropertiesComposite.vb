Public Class BattlePropertiesComposite
    ''' <summary>
    ''' 物理攻击
    ''' </summary>
    Public Property PhysicsAttack As Composite(Of Integer)
    ''' <summary>
    ''' 基础魔法攻击
    ''' </summary>
    Public Property BaseElementAttack As Composite(Of Integer)
    ''' <summary>
    ''' 物理防御
    ''' </summary>
    Public Property PhysicsDefend As Composite(Of Integer)
    ''' <summary>
    ''' 基础魔法防御
    ''' </summary>
    Public Property BaseElementDefend As Composite(Of Integer)
    ''' <summary>
    ''' 命中率
    ''' </summary>
    Public Property HitRate As Composite(Of Single)
    ''' <summary>
    ''' 闪避率
    ''' </summary>
    Public Property DodgeRate As Composite(Of Single)
    ''' <summary>
    ''' 格挡率
    ''' </summary>
    Public Property BlockRate As Composite(Of Single)
    ''' <summary>
    ''' 重击率
    ''' </summary>
    Public Property CriticalRate As Composite(Of Single)
    ''' <summary>
    ''' 敏捷度。决定进度条的速度。
    ''' </summary>
    Public Property Agility As Composite(Of Single)
    ''' <summary>
    ''' 分开计算的魔法伤害
    ''' </summary>
    Public Property ElementAttacks As Dictionary(Of ElementPhase, Composite(Of Single))
    ''' <summary>
    ''' 分开计算的魔法防御
    ''' </summary>
    Public Property ElementDefence As Dictionary(Of ElementPhase, Composite(Of Single))
End Class