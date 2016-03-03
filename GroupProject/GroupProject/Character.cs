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
        protected double speed;
        protected int hp;

        //Properties
        /// <summary>
        /// How many pixels per frame the character can move
        /// </summary>
        public double Speed { get { return speed; } }

        //Constructor
        public Character(Rectangle rectangle, int hp, double speed)
            :base(rectangle)
        {
            this.texture = null;
            this.hp = hp;
            this.speed = speed;
        }

        public Character(int x, int y, int width, int height, int hp, double speed)
            :base(x,y,width, height)
        {
            this.texture = null;
            this.hp = hp;
            this.speed = speed;
        }

        //Methods
    }
}
