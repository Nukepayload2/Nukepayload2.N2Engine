using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaisingStudio.Xna.Graphics
{
    /// <summary>
    /// Defines line stroke pattern. There are some prepared strokes but you can define own pattern. 
    /// Only one restriction for correct custom pattern display: it should be placed from left to right across whole texture!
    /// Note, you can put even animated pattern, because sprite provider resolves required sprite runtime
    /// </summary>
    public class LineStroke
    {
        private TextureBrush _brush;
        /// <summary>
        /// Gets the pattern brush for this stroke.
        /// </summary>
        /// <value>The pattern brush.</value>
        public TextureBrush Brush
        {
            get { return _brush; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineStroke"/> class.
        /// </summary>
        /// <param name="TextureBrush">The pattern brush.</param>
        public LineStroke(TextureBrush brush)
        {
            if (null == brush) throw new ArgumentException("brush");
            _brush = brush;
        }

        #region Presets

        ///<summary>Solid line stroking</summary>
        public static LineStroke Solid { get; internal set; }

        ///<summary>Dual line stroking</summary>
        public static LineStroke Dual { get; internal set; }

        ///<summary>Smooth line stroking</summary>
        public static LineStroke Smooth { get; internal set; }

        ///<summary>Tiny dashed line stroking</summary>
        public static LineStroke TinyDashed { get; internal set; }

        ///<summary>Dashed line stroking</summary>
        public static LineStroke Dashed { get; internal set; }

        ///<summary>Combined dashed stroking</summary>
        public static LineStroke CombinedDashed { get; internal set; }

        ///<summary>Dot-dashed stroking</summary>
        public static LineStroke DotDashed { get; internal set; }

        ///<summary>Dotted stroking</summary>
        public static LineStroke Dotted { get; internal set; }

        #endregion

    }
}
