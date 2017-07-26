Namespace N2Math

    Partial Public Module Statistics
        ''' <summary>
        ''' 使用快速阶乘求组合数量
        ''' </summary>
        Function Combination([from] As Integer, [select] As Integer) As Integer
            Return Factorial([from]) \ Factorial([select]) \ Factorial([from] - [select])
        End Function
    End Module

End Namespace