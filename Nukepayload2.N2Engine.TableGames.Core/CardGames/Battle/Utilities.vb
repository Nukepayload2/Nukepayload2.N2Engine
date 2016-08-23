Imports System.Reflection

Namespace Battle

    Public Module Utilities
        Dim ran As New Random
        ''' <summary>
        ''' 产生Random整数
        ''' </summary> 
        ''' <returns></returns>
        Public Function RndEx(FromValue As Integer, ToValue As Integer) As Integer
            Return CInt(FromValue - 0.5 + (ran.NextDouble * (ToValue - FromValue + 1)))
        End Function
        <Extension>
        Public Async Function EnterStage(tj As IEffectHook, StageName As String, Params As Object()) As Task(Of Boolean)
            Return Await CType(tj.GetType.GetRuntimeMethod(StageName, {}).Invoke(tj, Params), Task(Of Boolean))
        End Function
        ''' <summary>
        ''' 计算围成圆形的Mark号最小差距
        ''' </summary>
        <Extension>
        Public Function DistanceOf(Sum As Integer, v1 As Integer, v2 As Integer) As Integer
            If v1 > v2 Then
                Dim t = v2
                v2 = v1
                v1 = t
            End If
            Return Math.Min(v2 - v1, v1 + Sum - v2)
        End Function
        ''' <summary>
        ''' a和b进行位与操作之AfterWhether为b
        ''' </summary> 
        ''' <returns></returns>
        <Extension>
        Public Function HasFlags(a As Integer, b As Integer) As Boolean
            Return b <> (a And b)
        End Function
        ''' <summary>
        ''' MilitaryOfficera中All成员进行位或操作之AfterWhether.Contain(b)
        ''' 可以判断没编码的AICategoryWhether包含编码After的AICategory
        ''' </summary> 
        <Extension>
        Public Function Contains(a As IEnumerable(Of CardCategories), b As Integer) As Boolean
            Dim va As Integer = 0
            For Each t In a
                va = va Or t
            Next
            Return va.HasFlags(b)
        End Function
        Public Async Function DoNothingTask() As Task
            Await Task.Run(Sub()

                           End Sub)
        End Function
        Public Async Function ResTrueTask() As Task(Of Boolean)
            Return Await Task.Run(Function() True)
        End Function
        Public Async Function ResFalseTask() As Task(Of Boolean)
            Return Await Task.Run(Function() False)
        End Function
        ''' <summary>
        ''' GetCurrentType的用无参数Sub New构造而来的对象。只Can用于Class的Type。
        ''' </summary>
        ''' <param name="tp"></param>
        ''' <returns></returns>
        <Extension>
        Public Function GetDefaultObject(tp As Type) As Object
            Return Activator.CreateInstance(tp)
        End Function
        Public Function RandomPatternPoints() As Poker
            Dim rnd As New Random
            Return New Poker(DirectCast(CInt(Math.Floor(rnd.NextDouble() * 4)), Patterns), 1 + CInt(Math.Floor(rnd.NextDouble() * 13)))
        End Function
    End Module
End Namespace