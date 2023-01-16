using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TowerDefense
{
    internal class GameObject
    {
        protected Texture2D tex;
        protected Vector2 pos;
        protected Rectangle hitBox;


        public GameObject(Texture2D tex, Vector2 pos, Rectangle hitBox)
        {
            this.tex = tex;
            this.pos = pos;
            this.hitBox = hitBox;
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex,
                pos,
                Color.White);
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public Texture2D Texture
        {
            get { return tex; }
        }
        public Rectangle HitBox
        {
            get { return hitBox; }
            set { hitBox = value; }
        }
    }
}
