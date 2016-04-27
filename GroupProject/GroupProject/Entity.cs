using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Entity
    {
        //Fields
        protected Rectangle rectangle;
        protected Texture2D texture;

        //Properties
        public Rectangle Rectangle { get { return rectangle; } }
        public int Width { get { return rectangle.Width; } }
        public int Height { get { return rectangle.Height; } }
        public int X
        {
            get { return rectangle.X; }
            set { rectangle.X = value; }
        }
        public int Y
        {
            get { return rectangle.Y; }
            set { rectangle.Y = value; }
        }


        //Constructors
        /// <summary>
        /// Creates a Entity from a rectangle
        /// </summary>
        /// <param name="rectangle">Entity's rectangle</param>
        public Entity(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            texture = null;
        }

        /// <summary>
        /// creates a character from rectangle components
        /// </summary>
        /// <param name="x">X coord of the Entity</param>
        /// <param name="y">Y coord of the Entity</param>
        /// <param name="width">the width of the Entity</param>
        /// <param name="height">the height of the Entity</param>
        public Entity(int x, int y, int width, int height)
        {
            this.rectangle = new Rectangle(x, y, width, height);
            texture = null;
        }

        //Methods

        /// <summary>
        /// Sets the texture if it hasent been set before
        /// </summary>
        /// <param name="texture">the Entity's sprite texture</param>
        public void SetTexture(Texture2D texture)
        {
            if (this.texture == null)
                this.texture = texture;
        }

        /// <summary>
        /// Draws the Entity if it has a texture
        /// </summary>
        /// <param name="spriteBatch">the game's spritebatch</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(texture != null)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
