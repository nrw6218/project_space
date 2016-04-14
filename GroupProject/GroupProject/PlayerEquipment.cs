using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class PlayerEquipment
    {
        //Fields
        private List<Item> currentEquipment;

        //Properties
        public List<Item> CurrentEquipment
        {
            get { return currentEquipment; }
        }

        //Constructor
        public PlayerEquipment()
        {
            currentEquipment = new List<Item>();
        }

        //Methods

        /// <summary>
        /// Draws each piece of equipment to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            int x = 50;
            int y = 50;
            int count = 0;
            
            foreach (Item i in currentEquipment)
            {
                i.Draw(spriteBatch, new Rectangle(x, y, 50, 50), Color.Coral);
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

        /// <summary>
        /// Adds an item to the player's equipment
        /// </summary>
        /// <param name="i">Item to be added</param>
        public void AddToEquipment(Item i)
        {
            currentEquipment.Add(i);
        }
    }
}
