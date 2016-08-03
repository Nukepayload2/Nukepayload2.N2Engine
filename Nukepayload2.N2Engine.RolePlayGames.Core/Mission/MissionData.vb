Imports Nukepayload2.N2Engine.Core

Public Class MissionData
    Inherits GameResourceModelBase
    Public Property MinRound As Integer
    Public Property MaxRound As Integer
    Public Property State As MissionState
    Public Property MissionType As MissionType
    Public Property UnlockConditions As New List(Of MissionCondition)
    Public Property CompleteConditions As New List(Of MissionCondition)

End Class