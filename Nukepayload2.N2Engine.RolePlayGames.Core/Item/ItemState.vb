Imports Nukepayload2.N2Engine.Resources

Public Class ItemState
    Inherits GameResourceModelBase
    Public Property AttachedItem As New List(Of ResourceId)
    Public Property Durability As ValueRange(Of Single)
    Public Property Charge As ValueRange(Of Single)
    Public Property Level As ValueRange(Of Integer)
    Public Property FilledMana As ValueRange(Of Integer)
    Public Property BattlePropertiesComposite As BattlePropertiesComposite
    Public Property BodyPropertiesComposite As BodyPropertiesComposite
End Class
