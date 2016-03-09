using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Map
    {
        //Fields
        private SubMap[,] map;

        private SubMap currentSubMap;

        //Properties
        public SubMap CurrentSubMap { get { return currentSubMap; } }
        public int MapColumns { get { return map.GetLength(0); } }
        public int MapRows { get { return map.GetLength(1); } }

        //Constructors
        public Map(int[,][,] mapIntArry)
        {
            /*
                the outer 2d array is an array of submaps
                the inner 2d arrays are arrays of blocks, which are the individual submaps
            */

            map = new SubMap[mapIntArry.GetLength(0), mapIntArry.GetLength(1)];

            for (int i = 0; i < mapIntArry.GetLength(0); i++)
            {
                for (int j = 0; j < mapIntArry.GetLength(1); j++)
                {
                    /*This section makes the submaps and adds them to the map 2d array
                    /*****************************************************************/
                    int[,] subMapIntArry = new int [mapIntArry[i,j].GetLength(0), mapIntArry[i, j].GetLength(1)];
                    for (int k = 0; k < mapIntArry[i, j].GetLength(0); k++)
                    {
                        for (int l = 0; l < mapIntArry[i, j].GetLength(1); l++)
                        {
                            subMapIntArry[k, l] = mapIntArry[i, j][k, l];
                    }}
                    map[i, j] = new SubMap(subMapIntArry, i*10 + j);
            }}

            currentSubMap = map[0, 0];
        }    

        //Methods
        public void SetCurrentSubmap(int mapIndex)
        {
            if ((mapIndex / 10 >= 0 || mapIndex / 10 < MapRows) && (mapIndex % 10 >= 0 || mapIndex % 10 < MapColumns))
                currentSubMap = map[mapIndex / 10, mapIndex % 10];
            else
                throw new IndexOutOfRangeException();
        }

    }
}
