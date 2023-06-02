using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefense.Enemies;

namespace TowerDefense.Bulets
{
    public class BulletManager
    {
        public List<Bullet> bulletList = new();

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
                        enemy.health--;
                        bullet.health--;

                        if (!bullet.IsAlive)
                        {
                            break;
                        }
                    }
                }

                bullet.existingTime -= deltaTime;
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