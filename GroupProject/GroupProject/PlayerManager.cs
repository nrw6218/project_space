using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupProject
{
    class PlayerManager
    {
        //Fields
        private static PlayerManager instance;
        private Player player;

        //Properties
        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        public Player Player { get { return player; } }

        //Constructors

        //Methods
        public void CreatePlayer()
        {
            int x = 100;
            int y = 100;
            int width = 64;
            int height = 64;
            int hp = 10;
            double speed = 2;
            player = new Player(x, y, width, height, hp , speed);
        }
    }
}
