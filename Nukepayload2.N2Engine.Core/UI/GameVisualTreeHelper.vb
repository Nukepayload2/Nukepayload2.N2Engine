Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI

    Public Class GameVisualTreeHelper
        ''' <summary>
        ''' 获取一个可见对象的绝对位置
        ''' </summary>
        ''' <param name="visual"></param>
        ''' <returns></returns>
        Public Shared Function GetAbsolutePosition(visual As GameVisual) As Vector2
            Dim loc As New Vector2
            Do
                If visual.Location.CanRead Then
                    loc += visual.Location.Value
                End If
                visual = visual.Parent
            Loop While visual IsNot Nothing
            Return loc
        End Function

        ''' <summary>
        ''' 判断点击的点是否落在矩形元素的范围内
        ''' </summary>
        ''' <param name="point">要判断的点</param>
        ''' <returns>点击的点是否落在矩形的范围内</returns>
        Public Shared Function HitTest(visual As GameVisual, point As Vector2) As Boolean
            Dim absolutePosition = GetAbsolutePosition(visual)
            If point.X >= absolutePosition.X AndAlso point.Y >= absolutePosition.Y Then
                absolutePosition += visual.Size.Value
                If point.X <= absolutePosition.X AndAlso point.Y <= absolutePosition.Y Then
                    Return True
                End If
            End If
            Return False
        End Function
    End Class

End Namespace