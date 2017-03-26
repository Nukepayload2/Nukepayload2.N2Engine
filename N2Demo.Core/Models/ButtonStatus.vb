Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Information

Public Class ButtonStatus
    Public ReadOnly Property Background As New Color(&HFFEFEFEF)
    Public ReadOnly Property PointerOverBackground As New Color(&HFFFFFFFF)
    Public ReadOnly Property PressedBorderColor As New Color(&HFF000000)
    Public ReadOnly Property BorderColor As New Color(&HFF7F7F7F)
    Public ReadOnly Property Text As String = "跳"
    Public ReadOnly Property Position As Vector2
    Public ReadOnly Property Size As New Vector2(48.0F, 48.0F)
    Public ReadOnly Property TextOffset As New Vector2(16.0F, 16.0F)

    ReadOnly _Margin As New Vector4(0F, 0F, 4.0F, 4.0F)

    Sub New()
        Dim bbSize = BackBufferInformation.Size
        Position = New Vector2(bbSize.Width - Size.X * 2 - _Margin.Z, bbSize.Height - Size.Y * 2 - _Margin.W)
    End Sub

End Class
