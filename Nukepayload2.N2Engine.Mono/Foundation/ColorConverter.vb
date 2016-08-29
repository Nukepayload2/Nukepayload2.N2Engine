#If WINDOWS_DESKTOP Then
Imports System.Runtime.CompilerServices
#End If

Module ColorConverter
    <Extension>
    Function AsXnaColor(color As Foundation.Color) As Microsoft.Xna.Framework.Color
        Return New Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A)
    End Function
End Module
