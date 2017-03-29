Imports System.Numerics
Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.Models
Imports Nukepayload2.N2Engine.PhysicsIntegration

''' <summary>
''' 具有物理属性的瓷片
''' </summary>
Public Class Tile
    Implements ITile

    Public Property SpriteSheetIndex As Integer Implements ITile.SpriteSheetIndex

    Public ReadOnly Property Collider As ICollider Implements ITile.Collider

    Public Property Body As Body Implements ITile.Body

    Public Property X As Integer Implements ITile.X

    Public Property Y As Integer Implements ITile.Y

    Sub New()
        Collider = New RectangleCollider(2.5F, New Vector2(64.0F, 64.0F).ToPhysicsUnit)
    End Sub

    Sub New(x As Integer, y As Integer)
        MyClass.New
        Me.X = x
        Me.Y = y
    End Sub

    Public Shared Widening Operator CType(tuple As (Integer, Integer)) As Tile
        Return New Tile(tuple.Item1, tuple.Item2)
    End Operator
End Class
