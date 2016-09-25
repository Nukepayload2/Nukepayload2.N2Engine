''' <summary>
''' 表示邻接矩阵存储的有向图
''' </summary>
''' <typeparam name="TVertex">顶点存放的数据的类型</typeparam>
''' <typeparam name="TConnection">边权值类型</typeparam>
Public Class MatrixMap(Of TVertex As IEquatable(Of TVertex), TConnection As Structure)
    Dim Values As TVertex()
    Dim Connections(,) As TConnection?
    ''' <summary>
    ''' 使用变形的邻接表初始化
    ''' </summary>
    ''' <param name="Vertexes">顶点本身以及各个边和对应的顶点</param>
    Sub New(Vertexes As Tuple(Of TVertex, Tuple(Of TConnection, TVertex)())())
        Dim ucase = Vertexes.Count - 1
        ReDim Values(ucase)
        ReDim Connections(ucase, ucase)
        For i = 0 To ucase
            Values(i) = Vertexes(i).Item1
            Connections(i, i) = New TConnection
            Dim targets = Vertexes(i).Item2
            For j = 0 To targets.Length - 1
                For k = 0 To ucase
                    If Values(k).Equals(targets(j).Item2) Then
                        Connections(i, k) = targets(j).Item1
                        Exit For
                    End If
                Next
            Next
        Next
    End Sub
    ''' <summary>
    ''' 得到后续的节点
    ''' </summary>
    ''' <param name="CurrentValue">现在的节点</param>
    Public Function GetNextVertexes(CurrentValue As TVertex) As IEnumerable(Of KeyValuePair(Of TVertex, TConnection))
        Return GetNextVertexesAt(SearchCurrent(CurrentValue))
    End Function
    ''' <summary>
    ''' 得到后续的节点
    ''' </summary>
    ''' <param name="CurPos">当前节点的索引</param>
    ''' <returns></returns>
    Public Iterator Function GetNextVertexesAt(CurPos As Integer) As IEnumerable(Of KeyValuePair(Of TVertex, TConnection))
        Dim DefaultValue As New TConnection
        For i = 0 To Values.Length - 1
            Dim ScanningConn = Connections(CurPos, i)
            If ScanningConn.HasValue AndAlso ScanningConn.Value.Equals(DefaultValue) Then
                Yield New KeyValuePair(Of TVertex, TConnection)(Values(i), ScanningConn.Value)
            End If
        Next
    End Function
    ''' <summary>
    ''' 查找当前结点的索引
    ''' </summary>
    Private Function SearchCurrent(CurrentValue As TVertex) As Integer
        Dim CurPos% = -1
        For i = 0 To Values.Length - 1
            If Values(i).Equals(CurrentValue) Then
                CurPos = i
            End If
        Next
        If CurPos < 0 Then
            Throw New ArgumentOutOfRangeException(NameOf(CurrentValue))
        End If
        Return CurPos
    End Function
    ''' <summary>
    ''' 获取全部前驱的节点
    ''' </summary>
    ''' <param name="CurrentValue">当前节点</param>
    ''' <returns></returns>
    Public Function GetDependencies(CurrentValue As TVertex) As IEnumerable(Of KeyValuePair(Of TVertex, TConnection))
        Return GetDependenciesAt(SearchCurrent(CurrentValue))
    End Function
    ''' <summary>
    ''' 获取全部前驱的节点
    ''' </summary>
    ''' <param name="CurPos">当前节点的索引</param>
    ''' <returns></returns>
    Public Iterator Function GetDependenciesAt(CurPos As Integer) As IEnumerable(Of KeyValuePair(Of TVertex, TConnection))
        Dim DefaultValue As New TConnection
        For i = 0 To Values.Length - 1
            Dim ScanningConn = Connections(i, CurPos)
            If ScanningConn.HasValue AndAlso ScanningConn.Value.Equals(DefaultValue) Then
                Yield New KeyValuePair(Of TVertex, TConnection)(Values(i), ScanningConn.Value)
            End If
        Next
    End Function
    ''' <summary>
    ''' 获取入度为0的节点
    ''' </summary>
    ''' <returns></returns>
    Public Iterator Function GetInitialVertexes() As IEnumerable(Of TVertex)
        For i = 0 To Values.Length - 1
            If Not GetDependenciesAt(i).Any Then
                Yield Values(i)
            End If
        Next
    End Function
End Class
