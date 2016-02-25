using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Player : Character
    {
        //Fields
        private int karma;
        private List<Entity> inventory;

        //Properties
        public int Karma
        {
            get { return karma; }
            set { karma = value; }
        }

        //Constructor
        public Player(Rectangle rectangle, int speed)
            :base(rectangle, speed)
        {
            karma = 0;
            inventory = new List<Entity>();
        }

        public Player(int x, int y, int width, int height, int speed)
            :base(x,y,width,height, speed)
        {
            karma = 0;
            inventory = new List<Entity>();
        }
    }
}
