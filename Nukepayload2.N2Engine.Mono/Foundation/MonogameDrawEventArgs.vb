Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports RaisingStudio.Xna.Graphics

Public Class MonogameDrawEventArgs
    Inherits EventArgs

    Sub New(spriteBatch As SpriteBatch, timing As GameTime)
        Me.SpriteBatch = spriteBatch
        Me.Timing = timing
    End Sub

    Public ReadOnly Property SpriteBatch As SpriteBatch
    Public ReadOnly Property Timing As GameTime
    Public Property DrawingContext As DrawingContext
End Class