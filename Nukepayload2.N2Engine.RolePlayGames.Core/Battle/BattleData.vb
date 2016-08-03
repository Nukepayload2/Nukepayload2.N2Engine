Imports Nukepayload2.N2Engine.Core

Public Class BattleData
    Public Property Music As ResourceId
    Public Property Background As ResourceId
    Public Property Monsters As New List(Of GameCharacterData)
    Public Property Players As New List(Of GameCharacterData)
    Public Property StartTime As Date
    Public Property EndTime As Date
End Class
