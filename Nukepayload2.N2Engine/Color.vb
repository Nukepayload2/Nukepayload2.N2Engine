Namespace Foundation
    ''' <summary>
    ''' 代表未预乘的A8R8G8B8颜色。
    ''' </summary>
    <TypeForwardedFrom("Nukepayload2.N2Engine.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")>
    Public Structure Color
        Implements IEquatable(Of Color)
        ''' <summary>
        ''' 通过 Alpha, Red, Green, Blue 通道初始化颜色。
        ''' </summary>
        Sub New(a As Byte, r As Byte, g As Byte, b As Byte)
            Me.A = a
            Me.R = r
            Me.G = g
            Me.B = b
        End Sub
        ''' <summary>
        ''' 使用编码的 32 位数字初始化颜色。
        ''' </summary>
        Sub New(value As Integer)
            A = (value >> 24) And &HFF
            R = (value >> 16) And &HFF
            G = (value >> 8) And &HFF
            B = value And &HFF
        End Sub
        ''' <summary>
        ''' 通过 Red, Green, Blue 通道初始化颜色。
        ''' </summary>
        Sub New(r As Byte, g As Byte, b As Byte)
            MyClass.New(255, r, g, b)
        End Sub
        ''' <summary>
        ''' Alpha 通道
        ''' </summary>
        Public Property A As Byte
        ''' <summary>
        ''' Red 通道
        ''' </summary>
        Public Property R As Byte
        ''' <summary>
        ''' Green 通道
        ''' </summary>
        Public Property G As Byte
        ''' <summary>
        ''' Blue 通道
        ''' </summary>
        Public Property B As Byte
        Public Shared Function FromArgb(a As Byte, r As Byte, g As Byte, b As Byte) As Color
            Return New Color(a, r, g, b)
        End Function
        Public Overloads Function Equals(other As Color) As Boolean Implements IEquatable(Of Color).Equals
            Return A = other.A AndAlso R = other.R AndAlso G = other.G AndAlso B = other.B
        End Function
        Public Shared Operator =(color1 As Color, color2 As Color) As Boolean
            Return color1.Equals(color2)
        End Operator
        Public Shared Operator <>(color1 As Color, color2 As Color) As Boolean
            Return Not color1.Equals(color2)
        End Operator
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is Color Then
                Return DirectCast(obj, Color).Equals(Me)
            End If
            Return False
        End Function
        Public Overrides Function GetHashCode() As Integer
            Return B Or G << 8 Or R << 16 Or A << 24
        End Function
        ''' <summary>
        ''' 将颜色转换成可以在Xaml使用的格式
        ''' </summary>
        Public Overrides Function ToString() As String
            Return "#" + GetHashCode.ToString("X")
        End Function
    End Structure
End Namespace
