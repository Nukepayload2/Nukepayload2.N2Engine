Imports System.Runtime.InteropServices

Namespace Foundation
    ''' <summary>
    ''' 用整数表示的大小。最常见的情况是表示位图的大小。
    ''' </summary>
    <StructLayout(LayoutKind.Explicit)>
    Public Structure SizeInInteger
        ''' <summary>
        ''' 宽度
        ''' </summary>
        <FieldOffset(0)>
        Public Width As Integer
        ''' <summary>
        ''' 高度
        ''' </summary>
        <FieldOffset(4)>
        Public Height As Integer
        ''' <summary>
        ''' 宽度（按照无符号格式读写）
        ''' </summary>
        <FieldOffset(0)>
        Public UnsignedWidth As UInteger
        ''' <summary>
        ''' 高度（按照无符号格式读写）
        ''' </summary>
        <FieldOffset(4)>
        Public UnsignedHeight As UInteger

        Public Sub New(width As Integer, height As Integer)
            Me.Width = width
            Me.Height = height
        End Sub

        Public Sub New(width As UInteger, height As UInteger)
            UnsignedWidth = width
            UnsignedHeight = height
        End Sub
    End Structure

End Namespace