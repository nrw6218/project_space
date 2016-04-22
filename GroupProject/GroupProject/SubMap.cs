using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class SubMap
    {
        //Fields
        private Block[,] subMap;
        private int mapIndex;
        private List<Block> walls;
        private List<Door> doors;
        private List<Item> inventory;
        private List<Item> equipment;
        private List<Enemy> enemies;

        //Properties
        public int MapIndex { get { return mapIndex; } }
        public List<Item> Inventory { get { return inventory; } }
        public List<Item> Equipment { get { return equipment; } }
        public List<Enemy> Enemies { get { return enemies; } }

        //Constructors
        public SubMap(int[,] intMap, int mapIndex)
        {
            this.mapIndex = mapIndex;
            walls = new List<Block>();
            doors = new List<Door>();

            inventory = new List<Item>();
            equipment = new List<Item>();

            enemies = new List<Enemy>();        

            //make blocks and add them to map
            subMap = new Block[intMap.GetLength(0), intMap.GetLength(1)];
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    if (intMap[i, j] == 30)
                    {
                        subMap[i, j] = new Door(j, i, intMap[i, j]);
                        walls.Add(subMap[i, j]);
                        doors.Add((Door)subMap[i, j]);
                    }
                    else
                    {
                        subMap[i, j] = new Block(j, i, intMap[i, j]);

                        if (intMap[i, j] == 00 || intMap[i, j] == 01 || intMap[i, j] == 10 || intMap[i, j] == 33)
                            walls.Add(subMap[i, j]);
                    }
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

            foreach(Item i in inventory)
                i.Draw(spriteBatch, i.MapPosition);

            foreach (Item i in equipment)
                i.Draw(spriteBatch, i.MapPosition);

            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
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

        //only access through mapManager
        public void UnlockDoors()
        {
            foreach (Door d in doors)
            {
                if (d.Locked && PlayerManager.Instance.Player.Rectangle.Intersects(d.UnlockRect))
                {
                    Console.WriteLine("door");
                    d.Unlock();
                    PlayerManager.Instance.PlayerEquipment.RemoveKey();
                    walls.Remove(d);
                }
            }
        }

    }
}
