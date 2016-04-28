using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class PlayerAttackManager
    {
        private static Rectangle ATTACK_UP = new Rectangle(0, 0, 30, 100);
        private static Rectangle ATTACK_DOWN = new Rectangle(0, 0, 30, 100);
        private static Rectangle ATTACK_LEFT = new Rectangle(0, 0, 100, 30);
        private static Rectangle ATTACK_RIGHT = new Rectangle(0, 0, 100, 30);

        public void Update()
        {
            ATTACK_UP.X = PlayerManager.Instance.Player.X + 20;
            ATTACK_UP.Y = PlayerManager.Instance.Player.Y - 90;
            ATTACK_DOWN.X = PlayerManager.Instance.Player.X + 20;
            ATTACK_DOWN.Y = PlayerManager.Instance.Player.Y + 55;
            ATTACK_LEFT.X = PlayerManager.Instance.Player.X - 85;
            ATTACK_LEFT.Y = PlayerManager.Instance.Player.Y + 20;
            ATTACK_RIGHT.X = PlayerManager.Instance.Player.X + 50;
            ATTACK_RIGHT.Y = PlayerManager.Instance.Player.Y + 20;
        }

        public void Attack(Direction dir)
        {
            Update();
            Rectangle rect = new Rectangle(); 
            switch (dir)
            {
                case Direction.Up:
                    rect = ATTACK_UP;
                    break;

                case Direction.Down:
                    rect = ATTACK_DOWN;
                    break;

                case Direction.Left:
                    rect = ATTACK_LEFT;
                    break;

                case Direction.Right:
                    rect = ATTACK_RIGHT;
                    break;
            }
            foreach(Enemy e in MapManager.Instance.CurrentSubMap.Enemies)
            {
                if (rect.Intersects(e.Rectangle))
                {
                    e.Hp--;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    spritebatch.Draw(TextureManager.Instance.Textures["weaponUp"], ATTACK_UP, Color.White);
                    break;

                case Direction.Down:
                    spritebatch.Draw(TextureManager.Instance.Textures["weaponDown"], ATTACK_DOWN, Color.White);
                    break;

                case Direction.Left:
                    spritebatch.Draw(TextureManager.Instance.Textures["weaponLeft"], ATTACK_LEFT, Color.White);
                    break;

                case Direction.Right:
                    spritebatch.Draw(TextureManager.Instance.Textures["weaponRight"], ATTACK_RIGHT, Color.White);
                    break;
            }
        }
    }
}
