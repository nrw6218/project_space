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
        private int previousX;
        private int previousY;

        public enum state { Attacking, PickingItem };

        public int PreviousX
        {
            get { return previousX; }
            set { previousX = value; }
        }
        public int PreviousY
        {
            get { return previousY; }
            set { previousY = value; }
        }

        //Properties
        public int Karma
        {
            get { return karma; }
            set { karma = value; }
        }

        //Constructor
        public Player(Rectangle rectangle, int hp, double speed)
            :base(rectangle, hp, speed)
        {
            karma = 0;
            previousX = X;
            previousY = Y;
        }
        
        public Player(int x, int y, int width, int height, int hp, double speed)
            :base(x, y, width, height, hp, speed)
        {
            karma = 0;
            previousX = X;
            previousY = Y;
        }

        //Methods
        public void TakeDamage(int dmg)
        {
            hp -= dmg;

        }
    }
}
