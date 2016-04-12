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
        private int previousX;
        private int previousY;

        //Properties
        public bool Hostile { get { return PlayerManager.Instance.Player.Karma < 0; } }

        //Constructor
        public Enemy(Rectangle rectangle, int hp, double speed)
            :base(rectangle, hp, speed)
        {
            previousX = X;
            previousY = Y;
        }

        public Enemy(int x, int y, int width, int height, int hp, double speed)
            :base(x, y, width, height, hp, speed)
        {
            previousX = x;
            previousY = y;
        }

        //Methods
    }
}
