using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RaisingStudio.Xna.Graphics
{
    public class DrawingComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        public DrawingTexture DrawingTexture
        {
            get;
            private set;
        }

        public DrawingContext DrawingContext
        {
            get
            {
                return this.DrawingTexture.DrawingContext;
            }
        }

        #region Constructor
        public DrawingComponent(Game game)
            : base(game)
        {
        }
        #endregion

        protected override void LoadContent()
        {
            base.LoadContent();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            int bufferWidth = pp.BackBufferWidth;
            int bufferHeight = pp.BackBufferHeight;
            this.DrawingTexture = new DrawingTexture(GraphicsDevice, bufferWidth, bufferHeight);
        }

        private bool drawable = false;
        public bool Drawable
        {
            get
            {
                return this.drawable;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (drawable)
            {
                this.DrawingTexture.End();
                this.drawable = false;
            }
            spriteBatch.Begin();
            spriteBatch.Draw(this.DrawingTexture, this.DrawingTexture.Bounds, Color.White);
            spriteBatch.End();
            if (!drawable)
            {
                this.DrawingTexture.Begin();
                this.drawable = true;
            }
            base.Draw(gameTime);
        }
    }
}
