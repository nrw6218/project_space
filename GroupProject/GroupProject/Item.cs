using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class Item
    {
        //Fields
        private Rectangle invenposition;
        private Rectangle mapposition;
        private Texture2D texture;
        private string description;
        private Entity entity;

        //Properties
        public Rectangle InvenPosition
        {
            get { return invenposition; }
            set { invenposition = value; }
        }
        public Rectangle MapPosition
        {
            get { return mapposition; }
            set { mapposition = value; }
        }

        public string Description
        {
            get { return description; }
        }
        
        //Constructors
        public Item(Texture2D _texture, string _description)
        {
            texture = _texture;
            description = _description;
            entity = new Entity(mapposition);
            entity.SetTexture(texture);
        }
        public Item(Texture2D _texture, string _description, Rectangle _mapposition)
        {
            texture = _texture;
            description = _description;
            entity = new Entity(mapposition);
            entity.SetTexture(texture);
            mapposition = _mapposition;
        }



        //methods

        /// <summary>
        /// Draws the item to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="isInInventory">Whether the item is being drawn to the gameword or the inventory</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle position, Color color, bool isInInventory = false)
        {
            if (isInInventory)
                spriteBatch.Draw(texture, position, color);
            if (!isInInventory)
                spriteBatch.Draw(texture, mapposition, color);
        }


        public void Draw(SpriteBatch spriteBatch, Rectangle position, bool isInInventory = false)
        {
            Draw(spriteBatch, position, Color.White, isInInventory);
        }
    }
}
