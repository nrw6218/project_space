using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Door : Entity
    {
        //Fields
        private Texture2D tex;
        private Boolean isLocked;
        private Vector2 position;

        //Properties
        public Boolean IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        //Constructor
        public Door(Rectangle rect, Texture2D tex, Vector2 position)
            :base(rect)
        {
            this.tex = tex;
            this.isLocked = true;
            this.position = position;
        }

        //Methods
        public void Unlock()
        {
            this.tex = null;
            this.isLocked = false;
        }
    }
}
