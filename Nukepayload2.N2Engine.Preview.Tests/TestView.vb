Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Foundation.Preview

Public Class TestView
    Public ReadOnly Property Int32Value As New PropertyBinder(Of Integer)
    Public ReadOnly Property CachedInt32Value As New CachedPropertyBinder(Of Integer)
End Class
