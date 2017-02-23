Imports Nukepayload2.N2Engine.Foundation

Namespace N2Math
    Public Class RandomGenerator
        Public Shared ReadOnly Property Rand As New Random

        Public Shared Sub Randomize()
            _Rand = New Random
        End Sub
        Public Shared Sub Randomize(seed As Integer)
            _Rand = New Random(seed)
        End Sub
        ''' <summary>
        ''' 大于等于0
        ''' </summary>
        Public Shared Function RandomInt32() As Integer
            Return Rand.Next()
        End Function
        ''' <summary>
        ''' 大于等于min, 小于max
        ''' </summary>
        Public Shared Function RandomInt32(min As Integer, max As Integer) As Integer
            Return Rand.Next(min, max)
        End Function
        ''' <summary>
        ''' 大于等于0，小于max
        ''' </summary>
        Public Shared Function RandomInt32(max As Integer) As Integer
            Return Rand.Next(max)
        End Function
        ''' <summary>
        ''' 0到1之间
        ''' </summary>
        Public Shared Function RandomSingle() As Single
            Return CSng(Rand.Next) * 4.656613E-10F
        End Function
        ''' <summary>
        ''' -1到1之间
        ''' </summary>
        Public Shared Function RandomSingle2() As Single
            Return CSng(Rand.Next) * 9.313226E-10F - 1.0F
        End Function
        ''' <summary>
        ''' 生成 -1,-1 到 1,1 之间的随机向量
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function RandomVector2() As Vector2
            Return New Vector2(RandomSingle2, RandomSingle2)
        End Function

        ''' <summary>
        ''' 生成 0,0 到 1,1 之间的随机向量
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function RandomVector2Positive() As Vector2
            Return New Vector2(RandomSingle, RandomSingle)
        End Function
        ''' <summary>
        ''' 生成 0,0 到 1,1 之间的随机向量
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function RandomVector2Positive(scaleX As Integer, scaleY As Integer) As Vector2
            Return New Vector2(RandomSingle() * scaleX, RandomSingle() * scaleY)
        End Function
        ''' <summary>
        ''' 0到1之间
        ''' </summary>
        Public Shared Function RandomDouble() As Double
            Return Rand.NextDouble
        End Function
        ''' <summary>
        ''' 连透明度都是随机的
        ''' </summary>
        Public Shared Function RandomColor() As Color
            Dim buf(3) As Byte
            Rand.NextBytes(buf)
            Return Color.FromArgb(buf(0), buf(1), buf(2), buf(3))
        End Function
        ''' <summary>
        ''' 按照固定的透明度随机
        ''' </summary>
        Public Shared Function RandomColor(transparency As Byte) As Color
            Dim buf(2) As Byte
            Rand.NextBytes(buf)
            Return Color.FromArgb(transparency, buf(0), buf(1), buf(2))
        End Function
    End Class
End Namespace