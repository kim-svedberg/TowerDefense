using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace TowerDefense
{
    internal class Tower : GameObject
    {
        public Tower(Texture2D tex, Vector2 pos, Rectangle hitBox): base(tex, pos, hitBox) 
        { }

        public void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

        public void PlantingFlower(FlowerPot pot)
        {
            if (hitBox.Intersects(pot.HitBox))
            {
                pos = pot.Pos;
            }
        }
    }
}
