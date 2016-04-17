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
        //two digit number in yx form, 25 is the 6th texture in the 3rd row
        protected int textureId;

        //Properties
        public Rectangle Rectangle { get { return rectangle; } }

        public static int BLOCK_SIZE { get { return 64; } }
        public static int TEXURE_SIZE { get { return 96; } }
        public static int DEFAULT_BLOCK { get { return 44; } }

        //Constructors
        public Block(int x, int y, int textureId)
        {
            this.textureId = textureId;
            rectangle = new Rectangle(x * BLOCK_SIZE, y * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
        }

        virtual public void Draw(SpriteBatch spriteBatch, Texture2D spriteSheet)
        {
            spriteBatch.Draw(
                spriteSheet,
                rectangle,
                new Rectangle(
                    (textureId % 10) * TEXURE_SIZE,
                    (textureId / 10) * TEXURE_SIZE,
                    TEXURE_SIZE,
                    TEXURE_SIZE
                    ),
                Color.White);
        }
                
        //Methods
    }
}