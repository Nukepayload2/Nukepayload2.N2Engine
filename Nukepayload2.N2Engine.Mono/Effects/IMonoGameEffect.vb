Imports Microsoft.Xna.Framework.Graphics
Public Interface IMonoGameEffect
    ''' <summary>
    ''' 向纹理应用一个特效，返回加过特效的纹理。
    ''' </summary>
    Function ApplyEffect(source As Texture2D) As Texture2D
End Interface
