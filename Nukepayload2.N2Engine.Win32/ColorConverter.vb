Imports System.Runtime.CompilerServices

Partial Module ColorConverter
    <Extension>
    Function AsWindowsColor(color As Microsoft.Xna.Framework.Color) As System.Windows.Media.Color
        Return Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B)
    End Function
    <Extension>
    Function AsXnaColor(color As System.Windows.Media.Color) As Microsoft.Xna.Framework.Color
        Return New Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A)
    End Function
End Module