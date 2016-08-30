Imports System.Runtime.CompilerServices

Partial Module ColorConverter
    <Extension>
    Function AsGtkColor(color As Microsoft.Xna.Framework.Color) As Gdk.RGBA
        Dim col As New Gdk.RGBA
        col.Alpha = color.A / 255
        col.Red = color.A / 255
        col.Green = color.A / 255
        col.Blue = color.A / 255
        Return col
    End Function
    <Extension>
    Function AsXnaColor(color As Gdk.RGBA) As Microsoft.Xna.Framework.Color
        Return New Microsoft.Xna.Framework.Color(CSng(color.Red * 255), CSng(color.Green * 255), CSng(color.Blue * 255), CSng(color.Alpha * 255))
    End Function
End Module