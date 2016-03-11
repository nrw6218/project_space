using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
     public class mapInventory
    {
        List<Item> currinventory;

        public List<Item> Currinventory { get { return currinventory; } }

        public mapInventory()
        {
            currinventory = new List<Item>(0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Item a in currinventory)
            {
                a.Draw(spriteBatch, a.MapPosition);
            }
        }

        public void addToInventory(Item a)
        {
            currinventory.Add(a);
        }

        public void removeFromInventory(Item a)
        {
            currinventory.Remove(a);
        }

    }
}

