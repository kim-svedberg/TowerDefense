using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TowerDefense
{
    public class Bullet : GameObject
    {
        Texture2D tex;
        Rectangle srcRec;

        private int frame = 100;
        private double frameTimer, frameInterval = 100;

        public Vector2 direction;
        float speed = 3;

        public int health;
        public float existingTime = 3f;

        public bool IsAlive => health > 0 && existingTime > 0;

        public Bullet(Texture2D tex, Vector2 pos, Size2 size) : base(pos, size)
        {
            this.tex = tex;
            srcRec = new Rectangle(0, 0, AssetManager.bulletTex.Width / 6, AssetManager.bulletTex.Height);
            health = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                tex,
                Position,
                srcRec,
                Color.White);

            //spriteBatch.DrawRectangle(HitBox, Color.Red, 1);
        }

        public void Update(float deltaTime)
        {
            Animation(deltaTime);

            Position += GetDirection(direction) * speed;
        }

        public Vector2 GetDirection(Vector2 targetPos)
        {
            Vector2 normalizedVector = Vector2.Normalize(targetPos - Position);
            return normalizedVector;
        }

        public void Animation(float deltaTime)
        {
            frameTimer -= MathF.Ceiling(deltaTime * 1000f);
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srcRec.X = (frame % 6) * (AssetManager.bulletTex.Width / 6);
            }
        }
    }
}
