Option Strict On

''' <summary>
''' 表示缓存
''' </summary>
Public MustInherit Class Cache(Of T)

    Public Sub New(loader As LoaderDelegate(Of T), cacheSize As Integer)
        Me.Loader = loader
        If cacheSize < 1 Then
            Throw New ArgumentOutOfRangeException(NameOf(cacheSize))
        End If
        Me.CacheSize = cacheSize
    End Sub

    ''' <summary>
    ''' 用于初始化Cache中的某个单元以便以后插入数据
    ''' </summary>
    Public ReadOnly Property Loader As LoaderDelegate(Of T)
    ''' <summary>
    ''' 缓存大小
    ''' </summary>
    Public ReadOnly Property CacheSize As Integer
    ''' <summary>
    ''' 放置一个内容到此缓存中
    ''' </summary>
    ''' <param name="key">用于检索的内容</param>
    ''' <param name="keySelect">比较与Cache中的内容</param>
    ''' <param name="hitCallback">成功命中后调用</param>
    ''' <param name="faultCallbackAsync">资源申请失败时调用，或者是发生置换时被换出后调用。从换出对象得到换入对象，然后进行换入。</param>
    Public MustOverride Function PutAsync(Of Tkey)(key As Tkey, keySelect As Predicate(Of T), hitCallback As Action(Of T), faultCallbackAsync As AsyncFaultCallback(Of T)) As Task

End Class
''' <summary>
''' 从换出对象得到换入对象，然后进行换入。
''' </summary>
''' <param name="swapOut">换出对象</param>
Public Delegate Function AsyncFaultCallback(Of T)(swapOut As T) As Task(Of T)
''' <summary>
''' 对单个条目的加载器
''' </summary>
''' <param name="item">加载的条目</param>
Public Delegate Sub LoaderDelegate(Of T)(ByRef item As T)
''' <summary>
''' 线性存储性质的的缓存
''' </summary>
Friend MustInherit Class LinearCache(Of T)
    Inherits Cache(Of T)

    Sub New(loader As LoaderDelegate(Of T), cacheSize As Integer)
        MyBase.New(loader, cacheSize)
        ReDim Storage(cacheSize - 1)
        For i = 0 To Storage.Length - 1
            loader(Storage(i))
        Next
    End Sub

    Public ReadOnly Property Storage As T()
End Class

''' <summary>
''' 找第一个可替换区域进行替换的缓存
''' </summary>
Friend Class FirstFitCache(Of T)
    Inherits LinearCache(Of T)

    Sub New(loader As LoaderDelegate(Of T), canSafelyReplace As Predicate(Of T), cacheSize As Integer, throwIfUnsafe As Boolean)
        MyBase.New(loader, cacheSize)
        Me.ThrowIfUnsafe = throwIfUnsafe
        Me.CanSafelyReplace = canSafelyReplace
    End Sub

    ''' <summary>
    ''' 可以替换并且不会产生表面上的行为变更
    ''' </summary>
    Public ReadOnly Property CanSafelyReplace As Predicate(Of T)
    ''' <summary>
    ''' 申请资源遇到困难的时候抛出异常还是驳回申请资源的请求但不抛出异常
    ''' </summary>
    Public Property ThrowIfUnsafe As Boolean

    Public Overrides Async Function PutAsync(Of Tkey)(key As Tkey, keySelect As Predicate(Of T), hitCallback As Action(Of T), faultCallbackAsync As AsyncFaultCallback(Of T)) As Task
        ' 命中测试
        For Each item In Storage
            If keySelect(item) Then
                hitCallback(item)
                Return
            End If
        Next
        ' 找第一个播放完毕的腾出来
        For i = 0 To Storage.Length - 1
            Dim item = Storage(i)
            If CanSafelyReplace(item) Then
                Storage(i) = Await faultCallbackAsync(item)
                Return
            End If
        Next
        ' 没找到能直接换的
        If ThrowIfUnsafe Then
            Throw New CacheOverflowException
        Else
            Await faultCallbackAsync(Nothing)
        End If
    End Function
