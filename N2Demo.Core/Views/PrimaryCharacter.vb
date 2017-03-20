Imports Nukepayload2.N2Engine.PhysicsIntegration
Imports Nukepayload2.N2Engine.UI.Elements

Public Class PrimaryCharacter
    Inherits GameContentEntity(Of SpriteElement)

    Public Sub New(content As SpriteElement, collider As ICollider)
        MyBase.New(content, collider)
    End Sub

End Class
