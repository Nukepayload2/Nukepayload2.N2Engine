Imports System.Runtime.CompilerServices

Partial Module Conversion
    <Extension>
    Function AsGtkColor(color As Microsoft.Xna.Framework.Color) As Gdk.RGBA
        Dim col As New Gdk.RGBA With {
            .Alpha = color.A / 255,
            .Red = color.A / 255,
            .Green = color.A / 255,
            .Blue = color.A / 255
        }
        Return col
    End Function
    <Extension>
    Function AsXnaColor(color As Gdk.RGBA) As Microsoft.Xna.Framework.Color
        Return New Microsoft.Xna.Framework.Color(CSng(color.Red * 255), CSng(color.Green * 255), CSng(color.Blue * 255), CSng(color.Alpha * 255))
    End Function
End Module