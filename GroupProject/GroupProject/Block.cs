using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Block
    {
        //Fields
        protected Rectangle rectangle;
        //two digit number in yx form, 25 is the 6th texture in the 3rd row
        protected int textureId;
        protected int x;
        protected int y;
        protected bool isDoor;

        //Properties
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public bool IsDoor { get { return isDoor; } }

        public Rectangle Rectangle { get { return rectangle; } }

        public static int BLOCK_SIZE { get { return 64; } }
        public static int TEXURE_SIZE { get { return 96; } }
        public static int DEFAULT_BLOCK { get { return 44; } }

        //Constructors
        public Block(int x, int y, int textureId)
        {
            this.x = x;
            this.y = y;
            this.textureId = textureId;
            isDoor = false;
            rectangle = new Rectangle(x * BLOCK_SIZE, y * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
        }

        //Methods

        /// <summary>
        /// Draws the block
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="spriteSheet"></param>
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TextureManager.Instance.Textures["tilesheet"],
                rectangle,
                new Rectangle(
                    (textureId % 10) * TEXURE_SIZE,
                    (textureId / 10) * TEXURE_SIZE,
                    TEXURE_SIZE,
                    TEXURE_SIZE
                    ),
                Color.White);
        }
    }
}