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
        private SubMap location;
        //Properties
        public bool Hostile { get { return PlayerManager.Instance.Player.Karma < 0; } }

        public SubMap Location
        {
            get { return location; }
        }

        //Constructor
        public Enemy(Rectangle rectangle, int hp, double speed)
            : base(rectangle, hp, speed)
        {
            previousX = X;
            previousY = Y;
        }

        public Enemy(int x, int y, int width, int height, int hp, double speed, SubMap _location, Texture2D _texture)
            : base(x, y, width, height, hp, speed)
        {
            previousX = x;
            previousY = y;
            location = _location;
            this.texture = _texture;
        }

        //Methods
        public void Update()
        {
                this.Move();
                this.Attack();

        }

        private void Move()
        {
            Rectangle playerloc = PlayerManager.Instance.Player.Rectangle;

            //move the player horizontally in the correct direction

            if (playerloc.X > this.X)
                this.X += 1;
            else
                this.X -= 1;

            List<Block> collidingWalls = MapManager.Instance.CurrentSubMap.CollidingWalls();

            foreach (Block w in collidingWalls)
            {
                //if the enemy is moving to the left, hitting a block on its right side
                if (this.X < this.previousX)
                    this.X = w.Rectangle.X + w.Rectangle.Width;
                //if the enemy is moving to the right, hitting a block on its left side
                else if (this.X > this.previousX)
                    this.X = w.Rectangle.X - this.Rectangle.Width;
            }

            //move the enemy vertically in the correct direction
            if (playerloc.Y > this.Y)
                this.Y += 5;
            else
                this.Y -= 5;

            collidingWalls = MapManager.Instance.CurrentSubMap.CollidingWalls();

            foreach (Block w in collidingWalls)
            {
                //if the enemy is moving up, hitting a block on its bottom
                if (this.Y < this.previousY)
                    this.Y = w.Rectangle.Y + w.Rectangle.Height;
                //if the enemy is moving down, hitting a block on its top
                else if (this.Y > this.previousY)
                    this.Y = w.Rectangle.Y - this.Rectangle.Height;
            }
        }

        private void Attack()
        {
            if (this.Rectangle.Intersects(PlayerManager.Instance.Player.Rectangle))
            {
                PlayerManager.Instance.Player.TakeDamage(1);
            }
        }

    }
}
