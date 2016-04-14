using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class MapEquipment
    {
        //Fields
        private List<Item> currentEquipment;

        //Properties
        public List<Item> CurrentEquipment
        {
            get { return currentEquipment; }
        }

        public int Count
        {
            get { return currentEquipment.Count; }
        }

        //Constructor
        public MapEquipment()
        {
            currentEquipment = new List<Item>();
        }

        //Methods

        /// <summary>
        /// Draws each piece of equipment to the screen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch objects</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Item i in currentEquipment)
            {
                i.Draw(spriteBatch, i.MapPosition);
            }
        }

        /// <summary>
        /// Adds an item to the map's equipment
        /// </summary>
        /// <param name="i">Item to add</param>
        public void AddToEquipment(Item i)
        {
            currentEquipment.Add(i);
        }

        /// <summary>
        /// Removes an item from the map's equipment (to be used when the player picks up)
        /// </summary>
        /// <param name="i">Item to be removed</param>
        public void RemoveFromEquipment(Item i)
        {
            currentEquipment.Remove(i);
        }
    }
}
