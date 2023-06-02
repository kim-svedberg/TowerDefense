using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using TowerDefense.Enemies;

namespace TowerDefense.Bullets
{
    public class Bullet : GameObject
    {
        TextureRegion2D[] frames;

        private int frame = 0;
        private double frameTimer, frameInterval = 100;

        public Vector2 direction;
        protected float speed = 3;

        public int health;
        protected float existingTime = 3f;

        public bool IsAlive => health > 0 && existingTime > 0;

        public Bullet(TextureRegion2D[] frames, Vector2 pos, Size2 size) : base(pos, size)
        {
            this.frames = frames;
            health = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                frames[frame],
                Position,
                Color.White);

            //spriteBatch.DrawRectangle(HitBox, Color.Red, 1);
        }

        public void Update(float deltaTime)
        {
            existingTime -= deltaTime;

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
                frame = (frame + 1) % frames.Length;
            }
        }

        public virtual bool OnHit(SlimeEnemy enemy)
        {
            enemy.health--;
            
            health--;

            return !IsAlive;
        }
    }
}
