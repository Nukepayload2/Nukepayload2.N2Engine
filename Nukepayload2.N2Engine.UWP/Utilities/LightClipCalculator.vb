Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Geometry
Imports Nukepayload2.N2Engine.UWP.Geometry

Namespace Lighting
    Public Class LightClipCalculator
        ''' <summary>
        ''' 线段
        ''' </summary>
        Private Structure LineSegment
            Dim Point1, Point2 As Vector2
            Public Sub New(point1 As Vector2, point2 As Vector2)
                Me.Point1 = point1
                Me.Point2 = point2
            End Sub
            Public ReadOnly Property Length As Single
                Get
                    Return (Point2 - Point1).Length
                End Get
            End Property
            Const Allowance! = 0.01
            ''' <summary>
            ''' 两个线段有没有交点
            ''' </summary>
            Public Function HasIntersection(another As LineSegment) As Boolean
                Dim inters = Intersection(another)
                If inters.HasValue Then
                    Return (inters.Value - Point1).Length > Allowance AndAlso (inters.Value - Point2).Length > Allowance AndAlso
                        (inters.Value - another.Point1).Length > Allowance AndAlso (inters.Value - another.Point2).Length > Allowance
                End If
                Return False
            End Function
            ''' <summary>
            ''' 求线段的交点
            ''' </summary>
            Public Function Intersection(another As LineSegment) As Vector2?
                Dim a = Point1, b = Point2, c = another.Point1, d = another.Point2
                Dim sabc = (a.X - c.X) * (b.Y - c.Y) - (a.Y - c.Y) * (b.X - c.X)
                Dim sabd = (a.X - d.X) * (b.Y - d.Y) - (a.Y - d.Y) * (b.X - d.X)
                If sabc * sabd >= 0 Then Return Nothing
                Dim scda = (c.X - a.X) * (d.Y - a.Y) - (c.Y - a.Y) * (d.X - a.X)
                Dim scdb = scda + sabc - sabd
                If scda * scdb >= 0 Then Return Nothing
                Dim t = scda / (sabd - sabc)
                Dim dx = t * (b.X - a.X), dy = t * (b.Y - a.Y)
                Return New Vector2(a.X + dx, a.Y + dy)
            End Function
            ''' <summary>
            ''' 以Point1为起点，将线段的长度延伸到边界之外充当射线以便计算
            ''' </summary>
            Public Function RayToBoundary(BoundarySize As Size) As LineSegment
                Dim length! = CSng(BoundarySize.Width + BoundarySize.Height)
                Dim direction = Point2 - Point1
                Point2 = Point1 + length / direction.Length() * direction
                Return Me
            End Function
            Public Function AngleX#()
                Dim direction = Point2 - Point1
                Dim cosval = Math.Acos(direction.X / direction.Length)
                Return If(direction.Y < 0, 2 * Math.PI - cosval, cosval)
            End Function
            Public Function Angle#(other As LineSegment)
                Dim v1 = Point2 - Point1
                Dim v2 = other.Point2 - other.Point1
                Dim v1v2 = v1.X * v2.X + v1.Y * v2.Y
                Dim acos = Math.Acos(v1v2 / (v1.Length * v2.Length))
                Return If(v1.X * v2.Y < v2.X * v1.Y, 2 * Math.PI - acos, acos)
            End Function
        End Structure

        Public Function CalculateClipGeometry(resource As ICanvasResourceCreator, SourcePoint As Vector2, Geometies As CanvasGeometry(), ScreenSize As Size) As CanvasGeometry
            Dim geos = Aggregate geo In Geometies
                           Let Lines = Aggregate tes In geo.Tessellate
                                       From ln In {New LineSegment(tes.Vertex1, tes.Vertex2), New LineSegment(tes.Vertex1, tes.Vertex3), New LineSegment(tes.Vertex3, tes.Vertex2)}
                                       Select ln Distinct Into ToArray
                           Select Rays = Aggregate tes In geo.Tessellate
                               From light In {New LineSegment(SourcePoint, tes.Vertex1), New LineSegment(SourcePoint, tes.Vertex2), New LineSegment(SourcePoint, tes.Vertex3)}
                                   Where Not (Aggregate l In Lines Where light.RayToBoundary(ScreenSize).HasIntersection(l) Into Any)
                               Select light
                           Into ToArray
                           Where Rays.Length >= 2
                           Let Fir = Rays.First
                           Select Arr = Aggregate ln In Rays Order By ln.Angle(Fir) Into ToArray
                           Select CanvasGeometry.CreatePolygon(resource, {Arr.First.Point2, Arr.First.RayToBoundary(ScreenSize).Point2, Arr.Last.RayToBoundary(ScreenSize).Point2, Arr.Last.Point2})
                       Into ToArray
            Return geos.Union
        End Function

    End Class
End Namespace
