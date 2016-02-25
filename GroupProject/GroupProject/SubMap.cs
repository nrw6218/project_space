using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class SubMap
    {
        private Block[,] subMap;

        public SubMap(int[,] intMap)
        {
            //make blocks and add them to map
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    subMap[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
