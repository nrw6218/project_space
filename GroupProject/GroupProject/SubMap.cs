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
        //Fields
        private Block[,] subMap;

        //Properties
        public SubMap(int[,] intMap, List<Entity> subMapEntities)
        {
            //make blocks and add them to map
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    subMap[i, j] = new Block(j, i, intMap[i, j]);
                }
            }
        }

        //Constructors

        //Methods
        public void Draw(SpriteBatch spriteBatch, Texture2D spriteSheet)
        {
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    subMap[i, j].Draw(spriteBatch, spriteSheet);
                }
            }
        }
    }
}
