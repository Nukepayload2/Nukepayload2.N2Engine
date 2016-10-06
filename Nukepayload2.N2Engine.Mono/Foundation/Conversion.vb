#If WINDOWS_DESKTOP Or DESKTOP_OPENGL Then
Imports System.Runtime.CompilerServices
#End If
''' <summary>
''' 将N2引擎或.NET中定义的类型转换为Xna/Monogame的类型
''' </summary>
Module Conversion
    ''' <summary>
    ''' 转换为Xna颜色
    ''' </summary>
    ''' <param name="color">N2引擎的颜色</param>
    <Extension>
    Function AsXnaColor(color As Nukepayload2.N2Engine.Foundation.Color) As Microsoft.Xna.Framework.Color
        Return New Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A)
    End Function
    ''' <summary>
    ''' 转换为Xna二维向量
    ''' </summary>
    ''' <param name="vector">要转换的.NET中的向量</param>
    <Extension>
    Function AsXnaVector2(vector As System.Numerics.Vector2) As Microsoft.Xna.Framework.Vector2
        Return New Microsoft.Xna.Framework.Vector2(vector.X, vector.Y)
    End Function
End Module