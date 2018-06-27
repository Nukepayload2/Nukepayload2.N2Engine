Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls

    Public Class VirtualJoystickContent
        Inherits GameTemplatedContent
        Implements IVirtualJoystickContent

        Dim _circleStroke As New EllipseElement
        Dim _circleFilled As New EllipseElement

        Public Property Stroke As PropertyBinder(Of Color) Implements IVirtualJoystickContent.Stroke
            Get
                Return _circleStroke.Stroke
            End Get
            Set(value As PropertyBinder(Of Color))
                _circleStroke.Stroke = value
            End Set
        End Property

        Public Property Fill As PropertyBinder(Of Color) Implements IVirtualJoystickContent.Fill
            Get
                Return _circleFilled.Fill
            End Get
            Set(value As PropertyBinder(Of Color))
                _circleFilled.Fill = value
            End Set
        End Property

        Public Property StrokeSize As PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.StrokeSize
            Get
                Return _circleStroke.Size
            End Get
            Set(value As PropertyBinder(Of Vector2))
                _circleStroke.Size = value
            End Set
        End Property

        Public Property FillSize As PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.FillSize
            Get
                Return _circleFilled.Size
            End Get
            Set(value As PropertyBinder(Of Vector2))
                _circleFilled.Size = value
            End Set
        End Property

        ''' <summary>
        ''' 虚拟摇杆的摇杆框的中心坐标。预计不会频繁变动。
        ''' </summary>
        Public Property StartPoint As PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.StartPoint
            Get
                Return _circleStroke.Location
            End Get
            Set
                _circleStroke.Location = Value
            End Set
        End Property

        ''' <summary>
        ''' 虚拟摇杆的摇杆头的中心坐标。预计会频繁变动。
        ''' </summary>
        Public Property EndPoint As PropertyBinder(Of Vector2) Implements IVirtualJoystickContent.EndPoint
            Get
                Return _circleFilled.Location
            End Get
            Set(value As PropertyBinder(Of Vector2))
                _circleFilled.Location = value
            End Set
        End Property

        Sub New()
            With Children
                .Add(_circleStroke)
                .Add(_circleFilled)
            End With
        End Sub
    End Class

End Namespace