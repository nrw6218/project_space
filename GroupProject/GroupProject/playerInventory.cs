using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GroupProject
{
    public class PlayerInventory
    {
        //Fields
        private List<Item> inventory;

        //Properties
        public List<Item> Inventory { get { return inventory; } }

        //Constructors
        public PlayerInventory()
        {
            inventory = new List<Item>(0);
        }

        //Methods

        /// <summary>
        /// Draws the player's inventory to the screen with it's items 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            int x = 50;
            int y = 50;
            int count = 0;
            

            foreach(Item a in inventory)
            {
                a.Draw(spriteBatch, new Rectangle(x, y, 50, 50),Color.LightGreen, true);
                x += 100;
                count++;
                if (count == 5)
                {
                    count = 0;
                    x = 50;
                    y += 100;
                }
            }
        }        
    }
}
