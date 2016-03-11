using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GroupProject
{
    public class playerInventory
    {
        List<Item> currinventory;

        public List<Item> Currinventory { get { return currinventory; } }

        public playerInventory()
        {
            currinventory = new List<Item>(0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int x = 50;
            int y = 50;
            int count = 0;
            

            foreach(Item a in currinventory)
            {
                a.Draw(spriteBatch, new Rectangle(x, y, 50, 50));
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

        public void addToInventory(Item a)
        {
            currinventory.Add(a);
        }

        
    }
}
