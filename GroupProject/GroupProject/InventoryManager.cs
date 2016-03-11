using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupProject
{
    class InventoryManager
    {
        private static InventoryManager instance;
        private playerInventory playerInventory;

        //Properties
        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InventoryManager();
                }
                return instance;
            }
        }

        public playerInventory PlayerInventory { get { return playerInventory; } }

        //Constructors

        //Methods
        public void CreateInventory()
        {
            playerInventory = new playerInventory();
        }
    }
}
