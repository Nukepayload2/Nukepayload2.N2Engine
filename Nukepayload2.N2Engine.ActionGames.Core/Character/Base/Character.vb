Imports Nukepayload2.N2Engine.Resources
''' <summary>
''' 表示游戏角色的基类
''' </summary>
Public Class Character
    Implements ICharacter
    ''' <summary>
    ''' 人物贴图
    ''' </summary>
    Public Property Image As ResourceId Implements ICharacter.Image
    ''' <summary>
    ''' 角色方位
    ''' </summary>
    Public Property Location As Location Implements ICharacter.Location
    ''' <summary>
    ''' 角色名称
    ''' </summary>
    Public Property Name As String
    ''' <summary>
    ''' 生命值
    ''' </summary>
    Public Property HP As RemainingCounter Implements ICharacter.HP
    ''' <summary>
    ''' 能力集合
    ''' </summary>
    Public Property Abilities As List(Of Ability) Implements ICharacter.Abilities
End Class
