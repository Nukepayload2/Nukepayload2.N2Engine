Namespace N2Math

    Partial Public Module Statistics
        Dim mask As Integer
        Dim cnrs(100) As Integer
        Dim number As Integer
        Dim p_size As Integer

        Private Function Power(n As Integer, m As Integer) As Integer
            Dim temp As Integer
            If m = 1 Then
                temp = n
            ElseIf ((m And 1) <> 0) Then
                temp = n * Power(n, m - 1)
            Else
                temp = Power(n, m >> 1)
                temp *= temp
                cnrs(number) = (temp >> ((m >> 1) * p_size)) And mask
                number += 1
            End If
            Return temp
        End Function
        Private Function Factor(n As Integer) As Integer
            Dim temp As Integer
            If n = 1 Then
                Return 1
            ElseIf (n And 1) = 1 Then
                Return n * Factor(n - 1)
            Else
                temp = Factor(n >> 1)
                Try
                    Return cnrs(number) * temp * temp
                Finally
                    number += 1
                End Try
            End If
        End Function
        ''' <summary>
        ''' 求一个数的阶乘
        ''' </summary>
        Function Factorial(n As Integer) As Integer
            If n <= 1 Then
                Return 1
            End If
            Dim x = (1 << n) + 1
            number = 0
            mask = (1 << n) - 1
            p_size = n
            Power(x, n)
            number = 0
            Return Factor(n)
        End Function
    End Module
End Namespace
