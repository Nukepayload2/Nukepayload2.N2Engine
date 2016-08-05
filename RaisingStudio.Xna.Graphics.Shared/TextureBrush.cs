using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RaisingStudio.Xna.Graphics
{
    public abstract class TextureBrush : Brush
    {
        protected Texture2D texture;
        protected Rectangle region;
        
		/// <summary>
		/// Initializes a new instance of the <see cref="CanvasBrush"/> class.
		/// </summary>
		/// <param name="texture">The texture pattern.</param>
		/// <param name="region">The untransformed region.</param>
        protected TextureBrush(Texture2D texture, Rectangle region)
        {
            this.texture = texture;
            this.region = region;
		}

        /// <summary>
        /// Gets the pattern texture for this brush.
        /// </summary>
        /// <value>The pattern brush.</value>
        public override Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Gets the region of pattern to map on primitives.
        /// </summary>
        /// <value>The pattern region.</value>
        public override Rectangle Region
        {
            get { return region; }
        }
    }
}
