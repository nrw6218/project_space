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
        protected int textureId;

        //Properties
        public Rectangle Rectangle { get { return rectangle; } }
        public Texture2D Texture { get { return texture; } }

        public int BLOCK_SIZE { get { return 32; } }

        protected bool Collidable;

        //Constructors
        public Block(int x, int y, int textureId)
        {
            this.textureId = textureId;
            rectangle = new Rectangle(x, y, BLOCK_SIZE, BLOCK_SIZE);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //do the draw
        }

        public void SetTexture(Texture2D spriteSheet)
        {
            //set texture from textureId int a spritesheet
        }
                
        //Methods
    }
}