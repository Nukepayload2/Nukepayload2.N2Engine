Imports Microsoft.Xna.Framework

Public Class MonogameUpdateEventArgs
    Inherits EventArgs

    Sub New(timing As GameTime)
        Me.Timing = timing
    End Sub

    Public ReadOnly Property Timing As GameTime
End Class
