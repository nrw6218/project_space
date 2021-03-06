﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Character : Entity
    {
        //Fields
        protected double speed;
        protected int hp;
        protected int previousX;
        protected int previousY;

        //Properties
        public double Speed { get { return speed; } }
        public int Hp { get { return hp; } set { hp = value; } }

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

        //Constructors
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

        /// <summary>
        /// Updates character position
        /// </summary>
        public virtual void Update()
        {
            previousX = X;
            previousY = Y;
        }
    }
}
