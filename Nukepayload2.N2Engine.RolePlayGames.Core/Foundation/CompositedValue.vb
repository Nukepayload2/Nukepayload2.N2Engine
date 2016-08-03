''' <summary>
''' 经过对原值进行加法和乘法运算的值
''' </summary>
''' <typeparam name="T"></typeparam>
Public Structure CompositedValue(Of T)
    Public Property Value As T
    Public Property Multiply As Single
    Public Property Add As T
End Structure