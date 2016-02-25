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
        //Fields
        protected Rectangle rectangle;
        protected Texture2D texture;
        //two digit number in yx form, 25 is the 6th texture in the 3rd row
        protected int textureId;

        //Properties
        public Rectangle Rectangle { get { return rectangle; } }
        public Texture2D Texture { get { return texture; } }

        public int BLOCK_SIZE { get { return 32; } }
        public int SPRITESHEET_DIMENTIONS { get { return 16; } }

        protected bool Collidable;

        //Constructors
        public Block(int x, int y, int textureId)
        {
            this.textureId = textureId;
            rectangle = new Rectangle(x, y, BLOCK_SIZE, BLOCK_SIZE);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spriteSheet)
        {
            spriteBatch.Draw(
                spriteSheet,
                rectangle,
                new Rectangle(
                    (textureId % 10) * BLOCK_SIZE,
                    (textureId / 10) * BLOCK_SIZE,
                    BLOCK_SIZE,
                    BLOCK_SIZE
                    ),
                Color.White);
        }
                
        //Methods
    }
}