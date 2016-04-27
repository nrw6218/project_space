using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class EnemyManager
    {
        private static EnemyManager instance;
        public static EnemyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyManager();
                }
                return instance;
            }
        }
        private EnemyManager() { }

        //Fields

        //Properties

        //Constructors

        //Methods

        /// <summary>
        /// Creates a new instance of an enemy
        /// </summary>
        public void CreateEnemy(int x, int y, int width, int height, int hp, double speed, int row, int col, Texture2D _texture)
        {
            MapManager.Instance.CurrentMap.GetSubmap(row, col).Enemies.Add(new Enemy(x, y, width, height, hp, speed, _texture));
        }

        public void Update()
        {           
            for(int i  = MapManager.Instance.CurrentSubMap.Enemies.Count -1; i>=0; i--)
            {
                if (MapManager.Instance.CurrentSubMap.Enemies[i].Hp <= 0)
                    MapManager.Instance.CurrentSubMap.Enemies.RemoveAt(i);
                else
                    MapManager.Instance.CurrentSubMap.Enemies[i].Update();
            }
        }

    }
}
