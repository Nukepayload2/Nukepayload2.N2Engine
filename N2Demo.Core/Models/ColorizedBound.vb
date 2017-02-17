Imports System.Numerics
Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation

Public Class ColorizedBound
    Public Property Position As Vector2
    Public Property Size As Vector2
    Public Property Color As Color
    <JsonIgnore>
    Public Property Rotate As Single = Math.PI / 4
    <JsonIgnore>
    Public Property Skew As New Vector2
    <JsonIgnore>
    Public Property RelativeOrigin As New Vector2(0.5, 0.5)
    <JsonIgnore>
    Public Property Scale As New Vector2(1, 1)
End Class
