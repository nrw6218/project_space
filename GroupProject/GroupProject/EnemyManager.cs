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
        //Fields
        private static EnemyManager instance;
        private List<Enemy> enemylist;

        //Properties
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

        public List<Enemy> EnemyList
        {
            get { return enemylist; }
        }

        //Constructor

        //Methods

        /// <summary>
        /// Creates a new instance of an enemy
        /// </summary>
        public void CreateEnemy(int x, int y, int width, int height, int hp, double speed, SubMap _location, Texture2D _texture)
        {
            if (enemylist == null)
                enemylist = new List<Enemy>();

            enemylist.Add(new Enemy(x, y, width, height, hp, speed, _location, _texture));
        }

        public void Update()
        {
            for (int i = 0; i < enemylist.Count; i++)
            {
                if (enemylist[i].Location == MapManager.Instance.CurrentSubMap && enemylist[i].Hp <= 0)
                    enemylist.RemoveAt(i);
                else if (enemylist[i].Location == MapManager.Instance.CurrentSubMap)
                    enemylist[i].Update();
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Enemy a in enemylist)
            {
                if (a.Location == MapManager.Instance.CurrentSubMap)
                {
                    a.Draw(spritebatch);
                }
            }
        }

    }
}
