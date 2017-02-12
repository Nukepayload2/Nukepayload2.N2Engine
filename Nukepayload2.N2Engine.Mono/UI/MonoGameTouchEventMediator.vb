Imports System.Numerics
Imports Microsoft.Xna.Framework.Input.Touch
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.Utilities

Public Class MonoGameTouchEventMediator
    Implements ITouchEventMediator

    Sub New(attachedView As GameCanvas)
        Me.AttachedView = attachedView
    End Sub

    Public Property AttachedView As GameCanvas Implements IEventMediator.AttachedView

    Dim _Points As New Dictionary(Of UInteger, Vector2)
    Public ReadOnly Property Points As IReadOnlyDictionary(Of UInteger, Vector2) Implements ITouchStatusProvider.Points
        Get
            Return _Points
        End Get
    End Property

    Public Sub TryRaiseEvent() Implements IEventMediator.TryRaiseEvent
        Dim touchStat = TouchPanel.GetState
        Dim remainingPointIds = Points.Keys.ToList
        For Each pt In touchStat
            Dim pos = pt.Position
            Dim cyrN2Pos As New Vector2(pos.X, pos.Y)
            Dim ptId = pt.Id.ToUInt32
            Dim isPressed = Points.ContainsKey(ptId)
            If isPressed Then
                remainingPointIds.Remove(ptId)
            End If
            If pt.State = TouchLocationState.Pressed Then
                If Not isPressed Then
                    _Points.Add(ptId, cyrN2Pos)
                    Dim e As New GameTouchRoutedEventArgs(cyrN2Pos, ptId, pt.Pressure)
                    AttachedView.RaiseTouchDown(e)
                End If
            ElseIf pt.State = TouchLocationState.Moved Then
                Dim loc As New TouchLocation
                If pt.TryGetPreviousLocation(loc) Then
                    Dim lastLoc = loc.Position
                    Dim lastN2Pos As New Vector2(lastLoc.X, lastLoc.Y)
                    If lastN2Pos <> cyrN2Pos Then
                        Dim e As New GameTouchRoutedEventArgs(cyrN2Pos, lastN2Pos, ptId, pt.Pressure)
                        AttachedView.RaiseTouchMove(e)
                    End If
                End If
            End If
        Next
        For Each ptId In remainingPointIds
            Dim e As New GameTouchRoutedEventArgs(ptId, Points(ptId))
            AttachedView.RaiseTouchUp(e)
            _Points.Remove(ptId)
        Next
    End Sub

End Class
