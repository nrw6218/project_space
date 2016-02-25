using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Enemy : Character
    {
        //Fields
        private bool hostile;

        //Properties
        public bool Hostile
        {
            get { return hostile; }
            set { hostile = value; }
        }

        //Constructor
        public Enemy(Rectangle rectangle, int speed)
            :base(rectangle, speed)
        {
            hostile = false;
        }

        public Enemy(int x, int y, int width, int height, int speed)
            :base(x,y,width,height, speed)
        {
            hostile = false;
        }

        //Methods
        public Boolean isHostile(Player p)
        {
            if (p.Karma < 0)
                hostile = true;
            else
                hostile = false;
            return hostile;
        }
    }
}
