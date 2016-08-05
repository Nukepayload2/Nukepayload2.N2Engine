using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace RaisingStudio.Xna.Graphics
{
    public class TextureContext
    {
        private Color[] data;
        public Color[] Data
        {
            get { return this.data; }
        }

        private int width;
        public int Width
        {
            get { return this.width; }
        }

        private int height;
        public int Height
        {
            get { return this.Height; }
        }

        private Texture2D texture;
        public Texture2D Texture
        {
            get { return this.texture; }
        }

        public TextureContext(Color[] data, int width, int height)
        {
            this.data = data;
            this.width = width;
            this.height = height;
        }

        public TextureContext(Texture2D texture)
        {
            this.texture = texture;
            this.width = this.texture.Width;
            this.height = this.texture.Height;
        }

        #region Begin & End
        public void Begin()
        {
            if (this.texture != null)
            {
                this.data = new Color[this.texture.Width * this.texture.Height];
                this.texture.GetData<Color>(this.data);
            }
        }

        public void End()
        {
            if (this.texture != null)
            {
                this.texture.SetData<Color>(this.data);
            }
        }
        #endregion

        #region Pixel
        public void SetPixel(int x, int y, Color color)
        {
            if (((x >= 0) && (x < width)) && ((y >= 0) && (y < height)))
            {
                this.data[y * this.width + x] = color;
            }
        }

        public Color GetPixel(int x, int y)
        {
            if (((x >= 0) && (x < width)) && ((y >= 0) && (y < height)))
            {
                return this.data[y * this.width + x];
            }
            throw new IndexOutOfRangeException();
        }
        #endregion

        #region DrawPoints & DrawColors
        public virtual void DrawPoints(Vector2[] points, Vector2 location, Color color)
        {
            foreach (Vector2 point in points)
            {
                int x = ((int)point.X + (int)location.X);
                if ((x >= 0) && (x < width))
                {
                    int y = ((int)point.Y + (int)location.Y);
                    if ((y >= 0) && (y < height))
                    {
                        int index = width * y + x;
                        if ((index >= 0) && (index < data.Length))
                        {
                            data[index] = color;
                        }
                    }
                }
            }
        }

        public virtual void DrawColors(VertexPositionColor[] colors, Vector2 location)
        {
            foreach (VertexPositionColor vertexPositionColor in colors)
            {
                int x = ((int)vertexPositionColor.Position.X + (int)location.X);
                if ((x >= 0) && (x < width))
                {
                    int y = ((int)vertexPositionColor.Position.Y + (int)location.Y);
                    if ((y >= 0) && (y < height))
                    {
                        int index = width * y + x;
                        if ((index >= 0) && (index < data.Length))
                        {
                            data[index] = vertexPositionColor.Color;
                        }
                    }
                }
            }
        }

        public virtual void DrawColors(Color[] colors, Vector2 size, Vector2 location)
        {
            if (size == Vector2.Zero)
            {
                size = new Vector2(width, height);
            }
            for (int i = 0; i < colors.Length; i++)
            {
                int x = (int)(i % (int)size.X) + (int)location.X;
                Color color = colors[i];
                if ((x >= 0) && (x < width))
                {
                    int y = (int)(i / (int)size.X) + (int)location.Y;
                    if ((y >= 0) && (y < height))
                    {
                        int index = width * y + x;
                        if ((index >= 0) && (index < data.Length))
                        {
                            if (color != Color.Transparent)
                            {
                                data[index] = color;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region DrawLine
        public virtual void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            DrawLine(start.X, start.Y, end.X, end.Y, color);
        }

        public virtual void DrawLine(float startX, float startY, float endX, float endY, Color color)
        {
            ProcessDrawLine((int)startX, (int)startY, (int)endX, (int)endY, color);
        }

        public virtual void DrawLine(int startX, int startY, int endX, int endY, Color color)
        {
            ProcessDrawLine(startX, startY, endX, endY, color);
        }

        protected virtual void ProcessDrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            int dx = (x0 < x1) ? (x1 - x0) : (x0 - x1);
            int sx = (x0 < x1) ? 1 : -1;
            int dy = (y0 < y1) ? (y1 - y0) : (y0 - y1);
            int sy = (y0 < y1) ? 1 : -1;
            int e2, err = dx - dy;
            while (true)
            {
                if (((x0 >= 0) && (x0 < width)) && ((y0 >= 0) && (y0 < height)))
                {
                    this.data[y0 * width + x0] = color;
                }

                if ((x0 == x1) && (y0 == y1)) return;

                e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }
        #endregion

        #region DrawRectangle
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawRectangle(Rectangle rectangle, Color color)
        {
            ProcessDrawRectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, color);
        }

        public virtual void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            ProcessDrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        }

        public virtual void DrawRectangle(Vector2 location, Vector2 size, Color color)
        {
            DrawRectangle(location.X, location.Y, size.X, size.Y, color);
        }

        public virtual void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            ProcessDrawRectangle(x, y, width, height, color);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x of left top rect corner.</param>
        /// <param name="y">The y of left top rect cornder.</param>
        /// <param name="width">The width of rect.</param>
        /// <param name="height">The height of rect.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawRectangle(int x, int y, int width, int height, Color color)
        {
            ProcessDrawLine(x, y, x + width, y, color);
            ProcessDrawLine(x + width, y, x + width, y + height, color);
            ProcessDrawLine(x + width, y + height, x, y + height, color);
            ProcessDrawLine(x, y + height, x, y, color);
        }
        #endregion

        #region DrawTriangle
        public virtual void DrawTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            DrawTriangle(vertex1.X, vertex1.Y, vertex2.X, vertex2.Y, vertex3.X, vertex3.Y, color);
        }
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="x1">The vertex1 X coord.</param>
        /// <param name="y1">The vertex1 Y coord.</param>
        /// <param name="x2">The vertex2 X coord.</param>
        /// <param name="y2">The vertex2 Y coord.</param>
        /// <param name="x3">The vertex3 X coord.</param>
        /// <param name="y3">The vertex3 Y coord.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color color)
        {
            DrawTriangle((int)x1, (int)y1, (int)x2, (int)y2, (int)x3, (int)y3, color);
        }

        public virtual void DrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3, Color color)
        {
            ProcessDrawTriangle(x1, y1, x2, y2, x3, y3, color);
        }

        protected virtual void ProcessDrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3, Color color)
        {
            ProcessDrawLine(x1, y1, x2, y2, color);
            ProcessDrawLine(x2, y2, x3, y3, color);
            ProcessDrawLine(x3, y3, x1, y1, color);
        }
        #endregion

        #region DrawEllipse
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="region">The region to draw ellipse.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawEllipse(Rectangle region, Color color)
        {
            DrawEllipse(region.Left, region.Top, region.Width, region.Height, color);
        }

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="x">The x of left top point of rectangular region.</param>
        /// <param name="y">The y of left top point of rectangular region..</param>
        /// <param name="width">The width of rectangular region.</param>
        /// <param name="height">The height of rectangular region.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawEllipse(float x, float y, float width, float height, Color color)
        {
            DrawEllipse(new Vector2(x, y), new Vector2(width, height), color);
        }

        public virtual void DrawEllipse(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawEllipse(location, size, color);
        }

        protected virtual void ProcessDrawEllipse(Vector2 location, Vector2 size, Color color)
        {
            Vector2[] offsets = VertexFragment.EllipseSegments;
            Vector2[] points = new Vector2[offsets.Length];
            size -= new Vector2(1f, 1f);
            for (int i = 0; i < offsets.Length; i++)
            {
                points[i] = offsets[i] * size + location;
            }
            ProcessDrawPolyline(points, 0, points.Length, true, color);
        }
        #endregion

        #region DrawPolyline
        /// <summary>
        /// Draws the polyline.
        /// </summary>
        /// <param name="points">The array with points of polyline.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawPolyline(Vector2[] points, Color color)
        {
            DrawPolyline(points, 0, -1, false, color);
        }

        /// <summary>
        /// Draws the polyline.
        /// </summary>
        /// <param name="points">The array with points of polyline.</param>
        /// <param name="closed">if set to <c>true</c> to connect last point with the first one.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawPolyline(Vector2[] points, bool closed, Color color)
        {
            DrawPolyline(points, 0, -1, closed, color);
        }

        public virtual void DrawPolyline(Vector2[] points, int start, int count, bool closed, Color color)
        {
            ProcessDrawPolyline(points, start, count, closed, color);
        }

        /// <summary>
        /// Draws the polyline.
        /// </summary>
        /// <param name="points">The array with points of polyline.</param>
        /// <param name="start">The start element in array to use.</param>
        /// <param name="count">The element count to use.</param>
        /// <param name="closed">if set to <c>true</c> to connect last point with the first one.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawPolyline(Vector2[] points, int start, int count, bool closed, Color color)
        {
            count = DrawingBatch.CheckPoints(points, start, count);
            if (count > 1)
            {
                Vector2 firstPoint = points[start];
                Vector2 latestPoint = firstPoint;
                for (int i = 1; i < count; i++)
                {
                    int index = i + start;
                    Vector2 point = points[index];
                    ProcessDrawLine((int)latestPoint.X, (int)latestPoint.Y, (int)point.X, (int)point.Y, color);
                    latestPoint = point;
                }
                if (closed)
                {
                    ProcessDrawLine((int)latestPoint.X, (int)latestPoint.Y, (int)firstPoint.X, (int)firstPoint.Y, color);
                }
            }
            else
            {
                throw new ArgumentException("Drawing polyline/polygon required at least 2 points");
            }
        }
        #endregion

        public void Fill(int x, int y, Color color)
        {
            Fill(x, y, color, color);
        }

        public void Fill(int x, int y, Color boundaryColor, Color filledColor)
        {
            if (((x >= 0) && (x < width)) && ((y >= 0) && (y < height)))
            {
                Color color = this.data[y * this.width + x];
                if (color != boundaryColor && color != filledColor)
                {
                    this.data[y * this.width + x] = filledColor;

                    Fill(x + 1, y, boundaryColor, filledColor);
                    Fill(x - 1, y, boundaryColor, filledColor);
                    Fill(x, y + 1, boundaryColor, filledColor);
                    Fill(x, y - 1, boundaryColor, filledColor);
                }
            }
        }

        public void Fill(Vector2[] points, Color color)
        {
            Fill(points, Vector2.Zero, color);
        }

        public void Fill(Vector2[] points, Vector2 location, Color color)
        {
            int minX;
            int maxX;
            int minY;
            int maxY;
            GetBound(points, out minX, out maxX, out minY, out maxY);
            for (int y = minY+1; y < maxY; y++)
            {
                Vector2[] crossPoints = GetCrossPoints(points, new Vector2(minX, y), new Vector2(maxX, y));
                Vector2[] orderedCrossPoints = crossPoints.Select(c => new Vector2((float)Math.Round(c.X), y)).Distinct(c => c.X).OrderBy(c => c.X).ToArray();
                if (orderedCrossPoints.Length > 1)
                {
                    for (int i = 0; i < orderedCrossPoints.Length; i += 2)
                    {
                        if (i + 1 < orderedCrossPoints.Length)
                        {
                            DrawLine(orderedCrossPoints[i] + location, orderedCrossPoints[i + 1] + location, color);
                        }
                    }
                }
            }
        }

        private void GetBound(Vector2[] points, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = width;
            maxX = 0;

            minY = height;
            maxY = 0;

            for (int i = 0; i < points.Length; i++)
            {
                Vector2 point = points[i];
                int pointX = (int)point.X;
                int pointY = (int)point.Y;
                if (pointX > maxX)
                {
                    maxX = pointX;
                }
                if (pointX < minX)
                {
                    minX = pointX;
                }
                if (pointY > maxY)
                {
                    maxY = pointY;
                }
                if (pointY < minY)
                {
                    minY = pointY;
                }
            }
        }

        public const float Epsilon = 1.192092896e-07f;
        /// <summary>
        /// This method detects if two line segments (or lines) intersect,
        /// and, if so, the point of intersection. Use the <paramref name="firstIsSegment"/> and
        /// <paramref name="secondIsSegment"/> parameters to set whether the intersection point
        /// must be on the first and second line segments. Setting these
        /// both to true means you are doing a line-segment to line-segment
        /// intersection. Setting one of them to true means you are doing a
        /// line to line-segment intersection test, and so on.
        /// Note: If two line segments are coincident, then 
        /// no intersection is detected (there are actually
        /// infinite intersection points).
        /// Author: Jeremy Bell
        /// </summary>
        /// <param name="point1">The first point of the first line segment.</param>
        /// <param name="point2">The second point of the first line segment.</param>
        /// <param name="point3">The first point of the second line segment.</param>
        /// <param name="point4">The second point of the second line segment.</param>
        /// <param name="point">This is set to the intersection
        /// point if an intersection is detected.</param>
        /// <param name="firstIsSegment">Set this to true to require that the 
        /// intersection point be on the first line segment.</param>
        /// <param name="secondIsSegment">Set this to true to require that the
        /// intersection point be on the second line segment.</param>
        /// <returns>True if an intersection is detected, false otherwise.</returns>
        public static bool LineIntersect(ref Vector2 point1, ref Vector2 point2, ref Vector2 point3, ref Vector2 point4,
                                         bool firstIsSegment, bool secondIsSegment,
                                         out Vector2 point)
        {
            point = new Vector2();

            // these are reused later.
            // each lettered sub-calculation is used twice, except
            // for b and d, which are used 3 times
            float a = point4.Y - point3.Y;
            float b = point2.X - point1.X;
            float c = point4.X - point3.X;
            float d = point2.Y - point1.Y;

            // denominator to solution of linear system
            float denom = (a * b) - (c * d);

            // if denominator is 0, then lines are parallel
            if (!(denom >= -Epsilon && denom <= Epsilon))
            {
                float e = point1.Y - point3.Y;
                float f = point1.X - point3.X;
                float oneOverDenom = 1.0f / denom;

                // numerator of first equation
                float ua = (c * e) - (a * f);
                ua *= oneOverDenom;

                // check if intersection point of the two lines is on line segment 1
                if (!firstIsSegment || ua >= 0.0f && ua <= 1.0f)
                {
                    // numerator of second equation
                    float ub = (b * e) - (d * f);
                    ub *= oneOverDenom;

                    // check if intersection point of the two lines is on line segment 2
                    // means the line segments intersect, since we know it is on
                    // segment 1 as well.
                    if (!secondIsSegment || ub >= 0.0f && ub <= 1.0f)
                    {
                        // check if they are coincident (no collision in this case)
                        if (ua != 0f || ub != 0f)
                        {
                            //There is an intersection
                            point.X = (point1.X + ua * b);
                            point.Y = (point1.Y + ua * d);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This method detects if two line segments (or lines) intersect,
        /// and, if so, the point of intersection. Use the <paramref name="firstIsSegment"/> and
        /// <paramref name="secondIsSegment"/> parameters to set whether the intersection point
        /// must be on the first and second line segments. Setting these
        /// both to true means you are doing a line-segment to line-segment
        /// intersection. Setting one of them to true means you are doing a
        /// line to line-segment intersection test, and so on.
        /// Note: If two line segments are coincident, then 
        /// no intersection is detected (there are actually
        /// infinite intersection points).
        /// Author: Jeremy Bell
        /// </summary>
        /// <param name="point1">The first point of the first line segment.</param>
        /// <param name="point2">The second point of the first line segment.</param>
        /// <param name="point3">The first point of the second line segment.</param>
        /// <param name="point4">The second point of the second line segment.</param>
        /// <param name="intersectionPoint">This is set to the intersection
        /// point if an intersection is detected.</param>
        /// <param name="firstIsSegment">Set this to true to require that the 
        /// intersection point be on the first line segment.</param>
        /// <param name="secondIsSegment">Set this to true to require that the
        /// intersection point be on the second line segment.</param>
        /// <returns>True if an intersection is detected, false otherwise.</returns>
        public static bool LineIntersect(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4,
                                         bool firstIsSegment,
                                         bool secondIsSegment, out Vector2 intersectionPoint)
        {
            return LineIntersect(ref point1, ref point2, ref point3, ref point4, firstIsSegment, secondIsSegment,
                                 out intersectionPoint);
        }

        public Vector2[] GetCrossPoints(Vector2[] points, Vector2 vector1, Vector2 vector2)
        {
            List<Vector2> crossPoints = new List<Vector2>();
            Vector2 crossPoint;
            for (int i = 1; i < points.Length; i++)
            {
                if (LineIntersect(points[i - 1], points[i], vector1, vector2, true, true, out crossPoint))
                {
                    crossPoints.Add(crossPoint);
                }
            }
            if (LineIntersect(points[points.Length - 1], points[0], vector1, vector2, true, true, out crossPoint))
            {
                crossPoints.Add(crossPoint);
            }
            return crossPoints.ToArray();
        }

        #region DrawFilledRectangle
        /// <summary>
        /// Draws the solid simple rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawFilledRectangle(Rectangle rectangle, Color color)
        {
            DrawFilledRectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, color);
        }

        public virtual void DrawFilledRectangle(float x, float y, float width, float height, Color color)
        {
            DrawFilledRectangle(new Vector2(x, y), new Vector2(width, height), color);
        }
        public virtual void DrawFilledRectangle(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawFilledRectangle(location, size, color);
        }
        /// <summary>
        /// Draws the solid simple rectangle.
        /// </summary>
        /// <param name="x">The x of left top rect corner.</param>
        /// <param name="y">The y of left top rect cornder.</param>
        /// <param name="width">The width of rect.</param>
        /// <param name="height">The height of rect.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawFilledRectangle(Vector2 location, Vector2 size, Color color)
        {
            if (size.X > 0 && size.Y > 0)
            {
                Color[] contextData = new Color[this.Data.Length];
                TextureContext textureContext = new TextureContext(contextData, width, height);
                //textureContext.DrawRectangle(location, size, color);
                textureContext.Fill(new Vector2[] { location, new Vector2(location.X + size.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y), new Vector2(location.X, location.Y + size.Y) }, color);
                DrawColors(textureContext.Data, new Vector2(width, height), Vector2.Zero);
            }
        }
        #endregion

        #region DrawFilledTriangle
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="x1">The vertex1 X coord.</param>
        /// <param name="y1">The vertex1 Y coord.</param>
        /// <param name="x2">The vertex2 X coord.</param>
        /// <param name="y2">The vertex2 Y coord.</param>
        /// <param name="x3">The vertex3 X coord.</param>
        /// <param name="y3">The vertex3 Y coord.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawFilledTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color color)
        {
            DrawFilledTriangle(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3), color);
        }
        public virtual void DrawFilledTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            ProcessDrawFilledTriangle(vertex1, vertex2, vertex3, color);
        }
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="vertex1">The vertex1 reference.</param>
        /// <param name="vertex2">The vertex2 reference.</param>
        /// <param name="vertex3">The vertex3 reference.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawFilledTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            Color[] contextData = new Color[this.Data.Length];
            TextureContext textureContext = new TextureContext(contextData, width, height);
            //textureContext.DrawTriangle(vertex1, vertex2, vertex3, color);
            textureContext.Fill(new Vector2[] { vertex1, vertex2, vertex3 }, color);
            DrawColors(textureContext.Data, new Vector2(width, height), Vector2.Zero);
        }
        #endregion

        #region DrawFilledEllipse
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="region">The region to draw ellipse.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawFilledEllipse(Rectangle region, Color color)
        {
            DrawFilledEllipse(region.Left, region.Top, region.Width, region.Height, color);
        }

        public virtual void DrawFilledEllipse(float x, float y, float width, float height, Color color)
        {
            DrawFilledEllipse(new Vector2(x, y), new Vector2(width, height), color);
        }
        public virtual void DrawFilledEllipse(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawFilledEllipse(location, size, color);
        }
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="x">The x of left top point of rectangular region.</param>
        /// <param name="y">The y of left top point of rectangular region..</param>
        /// <param name="width">The width of rectangular region.</param>
        /// <param name="height">The height of rectangular region.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawFilledEllipse(Vector2 location, Vector2 size, Color color)
        {
            Color[] contextData = new Color[this.Data.Length];
            TextureContext textureContext = new TextureContext(contextData, width, height);
            Vector2[] offsets = VertexFragment.EllipseSegments;
            Vector2[] points = new Vector2[offsets.Length];
            size -= new Vector2(1f, 1f);
            for (int i = 0; i < offsets.Length; i++)
            {
                points[i] = offsets[i] * size + location;
            }
            //ProcessDrawPolyline(points, 0, points.Length, true, color);
            textureContext.Fill(points, color);
            DrawColors(textureContext.Data, new Vector2(width, height), Vector2.Zero);
        }
        #endregion
    }
}

