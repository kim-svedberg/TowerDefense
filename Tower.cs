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
        public Color color;
        public Tower(Texture2D tex, Vector2 pos, Rectangle hitBox): base(tex, pos, hitBox) 
        {
            //this.color = color;
        }

        public void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }

        public Vector2 GetDirection(Vector2 targetPos)
        {
            Vector2 normalizedVector = Vector2.Normalize(targetPos - pos);
            return normalizedVector;
        }


    }
}
