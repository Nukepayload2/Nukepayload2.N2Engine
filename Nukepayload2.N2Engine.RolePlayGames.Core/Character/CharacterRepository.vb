''' <summary>
''' 角色身上的各个可装卸物体的状态
''' </summary>
Public Class CharacterRepository
    Public Property Carrying As List(Of ItemState)
    Public Property Head As List(Of ItemState)
    Public Property LeftHand As List(Of ItemState)
    Public Property RightHand As List(Of ItemState)
    Public Property Clothes As List(Of ItemState)
    Public Property Pants As List(Of ItemState)
    Public Property Shoes As List(Of ItemState)
    Public Property Ornament As List(Of ItemState)
End Class