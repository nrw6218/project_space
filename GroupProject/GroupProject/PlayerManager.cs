using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GroupProject
{
    class PlayerManager
    {
        //Fields
        private static PlayerManager instance;
        private Player player;
        private PlayerInventory playerInventory;
        private PlayerEquipment playerEquipment;

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
        public PlayerInventory PlayerInventory { get { return playerInventory; } }
        public PlayerEquipment PlayerEquipment { get { return playerEquipment; } }

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
            if (player == null)
                player = new Player(x, y, width, height, hp , speed);
        }

        public void CreatePlayerInventory()
        {
            if(playerInventory == null)
                playerInventory = new PlayerInventory();
        }

        public void CreatePlayerEquipment()
        {
            if(playerEquipment == null)
            {
                playerEquipment = new PlayerEquipment();
            }
        }

        
    }
}
