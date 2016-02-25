using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Block
    {
        protected Rectangle rectangle;
        protected Texture2D texture;

        public Rectangle Rectangle { get { return rectangle; } }
        public Texture2D Texture { get { return texture; } }

        public int BLOCK_SIZE { get { return 32; } }

        protected bool Collidable;

        public Block(int textureId)
        {
            //texture = texture int spritesheet
            //rectangle = new Rectangle(x, y, BLOCK_SIZE, BLOCK_SIZE);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //do the draw
        }
    }
}