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
    Public Shared Function RandomSingle() As Single
        Dim buf(3) As Byte
        rand.NextBytes(buf)
        Return BitConverter.ToSingle(buf, 0)
    End Function
    Public Shared Function RandomDouble() As Double
        Return rand.NextDouble
    End Function
    Public Shared Function RandomColor() As Color
        Dim buf(3) As Byte
        rand.NextBytes(buf)
        Return Color.FromArgb(buf(0), buf(1), buf(2), buf(3))
    End Function
End Class
