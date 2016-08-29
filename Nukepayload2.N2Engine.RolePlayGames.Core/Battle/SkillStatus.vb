Imports Nukepayload2.N2Engine.Resources

Public Class SkillStatus
    Inherits GameResourceModelBase
    Public Property IsEnabled As Boolean
    Public Property Attached As New List(Of BuffDebuffStatus)
    Public Property Attack As CompositedValue(Of Single)
    Public Property ElementAttacks As Dictionary(Of ElementPhase, CompositedValue(Of Single))
    Public Property Point As ValueRange(Of Integer)
    Public Property ConsumePoint As CompositedValue(Of Integer)
    Public Property ConsumeMP As CompositedValue(Of Integer)
    Public Property ConsumeHP As CompositedValue(Of Integer)
    Public Property ConsumeDP As CompositedValue(Of Integer)
    Public Property RequiredCharacters As New List(Of ResourceId)
    Public Property Group As ResourceId
End Class