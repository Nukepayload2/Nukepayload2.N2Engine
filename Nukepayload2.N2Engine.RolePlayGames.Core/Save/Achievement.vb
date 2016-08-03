Imports Nukepayload2.N2Engine.Core

Public Class Achievement
    Inherits GameResourceModelBase
    Public Property Image As BitmapResource
    Public Property BattlePropertiesComposite As BattlePropertiesComposite
    Public Property BodyPropertiesComposite As BodyPropertiesComposite
    Public Property IsEnabled As Boolean
    Public Property Achieved As Boolean
    Public Property AchieveDate As Date
    Public Property LastSyncDate As Date
    Public Property Condition As MissionCondition

End Class