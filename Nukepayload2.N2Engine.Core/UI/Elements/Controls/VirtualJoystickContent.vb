Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls

    Public Class VirtualJoystickContent
        Inherits GameTemplatedContent
        Implements IVirtualJoystickContent

        Dim _circleStroke As New EllipseElement
        Dim _circleFilled As New EllipseElement

        Public ReadOnly Property Stroke As PropertyBinder(Of Color) Implements IVirtualJoystickContent.Stroke
            Get
                Return _circleStroke.Stroke
            End Get
        End Property

        Public ReadOnly Property Fill As PropertyBinder(Of Color) Implements IVirtualJoystickContent.Fill
            Get
                Return _circleFilled.Fill
            End Get
        End Property

        Public ReadOnly Property StrokeSize As PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.StrokeSize
            Get
                Return _circleStroke.Size
            End Get
        End Property

        Public ReadOnly Property FillSize As PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.FillSize
            Get
                Return _circleFilled.Size
            End Get
        End Property

        Public ReadOnly Property StartPoint As New PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.StartPoint
        Public ReadOnly Property EndPoint As New PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.EndPoint

        Sub New()
            _circleStroke.Location.Bind(
                Function() StartPoint.Value)
            _circleFilled.Location.Bind(
                Function() EndPoint.Value)
            With Children
                .Add(_circleStroke)
                .Add(_circleFilled)
            End With
        End Sub
    End Class

End Namespace