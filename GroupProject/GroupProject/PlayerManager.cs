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
            player = new Player(0, 0, 0, 0, 0, 0);
        }
    }
}
