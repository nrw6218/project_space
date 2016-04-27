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
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < subMap.GetLength(0); i++)
            {
                for (int j = 0; j < subMap.GetLength(1); j++)
                {
                    subMap[i, j].Draw(spriteBatch);
                }
            }

            foreach(Item i in inventory)
                i.Draw(spriteBatch, i.MapPosition);

            foreach (Item i in equipment)
                i.Draw(spriteBatch, i.MapPosition);

            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
        }

        public void CollidingWalls(Character c, int x, int y)
        {
            List<Block> collindingWalls = new List<Block>();

            c.X += x;

            foreach(Block w in walls)
            {
                if (c.Rectangle.Intersects(w.Rectangle))
                    collindingWalls.Add(w);
            }

            foreach (Block w in collindingWalls)
            {
                //if the character is moving to the left, hitting a block on its right side
                if (c.X < c.PreviousX)
                    c.X = w.Rectangle.X + w.Rectangle.Width;
                //if the character is moving to the right, hitting a block on its left side
                else if (c.X > c.PreviousX)
                    c.X = w.Rectangle.X - c.Rectangle.Width;
            }

            c.Y += y;
            collindingWalls.Clear();

            foreach (Block w in walls)
            {
                if (c.Rectangle.Intersects(w.Rectangle))
                    collindingWalls.Add(w);
            }

            foreach (Block w in collindingWalls)
            {
                //if the character is moving up, hitting a block on its bottom
                if (c.Y < c.PreviousY)
                    c.Y = w.Rectangle.Y + w.Rectangle.Height;
                //if the character is moving down, hitting a block on its top
                else if (c.Y > c.PreviousY)
                    c.Y = w.Rectangle.Y - c.Rectangle.Height;
            }

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
