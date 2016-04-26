using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public enum Direction { Left, Right, Up, Down };

    public class MapManager
    {
        private static MapManager instance;

        private MapManager() { }        

        private Map currentMap;

        public Map CurrentMap { get { return currentMap; } }
        public SubMap CurrentSubMap { get { return currentMap.CurrentSubMap; } }

        public static MapManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapManager();
                }
                return instance;
            }
        }

        //edit to make work
        public void NewMap(string file)
        {
            Stream inStream = File.OpenRead(file+".map");
            BinaryReader input = new BinaryReader(inStream);
            int mapHeight = input.ReadInt32();
            int mapWidth = input.ReadInt32();    
            int subMapHeight = 6;
            int subMapWidth = 12;

            //loads ints from file 
            int[,][,] mapIntArry = new int[mapHeight, mapWidth][,];
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    mapIntArry[i, j] = new int[subMapHeight, subMapWidth];
                    for (int k = 0; k < subMapHeight; k++)
                    {
                        for (int l = 0; l < subMapWidth; l++)
                        {
                            mapIntArry[i, j][k, l] = input.ReadInt32();
            }}}}
            input.Close();
            currentMap = new Map(mapIntArry);

            //loads enities for map
            StreamReader sr = new StreamReader(file + ".txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if(line == "start")
                {
                    string _row = sr.ReadLine();
                    string _col = sr.ReadLine();
                    int row = int.Parse(_row);
                    int col = int.Parse(_col);
            
                    while ((line = sr.ReadLine()) != "end")
                    {
                        string _x = sr.ReadLine();
                        string _y = sr.ReadLine();
                        int x = int.Parse(_x);
                        int y = int.Parse(_y);
                        if (line == "item")
                        {
                            line = sr.ReadLine();
                            switch (line)
                            {
                                case "crate":
                                    AddItemToInventory(
                                        row, 
                                        col, 
                                        new Item(
                                            TextureManager.Instance.Textures[line],
                                            "Crate", 
                                            new Rectangle(x, y, 50, 50)));
                                    break;

                                case "box":
                                    AddItemToInventory(
                                        row,
                                        col,
                                        new Item(
                                            TextureManager.Instance.Textures[line],
                                            "Mysterious Box",
                                            new Rectangle(x, y, 50, 50)));
                                    break;

                                case "artifact":
                                    AddItemToInventory(
                                        row,
                                        col,
                                        new Item(
                                            TextureManager.Instance.Textures[line],
                                            "Advanced Camera",
                                            new Rectangle(x, y, 50, 50)));
                                    break;

                                case "key":
                                    AddItemToEquiptment(
                                        row,
                                        col,
                                        new Item(
                                            TextureManager.Instance.Textures[line],
                                            "Key",
                                            new Rectangle(x, y, 25, 30)));
                                    break;
                            }
                        }
                        else if (line == "enemy")
                        {
                            EnemyManager.Instance.CreateEnemy(x, y, 50, 50, 3, 3, row, col, TextureManager.Instance.Textures[line]);
                        }                       
                    }
                }
            }
            sr.Close();
        }
        
        public void MoveSubmap(Direction dir)
        {
            int row = CurrentSubMap.MapIndex / 10;
            int col = CurrentSubMap.MapIndex % 10;
            switch (dir)
            {
                case Direction.Left:
                    col--;
                    break;

                case Direction.Right:
                    col++;
                    break;

                case Direction.Up:
                    row--;
                    break;

                case Direction.Down:
                    row++;
                    break;
            }
            int mapIndex = row * 10 + col;
            currentMap.SetCurrentSubmap(mapIndex);
        }

        public void MoveSubmap(int mapIndex)
        {
            currentMap.SetCurrentSubmap(mapIndex);
        }

        public void DoorCheck(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Space) && PlayerManager.Instance.PlayerEquipment.KeyCount > 0)
            {
                CurrentSubMap.UnlockDoors();
            }
        }

        public void AddItemToInventory(int row, int col, Item i)
        {
            currentMap.GetSubmap(row, col).Inventory.Add(i);
        }

        public void AddItemToEquiptment(int row, int col, Item i)
        {
            currentMap.GetSubmap(row, col).Equipment.Add(i);
        }
    }
}
