using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Character : Entity
    {
        //Fields
        int speed;

        //Properties
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        //Constructor
        public Character(Rectangle rectangle, int speed)
            :base(rectangle)
        {
            this.texture = null;
            this.speed = speed;
        }

        public Character(int x, int y, int width, int height, int speed)
            :base(x,y,width, height)
        {
            this.texture = null;
            this.speed = speed;
        }

        //Methods
    }
}
