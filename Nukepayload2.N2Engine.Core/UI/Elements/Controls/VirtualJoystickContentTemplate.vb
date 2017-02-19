Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' 虚拟摇杆内容的模板
    ''' </summary>
    Public Class VirtualJoystickContentTemplate
        Inherits GameVisualContainer

        Dim _circleStroke As New EllipseElement
        Dim _circleFilled As New EllipseElement

        Public ReadOnly Property Stroke As PropertyBinder(Of Color)
            Get
                Return _circleStroke.Stroke
            End Get
        End Property

        Public ReadOnly Property Fill As PropertyBinder(Of Color)
            Get
                Return _circleFilled.Fill
            End Get
        End Property

        Public ReadOnly Property StrokeSize As PropertyBinder(Of Vector2)
            Get
                Return _circleStroke.Size
            End Get
        End Property

        Public ReadOnly Property FillSize As PropertyBinder(Of Vector2)
            Get
                Return _circleFilled.Size
            End Get
        End Property

        Public ReadOnly Property StartPoint As New PropertyBinder(Of Vector2)(
            Function() _circleStroke.Location.Value + _circleStroke.Size.Value / 2,
            Sub(value) _circleStroke.Location.Value = value - _circleStroke.Size.Value / 2)

        Public ReadOnly Property EndPoint As New PropertyBinder(Of Vector2)(
            Function() _circleFilled.Location.Value + _circleFilled.Size.Value / 2,
            Sub(value) _circleFilled.Location.Value = value - _circleFilled.Size.Value / 2)

        Sub New()
            With Children
                .Add(_circleStroke)
                .Add(_circleFilled)
            End With
        End Sub
    End Class

End Namespace
