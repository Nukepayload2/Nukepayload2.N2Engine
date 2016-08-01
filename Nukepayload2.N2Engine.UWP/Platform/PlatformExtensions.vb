Public Module PlatformExtensions
    <Extension>
    Public Function AsWindowsColor(color As Core.Color) As Windows.UI.Color
        Return Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B)
    End Function
End Module