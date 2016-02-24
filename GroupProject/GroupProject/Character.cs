using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Character
    {
        protected Rectangle rectangle;
        protected Texture2D texture;

        public Rectangle Rectangle { get { return rectangle; } }
        public Texture2D Texture { get { return texture; } }
        public int Width { get { return rectangle.Width; } }
        public int Height { get { return rectangle.Height; } }
        public int X { get { return rectangle.X; } }
        public int Y { get { return rectangle.Y; } }

        /// <summary>
        /// Creates a character from a rectangle
        /// </summary>
        /// <param name="rectangle">character's rectangle</param>
        public Character(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            texture = null;
        }

        /// <summary>
        /// creates a character from rectangle components
        /// </summary>
        /// <param name="x">X coord of the character</param>
        /// <param name="y">Y coord of the character</param>
        /// <param name="width">the width of the character</param>
        /// <param name="height">the height of the character</param>
        public Character(int x, int y, int width, int height)
        {
            this.rectangle = new Rectangle(x, y, width, height);
            texture = null;
        }

        /// <summary>
        /// Sets the texture if it hasent been set before
        /// </summary>
        /// <param name="texture">the character's sprite texture</param>
        public void SetTexture(Texture2D texture)
        {
            if (this.texture == null)
                this.texture = texture;
        }

        /// <summary>
        /// Draws the character if it has a texture
        /// </summary>
        /// <param name="spriteBatch">the game's spritebatch</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(texture != null)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
