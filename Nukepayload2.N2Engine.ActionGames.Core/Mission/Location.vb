''' <summary>
''' 用于定位在地图块中的位置与方向
''' </summary>
Public Structure Location
    ''' <summary>
    ''' x坐标
    ''' </summary>
    Property X As Integer
    ''' <summary>
    ''' y坐标
    ''' </summary>
    Property Y As Integer
    ''' <summary>
    ''' 方向
    ''' </summary>
    Property Direction As Directions
End Structure
