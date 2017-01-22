Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Text

Namespace UI.Controls

    Public Class TextBlock
        Inherits GameControl

        Public ReadOnly Property Text As New PropertyBinder(Of String)
        Public ReadOnly Property Font As New GameFont
    End Class

End Namespace