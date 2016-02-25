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
        public Enemy(Rectangle rectangle, int hp, double speed)
            :base(rectangle, hp, speed)
        {
            hostile = false;
        }

        public Enemy(int x, int y, int width, int height, int hp, double speed)
            :base(x, y, width, height, hp, speed)
        {
            hostile = false;
        }

        //Methods
        public bool isHostile(Player p)
        {
            hostile = p.Karma < 0;
            return hostile;
        }
    }
}
