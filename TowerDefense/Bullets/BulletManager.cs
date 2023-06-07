using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefense.Enemies;

namespace TowerDefense.Bullets
{
    public class BulletManager
    {
        /// <summary>
        /// This class is responsible for managing a collection of bullets in a game. It provides methods to update and draw the bullets.
        /// </summary>
        public List<Bullet> bulletList = new();

        /// <summary>
        /// Called to update the state of each bullet in the collection. 
        /// It iterates through the bullets and calls their individual Update methods, passing the elapsed time. 
        /// It also checks for collision with the enemies managed by the EnemyManager. 
        /// If a bullet intersects with an alive enemy, the OnHit method of the bullet is called to handle the collision. 
        /// If the bullet is no longer alive after the collision, it is removed from the collection.
        /// </summary>

        public void Update(float deltaTime, EnemyManager enemyManager)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Update(deltaTime);

                foreach (SlimeEnemy enemy in enemyManager.slimeEnemyList)
                {
                    if (!enemy.IsAlive)
                    {
                        continue;
                    }

                    if (bullet.HitBox.Intersects(enemy.HitBox))
                    {
                        if (bullet.OnHit(enemy))
                        {
                            break;
                        }
                    }
                }

                if (!bullet.IsAlive)
                {
                    // Swap last bullet into current slot
                    bulletList[i] = bulletList[bulletList.Count - 1];
                    bulletList.RemoveAt(bulletList.Count - 1);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}