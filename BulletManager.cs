using MarioTest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Spline;
using System.Collections.Generic;
using System.Drawing;



namespace TowerDefense
{
    internal class BulletManager
    {
        public List<Bullet> bulletList = new List<Bullet>();
        List<Bullet> bulletsToRemove = new List<Bullet>();
        List<SlimeEnemy> enemiesToRemove = new List<SlimeEnemy>();
        
        CooldownTimer cooldownTimer = new CooldownTimer();
        public bool hit;

        public void Update(GameTime gameTime)
        {
            cooldownTimer.Update(gameTime.ElapsedGameTime.TotalSeconds);

        }

        public void HitTarget(List<SlimeEnemy> slimeEnemyList, GameTime gameTime, float deltaTime)
        {

            foreach (Bullet bullet in bulletList)
            {
                foreach (SlimeEnemy enemy in slimeEnemyList)
                {
                    if (bullet.HitBox.Intersects(enemy.HitBox))
                    {
                        bulletsToRemove.Add(bullet);
                        hit = true;

                        if (cooldownTimer.IsDone())
                        {
                            enemy.health--;
                        }
                        else if(!cooldownTimer.IsDone())
                        {
                            cooldownTimer.ResetAndStart(1.0);
                        }
                    }
                   
                    if(enemy.health <= 0)
                    {
                        enemiesToRemove.Add(enemy);
                        break;

                    }
                }

                if (!hit)
                {
                    bullet.existingTime -= deltaTime;

                    if (bullet.existingTime <= 0)
                    {
                        bulletsToRemove.Add(bullet);
                    }
                }
            }


            foreach (Bullet bullet in bulletsToRemove)
            {
                DestroyBullet(bullet);
                bulletList.Remove(bullet);
            }

            foreach (SlimeEnemy enemy in enemiesToRemove)
            {
                slimeEnemyList.Remove(enemy);
            }

            bulletsToRemove.Clear();
            enemiesToRemove.Clear();

        }
        public void DestroyBullet(Bullet bullet)
        {
            bulletList.Remove(bullet);

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
