Imports Nukepayload2.N2Engine.Foundation

Namespace N2Math
    Public Class RandomGenerator
        Shared rand As New Random
        Public Shared Sub Randomize()
            rand = New Random
        End Sub
        Public Shared Sub Randomize(seed As Integer)
            rand = New Random(seed)
        End Sub
        ''' <summary>
        ''' 大于等于0
        ''' </summary>
        Public Shared Function RandomInt32() As Integer
            Return rand.Next()
        End Function
        ''' <summary>
        ''' 大于等于min, 小于max
        ''' </summary>
        Public Shared Function RandomInt32(min As Integer, max As Integer) As Integer
            Return rand.Next(min, max)
        End Function
        ''' <summary>
        ''' 大于等于0，小于max
        ''' </summary>
        Public Shared Function RandomInt32(max As Integer) As Integer
            Return rand.Next(max)
        End Function
        ''' <summary>
        ''' 0到1之间
        ''' </summary>
        Public Shared Function RandomSingle() As Single
            Return CSng(rand.Next) / 16777216.0!
        End Function
        ''' <summary>
        ''' 0到1之间
        ''' </summary>
        Public Shared Function RandomDouble() As Double
            Return rand.NextDouble
        End Function
        ''' <summary>
        ''' 连透明度都是随机的
        ''' </summary>
        Public Shared Function RandomColor() As Color
            Dim buf(3) As Byte
            rand.NextBytes(buf)
            Return Color.FromArgb(buf(0), buf(1), buf(2), buf(3))
        End Function
        ''' <summary>
        ''' 按照固定的透明度随机
        ''' </summary>
        Public Shared Function RandomColor(transparency As Byte) As Color
            Dim buf(2) As Byte
            rand.NextBytes(buf)
            Return Color.FromArgb(transparency, buf(0), buf(1), buf(2))
        End Function
    End Class
End Namespace