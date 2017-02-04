Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.UI.SpriteFonts

Public Class N2FontTileMonoGame
    Inherits N2FontTilePlatformControllerBase(Of Texture2D)

    Public Sub New(data As N2FontTile)
        MyBase.New(data)
    End Sub
    ''' <summary>
    ''' 打开字体文件，然后加载纹理
    ''' </summary>
    Protected Overrides Function LoadTexture() As Texture2D
        Using strm = _data.Parent.GetTileData(_data.PngOffset, _data.PngLength)
            Return Texture2D.FromStream(GraphicsDeviceManagerExtension.SharedDevice, strm)
        End Using
    End Function

End Class
