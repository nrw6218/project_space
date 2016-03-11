﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
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
        public void NewMap(string fileName)
        {
            Stream inStream = File.OpenRead(fileName);
            BinaryReader input = new BinaryReader(inStream);
            /*int mapWidth = input.ReadInt32();
            int mapHeight = input.ReadInt32();
            int subMapWidth = input.ReadInt32();
            int subMapHeight = input.ReadInt32();*/
            int mapWidth = 1;
            int mapHeight = 1;
            int subMapWidth = 12;
            int subMapHeight = 6;

            //loads ints from file 
            int[,][,] mapIntArry = new int[mapHeight, mapWidth][,];
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
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
    }
}