using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Door : Block
    {
        //Fields
        private bool locked;
        private Rectangle unlockeRect;
        private static int UNLOCK_RANGE = 10;
      
        //Properties
        public bool Locked { get { return locked; } }

        public Rectangle UnlockRect { get { return unlockeRect; } }

        //Constructor
        public Door(int x, int y, int textureId)
            :base(x, y , textureId)
        {
            locked = true;

            unlockeRect = new Rectangle(
                x * Block.BLOCK_SIZE - UNLOCK_RANGE,
                y * Block.BLOCK_SIZE - UNLOCK_RANGE,
                Block.BLOCK_SIZE + UNLOCK_RANGE * 2,
                Block.BLOCK_SIZE + UNLOCK_RANGE* 2);
        }

        //Methods
        public void Unlock()
        {
            locked = false;
            textureId = Block.DEFAULT_BLOCK;
        }
    }
}
