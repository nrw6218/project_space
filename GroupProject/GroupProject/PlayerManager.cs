using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GroupProject
{
    class PlayerManager
    {
        private static PlayerManager instance;
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
        private PlayerManager()
        {
            int x = 100;
            int y = 100;
            int width = 64;
            int height = 64;
            int hp = 10;
            double speed = 3;
            player = new Player(x, y, width, height, hp, speed);
            playerInventory = new PlayerInventory();
            playerEquipment = new PlayerEquipment();
        }

        //Fields
        private Player player;
        private PlayerInventory playerInventory;
        private PlayerEquipment playerEquipment;

        //Properties       
        public Player Player { get { return player; } }
        public PlayerInventory PlayerInventory { get { return playerInventory; } }
        public PlayerEquipment PlayerEquipment { get { return playerEquipment; } }

        //Constructors

        //Methods      
    }
}
