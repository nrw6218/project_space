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
        private String description;
        private bool isInInventory;
        private bool isInEquipment;
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

        public String Description
        {
            get { return description; }
        }
        
        //Constructors
        public Item(Texture2D _texture, String _description)
        {
            texture = _texture;
            description = _description;
            entity = new Entity(mapposition);
            entity.SetTexture(texture);
            isInInventory = true;
            isInEquipment = true;
        }
        public Item(Texture2D _texture, String _description, Rectangle _mapposition)
        {
            texture = _texture;
            description = _description;
            entity = new Entity(mapposition);
            entity.SetTexture(texture);
            mapposition = _mapposition;
            isInInventory = false;
            isInEquipment = false;
        }



        //methods

        //Overloads other version of method by allowing the user to choose a color
        public void Draw(SpriteBatch spriteBatch, Rectangle position, Color color)
        {
            if (isInInventory)
                spriteBatch.Draw(texture, position, color);
            if (!isInInventory)
                spriteBatch.Draw(texture, mapposition, color);
        }


        public void Draw(SpriteBatch spriteBatch, Rectangle position)
        {
            if (isInInventory)
                spriteBatch.Draw(texture, position, Color.White);
            if (!isInInventory)
                spriteBatch.Draw(texture, mapposition, Color.White);
        }
    
        public void AddToPlayerInventory()
        {
            PlayerManager.Instance.PlayerInventory.AddToInventory(this);
            isInInventory = true;
        }

        public void AddToPlayerEquipment()
        {
            PlayerManager.Instance.PlayerEquipment.AddToEquipment(this);
            isInEquipment = true;
        }
    }
}
