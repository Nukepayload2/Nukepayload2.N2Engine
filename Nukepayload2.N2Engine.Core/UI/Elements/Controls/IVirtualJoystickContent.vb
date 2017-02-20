Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Controls
    Public Interface IVirtualJoystickContent
        ReadOnly Property EndPoint As PropertyBinder(Of Vector2)
        ReadOnly Property Fill As PropertyBinder(Of Color)
        ReadOnly Property FillSize As PropertyBinder(Of Vector2)
        ReadOnly Property StartPoint As PropertyBinder(Of Vector2)
        ReadOnly Property Stroke As PropertyBinder(Of Color)
        ReadOnly Property StrokeSize As PropertyBinder(Of Vector2)
    End Interface
End Namespace
