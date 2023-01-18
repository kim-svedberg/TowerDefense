using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    internal class FlowerPot : GameObject
    {
        public FlowerPot(Texture2D tex, Vector2 pos, Rectangle hitBox) : base(tex, pos, hitBox)
        {
        }

        public void Update()
        {
            hitBox.Location = new Vector2((int)pos.X, (int)pos.Y).ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
