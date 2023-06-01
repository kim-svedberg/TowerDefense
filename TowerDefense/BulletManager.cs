using System.Collections.Generic;

namespace TowerDefense
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

        //public void CreateBullet(Tower tower, List<SlimeEnemy>slimeEnemyList, GameTime gameTime)
        //{
        //    bulletStartPos = tower.Pos;

        //    bullet = new Bullet(AssetManager.bulletTex, bulletStartPos, new Rectangle(0, 0, AssetManager.bulletTex.Width / 6, AssetManager.bulletTex.Height), slimeEnemyList);
        //    bulletList.Add(bullet);

        //    SlimeEnemy? lastSlime = null;
        //    float lastDistSqr = float.MaxValue;

        //    foreach (SlimeEnemy slime in slimeEnemyList)
        //    {
        //        float distSqr = Vector2.DistanceSquared(slime.Pos, bullet.Pos);
        //        if (distSqr <= lastDistSqr)
        //        {
        //            lastSlime = slime;
        //            lastDistSqr = distSqr;
        //        }
        //    }

        //    if (lastSlime != null)
        //    {
        //        bullet.direction = lastSlime.Pos;
        //    }


        //    HitTarget(slimeEnemyList, bullet, gameTime);

        //}
    }
}
