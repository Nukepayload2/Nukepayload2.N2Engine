Imports Nukepayload2.N2Engine.Resources
''' <summary>
''' 游戏角色数据接口
''' </summary>
Public Interface ICharacter
    Property Abilities As List(Of Ability)
    Property HP As RemainingCounter
    Property Image As ResourceId
    Property Location As Location
End Interface
