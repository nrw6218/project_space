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
        private List<Block> walls;
        private MapInventory mapInventory;
        private MapEquipment mapEquipment;

        //Properties
        public int MapIndex { get { return mapIndex; } }
        public MapInventory MapInventory { get { return mapInventory; } }
        public MapEquipment MapEquipment { get { return mapEquipment; } }

        //Constructors
        public SubMap(int[,] intMap, int mapIndex)
        {
            this.mapIndex = mapIndex;
            walls = new List<Block>();
            mapInventory = new MapInventory();
            mapEquipment = new MapEquipment();

            //make blocks and add them to map
            subMap = new Block[intMap.GetLength(0), intMap.GetLength(1)];
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    subMap[i, j] = new Block(j, i, intMap[i, j]);
                    if (intMap[i, j] == 00 || intMap[i, j] == 01 || intMap[i, j] == 10 || intMap[i, j] == 30 || intMap[i, j] == 31 || intMap[i, j] == 33)
                        walls.Add(subMap[i, j]);
                    
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

        public List<Block> CollidingWalls()
        {
            List<Block> collindingWalls = new List<Block>();
            foreach(Block w in walls)
            {
                if (PlayerManager.Instance.Player.Rectangle.Intersects(w.Rectangle))
                    collindingWalls.Add(w);
            }
            return collindingWalls;
        }
    }
}
