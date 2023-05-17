using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using MonoGame.Extended;
using Spline;
using System.IO;
using MarioTest;
using MonoGame.Extended.Timers;
using SharpDX.Direct3D9;

namespace TowerDefense
{
    internal class SlimeEnemy : GameObject
    {
        
        Rectangle srcRec;
        SpriteEffects spriteEffects;

        public float steps;

        private int frame = 100;
        private double frameTimer, frameInterval = 100;

        public int health = 3;
        public float damageCooldown = 3f;


        public SlimeEnemy(Texture2D tex,
            Vector2 pos, 
            Rectangle hitBox) 
            : base(tex, pos, hitBox)
        {
            srcRec = new Rectangle(0, 0, AssetManager.slimeRunTex.Width / 4, AssetManager.slimeRunTex.Height);

        } 

        public void Update(GameTime gameTime, SimplePath path)
        {
            hitBox.Location = new Vector2((int)pos.X, (int)pos.Y).ToPoint();

            steps++;
            pos = path.GetPos(steps);

            if (steps == 1500)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;

            }

            //Animation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, SimplePath path)
        {
            //if (floatPosition < path.endT)
            {
                spriteBatch.Draw(tex,
                    pos,
                    srcRec,
                    Color.White,
                    0,
                    new Vector2(35, 50),
                    1,
                    spriteEffects,
                    0);
            }

            spriteBatch.DrawRectangle(hitBox, Color.Red, 1);
            //spriteBatch.DrawRectangle(srcRec, Color.AliceBlue, 1);
        }

        public void SlimePosForPath(SimplePath path)
        {
            steps = path.beginT;
        }
        public void Animation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srcRec.X = (frame % 4) * (AssetManager.bulletTex.Width / 4);
            }
        }
    }
}
