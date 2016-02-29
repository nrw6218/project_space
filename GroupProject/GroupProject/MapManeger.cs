using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public enum Direction { Left, Right, Up, Down };

    public class MapManeger
    {
        private static MapManeger instance;

        private MapManeger() { }

        private Texture2D spriteSheet;

        private Map currentMap;

        public Map CurrentMap { get { return currentMap; } }
        public SubMap CurrentSubMap { get { return currentMap.CurrentSubMap; } }

        public static MapManeger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapManeger();
                }
                return instance;
            }
        }

        public void LoadSpritesheet(Texture2D spriteSheet)
        {
            this.spriteSheet = spriteSheet;
        }

        //edit to make work
        public void NewMap(string fileName)
        {
            int[,,,] mapIntArry = default(int[,,,]);
            List<Entity>[,] mapEntities = default(List<Entity>[,]);
            //load int[,,,] and list<entity>[,] from binary file;
            currentMap = new Map(mapIntArry, mapEntities, spriteSheet);

        }
        
        public void MoveSubmap(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    break;

                case Direction.Right:
                    break;

                case Direction.Up:
                    break;

                case Direction.Down:
                    break;
            }
        }
    }
}
