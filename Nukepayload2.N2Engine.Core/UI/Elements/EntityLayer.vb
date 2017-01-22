Imports Box2D

Namespace UI.Elements
    Public Class EntityLayer
        Inherits GameLayer

        Sub New(world As World)
            Me.World = world
        End Sub

        Public Property World As World
    End Class
End Namespace