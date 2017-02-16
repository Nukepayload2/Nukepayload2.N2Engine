Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Brushes
Imports Microsoft.Graphics.Canvas.Effects
Imports Microsoft.Graphics.Canvas.Text
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UWP.Marshal
Imports Windows.UI.Text

Friend Class TextBlockRenderer

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, UI.Controls.GameTextBlock)
        Dim txt = view.Text
        If txt.CanRead Then
            Dim fnt = view.Font
            If fnt IsNot Nothing Then
                Dim format As New CanvasTextFormat With {
                    .FontFamily = fnt.FontFamily,
                    .FontSize = fnt.FontSize,
                    .FontStretch = CType(CInt(fnt.FontStretch), FontStretch),
                    .FontStyle = CType(CInt(fnt.FontStyle), FontStyle)
                }
                SetFormat(fnt, format)
                If view.Transform IsNot Nothing Then
                    Dim loc = view.Location.Value
                    Using cl = New CanvasCommandList(args.DrawingSession)
                        Using ds = cl.CreateDrawingSession
                            ds.DrawText(txt.Value, New Vector2, New CanvasSolidColorBrush(args.DrawingSession, fnt.Color.AsWindowsColor), format)
                        End Using
                        Using transformEffect As New Transform2DEffect With {.Source = cl, .TransformMatrix = view.Transform.GetTransformMatrix}
                            args.DrawingSession.DrawImage(transformEffect, New Vector2(loc.X, loc.Y))
                        End Using
                    End Using
                Else
                    args.DrawingSession.DrawText(txt.Value, view.Location.Value,
                         New CanvasSolidColorBrush(args.DrawingSession, fnt.Color.AsWindowsColor), format)
                End If
            End If
        End If
    End Sub

    Private Shared Sub SetFormat(fnt As UI.Text.GameFont, format As CanvasTextFormat)
        With format
            Select Case fnt.FontWeight
                Case UI.Text.FontWeight.Normal
                    .FontWeight = FontWeights.Normal
                Case UI.Text.FontWeight.Thin
                    .FontWeight = FontWeights.Thin
                Case UI.Text.FontWeight.Bold
                    .FontWeight = FontWeights.Bold
                Case UI.Text.FontWeight.Black
                    .FontWeight = FontWeights.Black
                Case UI.Text.FontWeight.ExtraBlack
                    .FontWeight = FontWeights.ExtraBlack
                Case UI.Text.FontWeight.ExtraBold
                    .FontWeight = FontWeights.ExtraBold
                Case UI.Text.FontWeight.ExtraLight
                    .FontWeight = FontWeights.ExtraLight
                Case UI.Text.FontWeight.Light
                    .FontWeight = FontWeights.Light
                Case UI.Text.FontWeight.Medium
                    .FontWeight = FontWeights.Medium
                Case UI.Text.FontWeight.SemiBold
                    .FontWeight = FontWeights.SemiBold
                Case UI.Text.FontWeight.SemiLight
                    .FontWeight = FontWeights.SemiLight
            End Select
        End With
    End Sub
End Class
