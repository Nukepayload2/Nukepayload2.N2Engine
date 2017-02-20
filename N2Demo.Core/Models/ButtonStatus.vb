Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation

Public Class ButtonStatus
    Public ReadOnly Property Background As New Color(&HFFEFEFEF)
    Public ReadOnly Property PointerOverBackground As New Color(&HFFFFFFFF)
    Public ReadOnly Property PressedBorderColor As New Color(&HFF000000)
    Public ReadOnly Property BorderColor As New Color(&HFF7F7F7F)
    Public ReadOnly Property Text As String
        Get
            If ClickCount <= 0 Then
                Return "点击我！"
            Else
                Return $"点了{ClickCount}次"
            End If
        End Get
    End Property
    Public ReadOnly Property Position As New Vector2(400.0F, 10.0F)
    Public ReadOnly Property Size As New Vector2(96.0F, 32.0F)
    Public ReadOnly Property TextOffset As New Vector2(16.0F, 8.0F)
    Public Property ClickCount As Integer
End Class
