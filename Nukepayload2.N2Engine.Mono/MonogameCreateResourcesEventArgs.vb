Imports Microsoft.Xna.Framework.Graphics

Public Class MonogameCreateResourcesEventArgs
    Inherits EventArgs

    Sub New(device As GraphicsDevice)
        Me.Device = device
    End Sub

    Public ReadOnly Property Device As GraphicsDevice
End Class
