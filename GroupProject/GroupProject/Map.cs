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

        //Constructors
        public Map(int[,,,] mapIntArry, List<Entity>[,] mapEntities, Texture2D spriteSheet)
        {
            /*EXPLINATION
                the 4d array is treated as a 2d array of 2d arrays
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
                    int[,] subMapIntArry = new int [mapIntArry.GetLength(2), mapIntArry.GetLength(3)];
                    for (int k = 0; k < mapIntArry.GetLength(2); k++)
                    {
                        for (int l = 0; l < mapIntArry.GetLength(3); l++)
                        {
                            subMapIntArry[k, l] = mapIntArry[i, j, k, l];
                    }}
                    map[i, j] = new SubMap(subMapIntArry, mapEntities[i,j]);
                }}            
        }    

        //Methods
        public void SetCurrentSubmap(int index)
        {
            //do
        }

    }
}
