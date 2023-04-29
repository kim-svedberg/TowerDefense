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
using System.Collections.Generic;

namespace TowerDefense
{
    internal class Bullet : Tower
    {
        Rectangle srcRec;

        private int frame = 100;
        private double frameTimer, frameInterval = 100;

        public Vector2 direction;
        float speed = 1;

        Random rnd = new Random();

        public Bullet(Texture2D tex, Vector2 pos, Rectangle hitBox, List<SlimeEnemy> slimeEnemyList) : base(tex, pos, hitBox)
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
            hitBox.Location = new Vector2(pos.X, pos.Y).ToPoint();
            pos += GetDirection(direction) * speed;
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
