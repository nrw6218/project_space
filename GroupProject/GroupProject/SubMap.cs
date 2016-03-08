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
        private int mapIndex;
        private List<Wall> walls;

        //Properties
        public int MapIndex { get { return mapIndex; } }

        //Constructors
        public SubMap(int[,] intMap, List<Entity> subMapEntities, int mapIndex)
        {
            this.mapIndex = mapIndex;
            walls = new List<Wall>();
            //make blocks and add them to map
            subMap = new Block[intMap.GetLength(0), intMap.GetLength(1)];
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    if(intMap[i, j] == 0)
                    {
                        Wall w = new Wall(j, i, intMap[i, j]);
                        subMap[i, j] = w;
                        walls.Add(w);
                    }
                    subMap[i, j] = new Floor(j, i, intMap[i, j]);
                }
            }
        }
        
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

        public List<Wall> CollidingWalls()
        {
            List<Wall> collindingWalls = new List<Wall>();
            foreach(Wall w in walls)
            {
                if (PlayerManager.Instance.Player.Rectangle.Intersects(w.Rectangle))
                    collindingWalls.Add(w);
            }
            return collindingWalls;
        }
    }
}
