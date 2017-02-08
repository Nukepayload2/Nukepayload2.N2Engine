Imports System.Numerics
Imports Newtonsoft.Json

Public Class ShakingViewer
    Public Property ShakeX As Single
    Public Property ShakeY As Single

    Public Property Offset As Vector2

    Public Sub Shake(x As Single, y As Single)
        ShakeX = x
        ShakeY = y
    End Sub

    Public Sub Update()
        ShakeX = -ShakeX * 0.9F
        ShakeY = -ShakeY * 0.9F
        Offset = New Vector2(ShakeX, ShakeY)
    End Sub

    <JsonIgnore>
    Public Property UpdateAction As Action = AddressOf Update
End Class
