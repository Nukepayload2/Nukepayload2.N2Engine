Imports Microsoft.Xna.Framework
Imports RaisingStudio.Xna.Graphics

Public Class MonogameDrawEventArgs
    Inherits EventArgs

    Sub New(drawingContext As DrawingContext, timing As GameTime)
        Me.DrawingContext = drawingContext
        Me.Timing = timing
    End Sub

    Public ReadOnly Property DrawingContext As DrawingContext
    Public ReadOnly Property Timing As GameTime
End Class