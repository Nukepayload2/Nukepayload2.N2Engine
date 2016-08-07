using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework;

namespace RaisingStudio.Xna.Graphics
{
    public class DrawingContext : DrawingBatch
    {
        public SpriteBatch SpriteBatch { get; private set; }
        private SpriteSortMode spriteSortMode;
        private BlendState blendState;
        private SamplerState samplerState;
        private DepthStencilState depthStencilState;
        private RasterizerState rasterizerState;
        private Effect customEffect;
        private Matrix transformMatrix;

        #region Constructor
        public DrawingContext(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public DrawingContext(RenderTarget2D renderTarget)
            : base(renderTarget)
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public DrawingContext(RenderTarget2D renderTarget, Texture2D background)
            : base(renderTarget, background)
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
        #endregion

        #region Commands

		private ConcurrentQueue<ContextCommand> _commands = new ConcurrentQueue<ContextCommand>();

        #endregion

        #region Draw
        public override void DrawLine(Microsoft.Xna.Framework.Vector2 start, Microsoft.Xna.Framework.Vector2 end, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawLine, Vectors = new Microsoft.Xna.Framework.Vector2[] { start, end }, Color = color });
        }

        public override void DrawRectangle(Microsoft.Xna.Framework.Vector2 location, Microsoft.Xna.Framework.Vector2 size, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawRectangle, Vectors = new Microsoft.Xna.Framework.Vector2[] { location, size }, Color = color });
        }

        public override void DrawTriangle(Microsoft.Xna.Framework.Vector2 vertex1, Microsoft.Xna.Framework.Vector2 vertex2, Microsoft.Xna.Framework.Vector2 vertex3, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawTriangle, Vectors = new Microsoft.Xna.Framework.Vector2[] { vertex1, vertex2, vertex3 }, Color = color });
        }

        public override void DrawEllipse(Microsoft.Xna.Framework.Vector2 location, Microsoft.Xna.Framework.Vector2 size, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawEllipse, Vectors = new Microsoft.Xna.Framework.Vector2[] { location, size }, Color = color });
        }

        public override void DrawPolyline(Microsoft.Xna.Framework.Vector2[] points, int start, int count, bool closed, Microsoft.Xna.Framework.Color color)
        {
            count = CheckPoints(points, start, count);
            Microsoft.Xna.Framework.Vector2[] copiedPoints = new Microsoft.Xna.Framework.Vector2[count];
            Array.Copy(points, start, copiedPoints, 0, count);
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawPolyline, Vectors = copiedPoints, Color = color, Addtions = new object[] { closed } });
        }


        public override void DrawFilledRectangle(Microsoft.Xna.Framework.Vector2 location, Microsoft.Xna.Framework.Vector2 size, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawFilledRectangle, Vectors = new Microsoft.Xna.Framework.Vector2[] { location, size }, Color = color });
        }

        public override void DrawFilledTriangle(Microsoft.Xna.Framework.Vector2 vertex1, Microsoft.Xna.Framework.Vector2 vertex2, Microsoft.Xna.Framework.Vector2 vertex3, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawFilledTriangle, Vectors = new Microsoft.Xna.Framework.Vector2[] { vertex1, vertex2, vertex3 }, Color = color });
        }

        public override void DrawFilledEllipse(Microsoft.Xna.Framework.Vector2 location, Microsoft.Xna.Framework.Vector2 size, Microsoft.Xna.Framework.Color color)
        {
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawFilledEllipse, Vectors = new Microsoft.Xna.Framework.Vector2[] { location, size }, Color = color });
        }
        #endregion

        #region Sprites
        #region DrawPoints
        public virtual void DrawPoints(Vector2[] points, Color color)
        {
            DrawPoints(points, 0, -1, color);
        }

        public virtual void DrawPoints(Vector2[] points, int start, int count, Color color)
        {
            DrawPoints(points, start, count, Vector2.Zero, color);
        }

        public virtual void DrawPoints(Vector2[] points, int start, int count, Vector2 location, Color color)
        {
            count = CheckPoints(points, start, count);
            Microsoft.Xna.Framework.Vector2[] copiedPoints = new Microsoft.Xna.Framework.Vector2[count];
            Array.Copy(points, start, copiedPoints, 0, count);
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawPoints, Vectors = copiedPoints, Color = color, Addtions = new object[] { location } });
        }
        #endregion

        #region DrawColors
        public virtual void DrawColors(VertexPositionColor[] colors)
        {
            DrawColors(colors, Vector2.Zero);
        }

        public virtual void DrawColors(VertexPositionColor[] colors, Vector2 location)
        {
            if (colors == null) throw new ArgumentNullException("colors");

            VertexPositionColor[] copiedColors = new VertexPositionColor[colors.Length];
            colors.CopyTo(copiedColors, 0);
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawColors, Vectors = new Vector2[] { location }, Addtions = new object[] { copiedColors } });
        }

        public virtual void DrawColors(Color[] colors)
        {
            DrawColors(colors, Vector2.Zero, Vector2.Zero);
        }

        public virtual void DrawColors(Color[] colors, Vector2 size)
        {
            DrawColors(colors, Vector2.Zero, size);
        }

        public virtual void DrawColors(Color[] colors, Vector2 size, Vector2 location)
        {
            if (colors == null) throw new ArgumentNullException("colors");

            Color[] copiedColors = new Color[colors.Length];
            colors.CopyTo(copiedColors, 0);
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawColors, Vectors = new Vector2[] { size, location }, Addtions = new object[] { copiedColors } });
        }
        #endregion

        #region DrawTexture
        public virtual void DrawTexture(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            Vector2 position = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            this.DrawTexture(texture, position, destinationRectangle, null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public virtual void DrawTexture(Texture2D texture, Vector2 position, Color color)
        {
            this.DrawTexture(texture, position, null, null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public virtual void DrawTexture(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            this.DrawTexture(texture, destinationRectangle, sourceRectangle, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        public virtual void DrawTexture(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Vector2 position = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            this.DrawTexture(texture, position, destinationRectangle, sourceRectangle, color, rotation, origin, Vector2.One, effects, layerDepth);
        }

        public virtual void DrawTexture(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            this.DrawTexture(texture, position, sourceRectangle, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public virtual void DrawTexture(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            this.DrawTexture(texture, position, null, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), effects, layerDepth);
        }

        protected virtual void DrawTexture(Texture2D texture, Vector2 position, Rectangle? destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawTexture, Vectors = new Vector2[] { position, origin, scale }, Color = color, Addtions = new object[] { texture, destinationRectangle, sourceRectangle, rotation, effects, layerDepth } });
        }
        #endregion

        #region DrawText
        public virtual void DrawText(SpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            Vector2 one = Vector2.One;
            DrawText(spriteFont, text, position, color, 0f, Vector2.Zero, one, SpriteEffects.None, 0f);
        }

        public virtual void DrawText(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Vector2 vector = new Vector2();
            vector.X = scale;
            vector.Y = scale;
            DrawText(spriteFont, text, position, color, rotation, origin, vector, effects, layerDepth);
        }

        public virtual void DrawText(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            if (spriteFont == null)
            {
                throw new ArgumentNullException("spriteFont");
            }
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            this._commands.Enqueue(new ContextCommand { CommandType = CommandType.DrawText, Vectors = new Vector2[] { position, origin, scale }, Color = color, Addtions = new object[] { spriteFont, text, rotation, effects, layerDepth } });
        }
        #endregion
        #endregion

        #region Begin & End
        public override void Begin()
        {
            base.Begin();
            this.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.Identity);
        }

        public virtual void Begin(SpriteSortMode sortMode, BlendState blendState)
        {
            this.Begin(sortMode, blendState, null, null, null, null, Matrix.Identity);
        }

        public virtual void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState)
        {
            this.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null, Matrix.Identity);
        }

        public virtual void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
        {
            this.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, Matrix.Identity);
        }

        public virtual void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
        {
            this.spriteSortMode = sortMode;
            this.blendState = blendState;
            this.samplerState = samplerState;
            this.depthStencilState = depthStencilState;
            this.rasterizerState = rasterizerState;
            this.customEffect = effect;
            this.transformMatrix = transformMatrix;
        }


        public override void End()
        {
            if (SortMode == DrawingSortMode.Order)
            {
                RenderInOrder();
            }
            else if (SortMode.HasFlag(DrawingSortMode.Sprite))
            {
                List<ContextCommand> primitiveCommands;
                List<ContextCommand> spriteCommands;
                AnalyseCommands(out primitiveCommands, out spriteCommands);
                RenderSprites(spriteCommands);
                RenderPrimitives(primitiveCommands);
            }
            else
            {
                List<ContextCommand> primitiveCommands;
                List<ContextCommand> spriteCommands;
                AnalyseCommands(out primitiveCommands, out spriteCommands);
                RenderPrimitives(primitiveCommands);
                Flush();
                RenderSprites(spriteCommands);
            }

            base.End();
        }
        #endregion

        #region Render
        protected virtual void RenderSprites(List<ContextCommand> spriteCommands)
        {
            RenderTargetBinding[] renderTagets = null;
            this.Render.Begin(this.SpriteBatch, this.spriteSortMode, this.blendState, this.samplerState, this.depthStencilState, rasterizerState, this.customEffect, this.transformMatrix, out renderTagets);
            foreach (ContextCommand command in spriteCommands)
            {
                switch (command.CommandType)
                {
                    case CommandType.DrawPoints:
                        {
                            RenderPoints(this.SpriteBatch, command.Vectors, (Vector2)command.Addtions[0], command.Color);
                            break;
                        }
                    case CommandType.DrawColors:
                        {
                            if(command.Addtions[0] is VertexPositionColor[])
                            {
                                RenderColors(this.SpriteBatch, (VertexPositionColor[])command.Addtions[0], command.Vectors[0]);
                            }
                            else
                            {
                                RenderColors(this.SpriteBatch, (Color[])command.Addtions[0], command.Vectors[0], command.Vectors[1]);
                            }
                            break;
                        }
                    case CommandType.DrawTexture:
                        {
                            if (command.Addtions[1] != null)
                            {
                                this.SpriteBatch.Draw((Texture2D)command.Addtions[0], (Rectangle)command.Addtions[1], (Rectangle?)command.Addtions[2], command.Color, (float)command.Addtions[3], command.Vectors[1], (SpriteEffects)command.Addtions[4], (float)command.Addtions[5]);
                            }
                            else
                            {
                                this.SpriteBatch.Draw((Texture2D)command.Addtions[0], command.Vectors[0], (Rectangle?)command.Addtions[2], command.Color, (float)command.Addtions[3], command.Vectors[1], command.Vectors[2], (SpriteEffects)command.Addtions[4], (float)command.Addtions[5]);
                            }
                            break;
                        }
                    case CommandType.DrawText:
                        {
                            this.SpriteBatch.DrawString((SpriteFont)command.Addtions[0], (string)command.Addtions[1], command.Vectors[0], command.Color, (float)command.Addtions[2], command.Vectors[1], command.Vectors[2], (SpriteEffects)command.Addtions[3], (float)command.Addtions[4]);
                            break;
                        }
                }
            }
            this.Render.End(this.SpriteBatch, renderTagets);
        }

        protected virtual void RenderPrimitives(List<ContextCommand> primitiveCommands)
        {
            foreach (ContextCommand command in primitiveCommands)
            {
                switch (command.CommandType)
                {
                    case CommandType.DrawLine:
                        {
                            ProcessDrawLine(command.Vectors[0], command.Vectors[1], command.Color);
                            break;
                        }
                    case CommandType.DrawRectangle:
                        {
                            ProcessDrawRectangle(command.Vectors[0], command.Vectors[1], command.Color);
                            break;
                        }
                    case CommandType.DrawTriangle:
                        {
                            base.DrawTriangle(command.Vectors[0], command.Vectors[1], command.Vectors[2], command.Color);
                            break;
                        }
                    case CommandType.DrawEllipse:
                        {
                            ProcessDrawEllipse(command.Vectors[0], command.Vectors[1], command.Color);
                            break;
                        }
                    case CommandType.DrawPolyline:
                        {
                            ProcessDrawPolyline(command.Vectors, 0, -1, (bool)command.Addtions[0], command.Color);
                            break;
                        }
                    case CommandType.DrawFilledRectangle:
                        {
                            ProcessDrawFilledRectangle(command.Vectors[0], command.Vectors[1], command.Color);
                            break;
                        }
                    case CommandType.DrawFilledTriangle:
                        {
                            ProcessDrawFilledTriangle(command.Vectors[0], command.Vectors[1], command.Vectors[2], command.Color);
                            break;
                        }
                    case CommandType.DrawFilledEllipse:
                        {
                            ProcessDrawFilledEllipse(command.Vectors[0], command.Vectors[1], command.Color);
                            break;
                        }
                }
            }
        }

        protected virtual void AnalyseCommands(out List<ContextCommand> primitiveCommands, out List<ContextCommand> spriteCommands)
        {
            primitiveCommands = new List<ContextCommand>();
            spriteCommands = new List<ContextCommand>();
            ContextCommand command;
            while (_commands.TryDequeue(out command))
            {
                switch (command.CommandType)
                {
                    case CommandType.DrawLine:
                    case CommandType.DrawRectangle:
                    case CommandType.DrawTriangle:
                    case CommandType.DrawEllipse:
                    case CommandType.DrawPolyline:
                    case CommandType.DrawFilledRectangle:
                    case CommandType.DrawFilledTriangle:
                    case CommandType.DrawFilledEllipse:
                        {
                            primitiveCommands.Add(command);
                            break;
                        }

                    case CommandType.DrawPoints:
                    case CommandType.DrawColors:
                    case CommandType.DrawTexture:
                    case CommandType.DrawText:
                        {
                            spriteCommands.Add(command);
                            break;
                        }
                }
            }
        }

        protected virtual void RenderInOrder()
        {
            bool latestPrimitives = false;
            bool latestSprite = false;
            RenderTargetBinding[] renderTagets = null;
            ContextCommand command;
            while (_commands.TryDequeue(out command))
            {
                switch (command.CommandType)
                {
                    #region Primitives
                    case CommandType.DrawLine:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawLine(command.Vectors[0], command.Vectors[1], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawRectangle:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawRectangle(command.Vectors[0], command.Vectors[1], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawTriangle:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawTriangle(command.Vectors[0], command.Vectors[1], command.Vectors[2], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawEllipse:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawEllipse(command.Vectors[0], command.Vectors[1], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawPolyline:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawPolyline(command.Vectors, 0, -1, (bool)command.Addtions[0], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawFilledRectangle:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawFilledRectangle(command.Vectors[0], command.Vectors[1], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawFilledTriangle:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawFilledTriangle(command.Vectors[0], command.Vectors[1], command.Vectors[2], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    case CommandType.DrawFilledEllipse:
                        {
                            if (latestSprite)
                            {
                                this.Render.End(this.SpriteBatch, renderTagets);
                            }
                            ProcessDrawFilledEllipse(command.Vectors[0], command.Vectors[1], command.Color);
                            latestPrimitives = true;
                            latestSprite = false;
                            break;
                        }
                    #endregion
                    #region Sprites
                    case CommandType.DrawPoints:
                        {
                            if (latestPrimitives)
                            {
                                Flush();
                            }
                            if (!latestSprite)
                            {
                                this.Render.Begin(this.SpriteBatch, this.spriteSortMode, this.blendState, this.samplerState, this.depthStencilState, rasterizerState, this.customEffect, this.transformMatrix, out renderTagets);
                            }
                            RenderPoints(this.SpriteBatch, command.Vectors, (Vector2)command.Addtions[0], command.Color);
                            latestSprite = true;
                            latestPrimitives = false;
                            break;
                        }
                    case CommandType.DrawColors:
                        {
                            if (latestPrimitives)
                            {
                                Flush();
                            }
                            if (!latestSprite)
                            {
                                this.Render.Begin(this.SpriteBatch, this.spriteSortMode, this.blendState, this.samplerState, this.depthStencilState, rasterizerState, this.customEffect, this.transformMatrix, out renderTagets);
                            }
                            if (command.Addtions[0] is VertexPositionColor[])
                            {
                                RenderColors(this.SpriteBatch, (VertexPositionColor[])command.Addtions[0], command.Vectors[0]);
                            }
                            else
                            {
                                RenderColors(this.SpriteBatch, (Color[])command.Addtions[0], command.Vectors[0], command.Vectors[1]);
                            }
                            latestSprite = true;
                            latestPrimitives = false;
                            break;
                        }
                    case CommandType.DrawTexture:
                        {
                            if (latestPrimitives)
                            {
                                Flush();
                            }
                            if (!latestSprite)
                            {
                                this.Render.Begin(this.SpriteBatch, this.spriteSortMode, this.blendState, this.samplerState, this.depthStencilState, rasterizerState, this.customEffect, this.transformMatrix, out renderTagets);
                            }
                            if (command.Addtions[1] != null)
                            {
                                this.SpriteBatch.Draw((Texture2D)command.Addtions[0], (Rectangle)command.Addtions[1], (Rectangle?)command.Addtions[2], command.Color, (float)command.Addtions[3], command.Vectors[1], (SpriteEffects)command.Addtions[4], (float)command.Addtions[5]);
                            }
                            else
                            {
                                this.SpriteBatch.Draw((Texture2D)command.Addtions[0], command.Vectors[0], (Rectangle?)command.Addtions[2], command.Color, (float)command.Addtions[3], command.Vectors[1], command.Vectors[2], (SpriteEffects)command.Addtions[4], (float)command.Addtions[5]);
                            }
                            latestSprite = true;
                            latestPrimitives = false;
                            break;
                        }
                    case CommandType.DrawText:
                        {
                            if (latestPrimitives)
                            {
                                Flush();
                            }
                            if (!latestSprite)
                            {
                                this.Render.Begin(this.SpriteBatch, this.spriteSortMode, this.blendState, this.samplerState, this.depthStencilState, rasterizerState, this.customEffect, this.transformMatrix, out renderTagets);
                            }
                            this.SpriteBatch.DrawString((SpriteFont)command.Addtions[0], (string)command.Addtions[1], command.Vectors[0], command.Color, (float)command.Addtions[2], command.Vectors[1], command.Vectors[2], (SpriteEffects)command.Addtions[3], (float)command.Addtions[4]);
                            latestSprite = true;
                            latestPrimitives = false;
                            break;
                        }
                    #endregion
                }
            }
            if (latestSprite)
            {
                this.Render.End(this.SpriteBatch, renderTagets);
            }
        }

	
        public virtual Texture2D CreateTexture ()
		{
			PresentationParameters pp = GraphicsDevice.PresentationParameters;
        	int bufferWidth = pp.BackBufferWidth;
        	int bufferHeight = pp.BackBufferHeight;
        	Texture2D texture = new Texture2D(GraphicsDevice, bufferWidth, bufferHeight);
			return texture;
		}
		
        protected virtual void RenderPoints(SpriteBatch spriteBatch, Vector2[] points, Vector2 location, Color color)
        {
            Texture2D texture = CreateTexture();	
			Color[] data = new Color[texture.Width * texture.Height];
			TextureContext textureContext = new TextureContext(data, texture.Width, texture.Height);
			textureContext.DrawPoints(points, location, color);
			texture.SetData<Color>(textureContext.Data);
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }

        protected virtual void RenderColors(SpriteBatch spriteBatch, VertexPositionColor[] colors, Vector2 location)
        {
            Texture2D texture = CreateTexture();			
			Color[] data = new Color[texture.Width * texture.Height];
			TextureContext textureContext = new TextureContext(data, texture.Width, texture.Height);
			textureContext.DrawColors(colors, location);
			texture.SetData<Color>(textureContext.Data);
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }

        protected virtual void RenderColors(SpriteBatch spriteBatch, Color[] colors, Vector2 size, Vector2 location)
        {
            Texture2D texture = CreateTexture();			
			Color[] data = new Color[texture.Width * texture.Height];
			TextureContext textureContext = new TextureContext(data, texture.Width, texture.Height);
			textureContext.DrawColors(colors, size, location);
			texture.SetData<Color>(textureContext.Data);
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
        #endregion
    }
}
