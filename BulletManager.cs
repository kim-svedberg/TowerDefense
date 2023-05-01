using MarioTest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;


namespace TowerDefense
{
    internal class BulletManager
    {
        public List<Bullet> bulletList = new List<Bullet>();
        EnemyManager enemyManager;
        Vector2 bulletStartPos;

        Bullet bullet;

        float bulletExistingTime;


        public bool hit;

        //TODO: 

        public void HitTarget(List<SlimeEnemy> slimeEnemyList, Bullet bullet, GameTime gameTime)
        {

            foreach (SlimeEnemy slimeEnemy in slimeEnemyList)
            {
                if (bullet.HitBox.Intersects(slimeEnemy.HitBox))
                {
                    enemyManager.slimeEnemyList.Remove(slimeEnemy);
                    DestroyBullet(bullet);
                    hit = true;
                }
            }

            if (!hit)
            {
                bulletExistingTime = 3;
                float deltaTime = gameTime.GetElapsedSeconds();
                bulletExistingTime -= deltaTime;

                if (bulletExistingTime <= 0)
                {
                    DestroyBullet(bullet);

                }

            }

        }

        public void CreateBullet(Tower tower, List<SlimeEnemy>slimeEnemyList, GameTime gameTime)
        {
            bulletStartPos = tower.Pos;

            bullet = new Bullet(AssetManager.bulletTex, bulletStartPos, new Rectangle(0, 0, AssetManager.bulletTex.Width, AssetManager.bulletTex.Height), slimeEnemyList);

            SlimeEnemy? lastSlime = null;
            float lastDistSqr = float.MaxValue;

            foreach (SlimeEnemy slime in slimeEnemyList)
            {
                float distSqr = Vector2.DistanceSquared(slime.Pos, bullet.Pos);
                if (distSqr <= lastDistSqr)
                {
                    lastSlime = slime;
                    lastDistSqr = distSqr;
                }
            }

            if (lastSlime != null)
            {
                bullet.direction = lastSlime.Pos;
            }

            bulletList.Add(bullet);

            HitTarget(slimeEnemyList, bullet, gameTime);

        }
        public void DestroyBullet(Bullet bullet)
        {
            bulletList.Remove(bullet);

        }
    }
}