End Class

''' <summary>
''' 如果没有命中，则进行先入先出的缓存
''' </summary>
Friend Class FifoCache(Of T)
    Inherits Cache(Of T)

    Sub New(loader As LoaderDelegate(Of T), cacheSize As Integer)
        MyBase.New(loader, cacheSize)
        Storage = New Queue(Of T)(cacheSize)
    End Sub

    Public ReadOnly Property Storage As Queue(Of T)

    Public Overrides Async Function PutAsync(Of Tkey)(key As Tkey, keySelect As Predicate(Of T), hitCallback As Action(Of T), faultCallbackAsync As AsyncFaultCallback(Of T)) As Task
        ' 命中测试
        For i = 0 To Storage.Count - 1
            Dim item = Storage.Dequeue
            If item Is Nothing Then
                Loader.Invoke(item)
            ElseIf keySelect(item) Then
                Storage.Enqueue(item)
                hitCallback(item)
                Return
            End If
            Storage.Enqueue(item)
        Next
        ' 进行FIFO搜索
        Dim obj = Storage.Dequeue()
        Storage.Enqueue(Await faultCallbackAsync(obj))
    End Function
End Class

''' <summary>
''' 替换近期最少使用单元的缓存
''' </summary>
Friend Class LRUCache(Of T)
    Inherits LinearCache(Of T)

    Sub New(loader As LoaderDelegate(Of T), cacheSize As Integer)
        MyBase.New(loader, cacheSize)
        ReDim ReferenceTimes(cacheSize - 1)
    End Sub

    Public ReadOnly Property ReferenceTimes As Integer()

    Dim putTime As Integer = -1

    Public Overrides Async Function PutAsync(Of Tkey)(key As Tkey, keySelect As Predicate(Of T), hitCallback As Action(Of T), faultCallbackAsync As AsyncFaultCallback(Of T)) As Task
        ' 命中测试，其中带有统计上次使用时间
        putTime += 1
        For i = 0 To Storage.Length - 1
            Dim item = Storage(i)
            If keySelect(item) Then
                ReferenceTimes(i) = putTime
                hitCallback(item)
                Return
            End If
        Next
        ' 进行LRU搜索
        Dim swapIndex = 0
        Dim minValue = Integer.MaxValue
        For i = 0 To ReferenceTimes.Length - 1
            Dim item = ReferenceTimes(i)
            If item < minValue Then
                minValue = item
                swapIndex = i
            End If
        Next
        Dim swapOut = Storage(swapIndex)
        Storage(swapIndex) = Await faultCallbackAsync(swapOut)
    End Function
End Class

''' <summary>
''' 替换剩余占用完成时间最短单元的缓存
''' </summary>
Friend Class SRTCache(Of T)
    Inherits LinearCache(Of T)

    Sub New(loader As LoaderDelegate(Of T), cacheSize As Integer, getRemainingTime As Func(Of T, TimeSpan))
        MyBase.New(loader, cacheSize)
        Me.GetRemainingTime = getRemainingTime
    End Sub

    Public ReadOnly Property GetRemainingTime As Func(Of T, TimeSpan)

    Public Overrides Async Function PutAsync(Of Tkey)(key As Tkey, keySelect As Predicate(Of T), hitCallback As Action(Of T), faultCallbackAsync As AsyncFaultCallback(Of T)) As Task
        ' 命中测试
        For i = 0 To Storage.Length - 1
            Dim item = Storage(i)
            If keySelect(item) Then
                hitCallback(item)
                Return
            End If
        Next
        ' 进行SRT搜索
        Dim swapIndex = 0
        Dim minRemaining = TimeSpan.MaxValue
        For i = 0 To Storage.Length - 1
            Dim item = GetRemainingTime(Storage(i))
            If item < minRemaining Then
                minRemaining = item
                swapIndex = i
            End If
        Next
        Dim swapOut = Storage(swapIndex)
        Storage(swapIndex) = Await faultCallbackAsync(swapOut)
    End Function
End Class