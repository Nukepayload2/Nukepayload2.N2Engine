Imports System.Runtime.InteropServices

Namespace Marshal
    Public Module ColorMarshal
        <StructLayout(LayoutKind.Explicit)>
        Private Structure ColorConverter
            <FieldOffset(0)>
            Friend N2Color As Foundation.Color
            <FieldOffset(0)>
            Friend WindowsColor As Windows.UI.Color
        End Structure
        ''' <summary>
        ''' 将引擎定义的颜色转换为 Windows RT 的颜色
        ''' </summary>
        <Extension>
        Public Function AsWindowsColor(color As Foundation.Color) As Windows.UI.Color
            Dim colorConverter1 As New ColorConverter With {.N2Color = color}
            Return colorConverter1.WindowsColor
        End Function
    End Module
End Namespace
