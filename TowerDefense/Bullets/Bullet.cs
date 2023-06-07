using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using System;
using TowerDefense.Enemies;

namespace TowerDefense.Bullets
{
    public class Bullet : GameObject
    {
        /// <summary>
        /// This class represents the basic bullet fired by towers. 
        /// It is responsible for managing the bullet's movement, animation, and collision behavior. 
        /// It contains methods to update the bullet's state, animate its appearance, and handle interactions when it hits a target. 
        /// The class also maintains properties for the bullet's frames, direction, speed, health, and existence time. 
        /// </summary>

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

        /// <summary>
        /// Calculates and returns the normalized direction vector from the bullet's position to the target position.
        /// The direction vector points towards the target.
        /// </summary>
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

        /// <summary>
        /// Handles the behavior when the bullet hits an enemy.
        ///Decreases the health of both the enemy and the bullet.
        /// Returns a boolean indicating whether the bullet is still alive.
        /// </summary>
        public virtual bool OnHit(SlimeEnemy enemy)
        {
            enemy.health--;

            health--;

            return !IsAlive;
        }
    }
}
