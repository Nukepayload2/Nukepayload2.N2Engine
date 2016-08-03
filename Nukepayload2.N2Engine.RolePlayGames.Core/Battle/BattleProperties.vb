Public Class BattleProperties

    ''' <summary>
    ''' 物理攻击
    ''' </summary>
    Public Property PhysicsAttack As CompositedValue(Of Integer)
    ''' <summary>
    ''' 基础魔法攻击
    ''' </summary>
    Public Property BaseElementAttack As CompositedValue(Of Integer)
    ''' <summary>
    ''' 物理防御
    ''' </summary>
    Public Property PhysicsDefend As CompositedValue(Of Integer)
    ''' <summary>
    ''' 基础魔法防御
    ''' </summary>
    Public Property BaseElementDefend As CompositedValue(Of Integer)
    ''' <summary>
    ''' 命中率
    ''' </summary>
    Public Property HitRate As CompositedValue(Of Single)
    ''' <summary>
    ''' 闪避率
    ''' </summary>
    Public Property DodgeRate As CompositedValue(Of Single)
    ''' <summary>
    ''' 格挡率
    ''' </summary>
    Public Property BlockRate As CompositedValue(Of Single)
    ''' <summary>
    ''' 重击率
    ''' </summary>
    Public Property CriticalRate As CompositedValue(Of Single)
    ''' <summary>
    ''' 敏捷度。决定进度条的速度。
    ''' </summary>
    Public Property Agility As CompositedValue(Of Single)
    ''' <summary>
    ''' 分开计算的魔法伤害
    ''' </summary>
    Public Property ElementAttacks As Dictionary(Of ElementPhase, CompositedValue(Of Single))
    ''' <summary>
    ''' 分开计算的魔法防御
    ''' </summary>
    Public Property ElementDefence As Dictionary(Of ElementPhase, CompositedValue(Of Single))
End Class
