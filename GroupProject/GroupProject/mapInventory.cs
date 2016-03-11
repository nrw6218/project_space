using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
     public class MapInventory
    {
        List<Item> currentInventory;

        public List<Item> CurrentInventory { get { return currentInventory; } }

        public MapInventory()
        {
            currentInventory = new List<Item>(0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Item a in currentInventory)
            {
                a.Draw(spriteBatch, a.MapPosition);
            }
        }

        public void AddToInventory(Item a)
        {
            currentInventory.Add(a);
        }

        public void RemoveFromInventory(Item a)
        {
            currentInventory.Remove(a);
        }

    }
}

