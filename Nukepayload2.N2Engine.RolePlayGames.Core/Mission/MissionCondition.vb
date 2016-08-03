Imports Nukepayload2.N2Engine.Core

Public Class MissionCondition
    Inherits GameResourceModelBase

    Public Property Item As Tuple(Of ResourceId, ValueRange(Of Integer))()
    Public Property PrequestMission As ResourceId()
    Public Property ForbiddenMission As ResourceId()
    Public Property Level As ValueRange(Of Integer)
    Public Property Round As ValueRange(Of Integer)
    Public Property PrequestFlag As Tuple(Of ResourceId, Integer())()
    Public Property ForbiddenFlag As Tuple(Of ResourceId, Integer())()

End Class
