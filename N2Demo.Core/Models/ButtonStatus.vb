Imports System.Numerics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Information

Public Class ButtonStatus
    Public ReadOnly Property Background As New Color(&H7FEFEFEF)
    Public ReadOnly Property PointerOverBackground As New Color(&H7FFFFFFF)
    Public ReadOnly Property PressedBorderColor As New Color(&H7F000000)
    Public ReadOnly Property BorderColor As New Color(&H7F7F7F7F)
    Public ReadOnly Property Text As String = "跳"
    Public ReadOnly Property Position As Vector2
        Get
            Dim bbSize = BackBufferInformation.ViewPortSize
            Return New Vector2(bbSize.Width - Size.X - _Margin.Z, bbSize.Height - Size.Y - _Margin.W)
        End Get
    End Property

    Public ReadOnly Property Size As New Vector2(120.0F, 120.0F)
    Public ReadOnly Property TextOffset As New Vector2(53.0F, 53.0F)

    ReadOnly _Margin As New Vector4(0F, 0F, 24.0F, 24.0F)

End Class
