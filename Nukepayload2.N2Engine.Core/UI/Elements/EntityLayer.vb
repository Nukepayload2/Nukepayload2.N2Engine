Imports System.Collections.Specialized
Imports FarseerPhysics.Dynamics

Namespace UI.Elements
    Public Class EntityLayer
        Inherits GameLayer

        Sub New(world As World)
            Me.World = world
            RegisterOnChildrenChanged(AddressOf ChildrenChanged)
        End Sub

        Private Sub ChildrenChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
            If e.NewItems IsNot Nothing Then
                For Each newItem As GameEntity In e.NewItems
                    newItem.CreateBody()
                Next
            End If
            If e.OldItems IsNot Nothing Then
                For Each oldItem As GameEntity In e.OldItems
                    World.RemoveBody(oldItem.Body)
                Next
            End If
        End Sub

        Private Sub EntityLayer_Updating(sender As GameVisual, e As UpdatingEventArgs) Handles Me.Updating
            World.Step(CSng(e.ElapsedTime.TotalSeconds))
        End Sub

        Public Property World As World
    End Class
End Namespace