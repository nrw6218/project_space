using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Attack
    {
        //Added a really basic and unituitive way to attack. 
        //Will probably evenually need its own manager and would also be able 
        //to be used for enemy attacks if a manager was made.


        private Rectangle mapposition;
        private Texture2D texture;
        private Entity entity;

        public Rectangle MapPosition
        {
            get { return mapposition; }
        }


        public Attack(Rectangle m, Texture2D t)
        {
            entity = new Entity(mapposition);
            entity.SetTexture(texture);
            mapposition = m;
            texture = t;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, mapposition, Color.White);
        }


    }
}
