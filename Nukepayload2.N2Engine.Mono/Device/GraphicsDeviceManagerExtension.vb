Imports Microsoft.Xna.Framework.Graphics

Public Class GraphicsDeviceManagerExtension
    Shared _Current As GraphicsDevice
    ''' <summary>
    ''' 获取共享的图像设备（用于呈现 Monogame 内容）
    ''' </summary>
    Public Shared Property SharedDevice As GraphicsDevice
        Get
            Return _Current
        End Get
        Friend Set
            _Current = Value
        End Set
    End Property
End Class
