Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class SparkParticleSystemRenderer

    Sub New(view As SparkParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        Debug.WriteLine("准备火花资源")
        DirectCast(View, SparkParticleSystemView).Data.Value.RemoveFromGameCanvasCallback = AddressOf View.RemoveFromGameCanvas
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim drawingSession = args.DrawingSession
        Dim SparkSys = DirectCast(View, SparkParticleSystemView).Data.Value
        Using cl = New CanvasCommandList(drawingSession)
            Using ds = cl.CreateDrawingSession
                For Each part In SparkSys.Particles
                    ds.FillRectangle(New Rect(part.Location.ToPoint, New Size(part.SparkSize, part.SparkSize)), part.SparkColor.AsWindowsColor)
                Next
            End Using
            If View.Transform Is Nothing Then
                drawingSession.DrawImage(cl)
            Else
                Using cl2 = New CanvasCommandList(drawingSession)
                    Using ds2 = cl2.CreateDrawingSession
                        ds2.Transform = View.Transform.GetTransformMatrix
                    End Using
                    drawingSession.DrawImage(cl2)
                End Using
            End If
        End Using
    End Sub

    Public Overrides Sub DisposeResources()
        Debug.WriteLine("释放全部火花资源")
    End Sub
End Class