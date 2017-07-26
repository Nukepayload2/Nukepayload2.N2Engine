Imports System.Runtime.InteropServices

Namespace Utilities
    ''' <summary>
    ''' 提供值类型的静态转换
    ''' </summary>
    Public Module StaticCast
        <StructLayout(LayoutKind.Explicit)>
        Private Structure Int32UInt32
            <FieldOffset(0)>
            Dim Int32Value As Integer
            <FieldOffset(0)>
            Dim UInt32Value As UInteger
        End Structure
        ''' <summary>
        ''' 将指定的 有符号 32 位整数 转换为 无符号 32 位整数。
        ''' </summary>
        ''' <param name="value">要转换的有符号 32 位整数</param>
        ''' <returns>无符号 32 位整数</returns>
        <Extension>
        Public Function ToUInt32(value As Integer) As UInteger
            Return (New Int32UInt32 With {.Int32Value = value}).UInt32Value
        End Function
    End Module
End Namespace
