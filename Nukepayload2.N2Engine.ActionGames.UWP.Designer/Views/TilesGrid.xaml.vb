' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports System.Numerics
Imports Nukepayload2.Collections.Specialized
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Public NotInheritable Class TilesGrid
    Inherits UserControl

    Public Property RowCount As Integer
        Get
            Return GetValue(RowCountProperty)
        End Get
        Set
            SetValue(RowCountProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly RowCountProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(RowCount),
                           GetType(Integer), GetType(TilesGrid),
                           New PropertyMetadata(2,
                                                Sub(s, e)
                                                    Dim this = DirectCast(s, TilesGrid)
                                                    If Not e.OldValue.Equals(e.NewValue) Then
                                                        this.ReDimPreserveTiles()
                                                    End If
                                                End Sub))

    Public Property ColumnCount As Integer
        Get
            Return GetValue(ColumnCountProperty)
        End Get
        Set
            SetValue(ColumnCountProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ColumnCountProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(ColumnCount),
                           GetType(Integer), GetType(TilesGrid),
                           New PropertyMetadata(2,
                                                Sub(s, e)
                                                    Dim this = DirectCast(s, TilesGrid)
                                                    If Not e.OldValue.Equals(e.NewValue) Then
                                                        this.ReDimPreserveTiles()
                                                    End If
                                                End Sub))

    Public Property Tiles As ObservableFixedArray2D(Of EditableTile)
        Get
            Return GetValue(TilesProperty)
        End Get
        Set
            SetValue(TilesProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly TilesProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Tiles),
                           GetType(ObservableFixedArray2D(Of EditableTile)), GetType(TilesGrid),
                           New PropertyMetadata(Nothing,
                           Sub(s, e)
                               DirectCast(s, TilesGrid).UpdateTilesView(e.NewValue)
                           End Sub))

    Public Property SelectedTileX As Integer
        Get
            Return GetValue(SelectedTileXProperty)
        End Get
        Set
            SetValue(SelectedTileXProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly SelectedTileXProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(SelectedTileX),
                           GetType(Integer), GetType(TilesGrid),
                           New PropertyMetadata(-1))

    Public Property SelectedTileY As Integer
        Get
            Return GetValue(SelectedTileYProperty)
        End Get
        Set
            SetValue(SelectedTileYProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly SelectedTileYProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(SelectedTileY),
                           GetType(Integer), GetType(TilesGrid),
                           New PropertyMetadata(-1))

    Public Property TileSize As Vector2
        Get
            Return GetValue(TileSizeProperty)
        End Get
        Set
            SetValue(TileSizeProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly TileSizeProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(TileSize),
                           GetType(Vector2), GetType(TilesGrid),
                           New PropertyMetadata(New Vector2(64, 64)))

    Private Sub TilesGrid_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        CreateTiles()
    End Sub

    Private Sub ReDimPreserveTiles()
        If RowCount > 0 AndAlso ColumnCount > 0 Then
            Tiles.ReDimPreserve(RowCount - 1, ColumnCount - 1)
        Else
            Tiles = Nothing
        End If
    End Sub

    Private Sub CreateTiles()
        If RowCount > 0 AndAlso ColumnCount > 0 Then
            Tiles.ReDim(RowCount - 1, ColumnCount - 1)
        Else
            Tiles = Nothing
        End If
    End Sub

    Dim _innerControls As FrameworkElement(,)

    WithEvents TilesData As ObservableFixedArray2D(Of EditableTile)

    Private Sub UpdateTilesView(newValue As ObservableFixedArray2D(Of EditableTile))
        Dim curRowDef = GrdTiles.RowDefinitions
        Dim curColDef = GrdTiles.ColumnDefinitions
        Dim destRowCount = newValue.RowCount
        Dim destColCount = newValue.ColumnCount
        If Not (curRowDef.Count = destRowCount AndAlso destColCount = curColDef.Count) Then
            ' 裁剪底下一条
            For i = destRowCount To curRowDef.Count - 1
                For j = 0 To curColDef.Count - 1
                    If _innerControls(i, j) IsNot Nothing Then
                        GrdTiles.Children.Remove(_innerControls(i, j))
                    End If
                Next
            Next
            ' 裁剪右边一条
            For j = destColCount To curColDef.Count - 1
                For i = 0 To destRowCount - 1
                    If _innerControls(i, j) IsNot Nothing Then
                        GrdTiles.Children.Remove(_innerControls(i, j))
                    End If
                Next
            Next
            ' 修改图块控件表
            ReDim Preserve _innerControls(destRowCount - 1, destColCount - 1)
            ' 修改网格
            Do While destRowCount < curRowDef.Count
                curRowDef.RemoveAt(curRowDef.Count - 1)
            Loop
            Do While destRowCount > curRowDef.Count
                curRowDef.Add(New RowDefinition With {.Height = New GridLength(TileSize.Y)})
            Loop
            Do While destColCount < curColDef.Count
                curColDef.RemoveAt(curColDef.Count - 1)
            Loop
            Do While destColCount > curColDef.Count
                curColDef.Add(New ColumnDefinition With {.Width = New GridLength(TileSize.X)})
            Loop
        End If
        ' 同步行列宽度
        If curColDef.Any AndAlso Math.Abs(curColDef.First.Width.Value - TileSize.X) < 0.001 Then
            For Each cd In curColDef
                cd.Width = New GridLength(TileSize.X)
            Next
        End If
        If curRowDef.Any AndAlso Math.Abs(curRowDef.First.Height.Value - TileSize.Y) < 0.001 Then
            For Each cd In curRowDef
                cd.Height = New GridLength(TileSize.Y)
            Next
        End If
        ' 添加新的土块
        Dim template = DirectCast(Resources!TileEditorDataTemplate, DataTemplate)
        For i = 0 To destRowCount - 1
            For j = 0 To destColCount - 1
                Dim ctl = _innerControls(i, j)
                If ctl Is Nothing Then
                    ctl = DirectCast(template.LoadContent, FrameworkElement)
                    _innerControls(i, j) = ctl
                    Grid.SetRow(ctl, i)
                    Grid.SetColumn(ctl, j)
                    GrdTiles.Children.Add(ctl)
                End If
                ctl.DataContext = newValue(i, j)
            Next
        Next
        TilesData = newValue
    End Sub
    ''' <summary>
    ''' 点击的时候更新选中的图块
    ''' </summary>
    Private Sub BtnItem_Click(sender As Object, e As RoutedEventArgs)
        Dim ele = DirectCast(sender, FrameworkElement)
        SelectedTileX = Grid.GetRow(ele)
        SelectedTileY = Grid.GetColumn(ele)
    End Sub
    ''' <summary>
    ''' 通知已经修改了图块表。
    ''' </summary>
    Public Sub NotifyTileChanged()
        Tiles = Tiles
    End Sub

    Private Sub TilesData_CollectionChanged2D(sender As Object, e As NotifyCollectionChanged2DEventArgs(Of EditableTile)) Handles TilesData.CollectionChanged2D
        If e.Action = Specialized.NotifyCollectionChangedAction.Replace Then
            Dim index = e.NewStartingIndex
            Dim ele = _innerControls(index.RowIndex, index.ColIndex)
            ele.DataContext = e.NewItems(0, 0)
        End If
    End Sub
End Class
