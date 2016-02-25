using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class MapManeger
    {
        private static MapManeger instance;

        private MapManeger() { }

        private Texture2D spriteSheet;

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
        //take file and map;
    }
}
