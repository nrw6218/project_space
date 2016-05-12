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
        private MapManager() { }    
            
        //Fields
        private Map currentMap;

        //Properties
        public Map CurrentMap
        {
            get { return currentMap; }
            set { currentMap = value; }
        }
        public SubMap CurrentSubMap { get { return currentMap.CurrentSubMap; } }

        //Constructors
        
        //Methods

        /// <summary>
        /// Loads new map from file
        /// </summary>
        /// <param name="file">file path</param>
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
                                case "health":
                                    AddItemToEquiptment(
                                        row,
                                        col,
                                        new Item(
                                            TextureManager.Instance.Textures[line],
                                            "Health",
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
        
        /// <summary>
        /// Changes the submap to an adjacent one
        /// </summary>
        /// <param name="dir">Driection the new submap is from the original</param>
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

        /// <summary>
        /// Moves subamp to a specific submap
        /// </summary>
        /// <param name="mapIndex"></param>
        public void MoveSubmap(int mapIndex)
        {
            currentMap.SetCurrentSubmap(mapIndex);
        }

        /// <summary>
        /// Checks to see if player is opening a door
        /// </summary>
        /// <param name="ks">Keyboardsstate</param>
        public void DoorCheck(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Space) && PlayerManager.Instance.PlayerEquipment.KeyCount > 0)
            {
                CurrentSubMap.UnlockDoors();
            }
        }

        /// <summary>
        /// Adds Item to the inventory of a submap
        /// </summary>
        /// <param name="row">Row int the map[,]</param>
        /// <param name="col">col int the map[,]</param>
        /// <param name="i">The item to add</param>
        public void AddItemToInventory(int row, int col, Item i)
        {
            currentMap.GetSubmap(row, col).Inventory.Add(i);
        }

        /// <summary>
        /// Adds item to the equipment of a submap
        /// </summary>
        /// <param name="row">Row int the map[,]</param>
        /// <param name="col">col int the map[,]</param>
        /// <param name="i">The item to add</param>
        public void AddItemToEquiptment(int row, int col, Item i)
        {
            currentMap.GetSubmap(row, col).Equipment.Add(i);
        }
    }
}
