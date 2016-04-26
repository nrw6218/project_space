using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    public class TextureManager
    {
        private static TextureManager instance;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }

        public Dictionary<string, Texture2D> Textures { get { return textures; } }
    }
}
