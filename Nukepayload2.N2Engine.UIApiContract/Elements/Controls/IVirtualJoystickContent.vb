Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Controls
    Public Interface IVirtualJoystickContent
        Property EndPoint As PropertyBinder(Of Vector2)
        Property Fill As PropertyBinder(Of Color)
        Property FillSize As PropertyBinder(Of Vector2)
        Property StartPoint As PropertyBinder(Of Vector2)
        Property Stroke As PropertyBinder(Of Color)
        Property StrokeSize As PropertyBinder(Of Vector2)
    End Interface
End Namespace
