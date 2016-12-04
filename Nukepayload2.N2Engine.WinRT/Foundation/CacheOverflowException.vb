Public Class CacheOverflowException
    Inherits Exception
    Sub New()
        MyBase.New("缓存发生溢出")
    End Sub
    Sub New(name$)
        MyBase.New($"缓存""{name}""发生溢出")
    End Sub
    Sub New(name$, description$)
        MyBase.New($"缓存""{name}""发生溢出, " + description)
    End Sub
End Class
