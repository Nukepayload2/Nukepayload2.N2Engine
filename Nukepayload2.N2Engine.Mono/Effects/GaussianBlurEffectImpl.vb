Imports Microsoft.Xna.Framework.Graphics

Friend Class GaussianBlurEffectImpl
    Implements IMonoGameEffect

    Public Function ApplyEffect(source As Texture2D) As Texture2D Implements IMonoGameEffect.ApplyEffect
        ' TODO: 实施高斯模糊 Pixel Shader 效果。
        Return source
    End Function
End Class
