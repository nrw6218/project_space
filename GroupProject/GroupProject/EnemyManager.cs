using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupProject
{
    class EnemyManager
    {
        //Fields
        private static EnemyManager instance;
        private Enemy enemy;

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

        public Enemy Enemy
        {
            get { return enemy; }
        }

        //Constructor

        //Methods

        /// <summary>
        /// Creates a new instance of an enemy
        /// </summary>
        public void CreateEnemy()
        {
            int x = 100;
            int y = 100;
            int width = 64;
            int height = 64;
            int hp = 10;
            double speed = 2;
            if (enemy == null)
                enemy = new Enemy(x, y, width, height, hp, speed);
        }


    }
}
