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

        //Properties

        //Constructors
        public Map(int[,,,] mapIntArry, Texture2D spriteSheet)
        {
            map = new SubMap[mapIntArry.GetLength(0), mapIntArry.GetLength(1)];

            for (int i = 0; i < mapIntArry.GetLength(0); i++)
            {
                for (int j = 0; j < mapIntArry.GetLength(1); j++)
                {
                    int[,] subMapIntArry = new int [mapIntArry.GetLength(2), mapIntArry.GetLength(3)];
                    for (int k = 0; k < mapIntArry.GetLength(2); k++)
                    {
                        for (int l = 0; l < mapIntArry.GetLength(3); l++)
                        {
                            subMapIntArry[k, l] = mapIntArry[i, j, k, l];
                        }
                    }
                    map[i, j] = new SubMap(subMapIntArry);

                }
            }            
        }    

        //Methods
    }
}
