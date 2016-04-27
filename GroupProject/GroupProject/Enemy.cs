using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Enemy : Character
    {
        //Fields

        //Properties
        public bool Hostile { get { return PlayerManager.Instance.Player.Karma < 0; } }

        //Constructors
        public Enemy(Rectangle rectangle, int hp, double speed)
            : base(rectangle, hp, speed)
        {
            previousX = X;
            previousY = Y;
        }

        public Enemy(int x, int y, int width, int height, int hp, double speed, Texture2D _texture)
            : base(x, y, width, height, hp, speed)
        {
            previousX = x;
            previousY = y;
            this.texture = _texture;
        }

        //Methods

        /// <summary>
        /// Updates enemy's postition and AI
        /// </summary>
        public override void Update()
        {
            base.Update();
            Move();
            Attack();
        }

        /// <summary>
        /// Moves the enemy towards the player
        /// </summary>
        private void Move()
        {
            //move the player horizontally in the correct direction
            int adjecent = PlayerManager.Instance.Player.X - this.X;
            int opposite = PlayerManager.Instance.Player.Y - this.Y;
            double hypotenuse = Math.Sqrt(Math.Pow((PlayerManager.Instance.Player.X - this.X), 2) + Math.Pow((PlayerManager.Instance.Player.Y - this.Y), 2));            

            int x = (int)(this.speed * (adjecent/hypotenuse));
            int y = (int)(this.speed * (opposite / hypotenuse));

            MapManager.Instance.CurrentSubMap.CollidingWalls(this, x, y);
        }

        /// <summary>
        /// Enemy tries to Attack the player
        /// </summary>
        private void Attack()
        {
            if (this.Rectangle.Intersects(PlayerManager.Instance.Player.Rectangle))
            {
                PlayerManager.Instance.Player.TakeDamage(1);
            }
        }

    }
}
