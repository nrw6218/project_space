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
        private int keyCount;

        //Properties
        public List<Item> CurrentEquipment { get { return currentEquipment; } }

        public int KeyCount { get { return keyCount; }}

        //Constructors
        public PlayerEquipment()
        {
            currentEquipment = new List<Item>();
            keyCount = 0;
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
                i.Draw(spriteBatch, new Rectangle(x, y, 50, 50), Color.Coral, true);
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
            if(i.Description == "Key")
            {
                keyCount++;
            }
        }

        /// <summary>
        /// Removes a key from the player's equipment
        /// </summary>
        public void RemoveKey()
        {
            if(keyCount > 0)
            {
                for(int i = 0; i < currentEquipment.Count; i++)
                {
                    if(currentEquipment[i].Description == "Key")
                    {
                        currentEquipment.RemoveAt(i);
                        keyCount--;
                        return;
                    }
                }
            }
        }

        public Boolean RemoveHealth()
        {
            for (int i = 0; i < currentEquipment.Count; i++)
            {
                if (currentEquipment[i].Description == "Health")
                {
                    currentEquipment.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
