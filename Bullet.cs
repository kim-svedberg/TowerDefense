using MarioTest;
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
    internal class Bullet : GameObject
    {
        Rectangle srcRec;

        private int frame = 100;
        private double frameTimer, frameInterval = 100;

        public Bullet(Texture2D tex, Vector2 pos, Rectangle hitBox) : base(tex, pos, hitBox)
        {

            srcRec = new Rectangle(0, 0, AssetManager.bulletTex.Width / 6, AssetManager.bulletTex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex,
                pos,
                srcRec,
                Color.White);
        }
        public void Update(GameTime gameTime)
        {
            Animation(gameTime);
        }

        public void Animation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srcRec.X = (frame % 6) * (AssetManager.bulletTex.Width / 6);
            }
        }
    }
}
