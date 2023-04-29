using MarioTest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace TowerDefense
{
    internal class BulletManager
    {
        List<Bullet> bulletList = new List<Bullet>();
        List<SlimeEnemy> slimeEnemyList = new List<SlimeEnemy>();

        Bullet bullet;

        bool hit;

        public void HitTarget(SlimeEnemy slimeEnemy)
        {
            if (bullet.HitBox.Intersects(slimeEnemy.HitBox))
            {
                slimeEnemyList.Remove(slimeEnemy);
            }
            //if (slimeEnemyList.Count == 0)
            //{
            //}
        }

        public void CreateBullets(Tower tower, List<SlimeEnemy>slimeEnemyList)
        {
            bullet = new Bullet(AssetManager.bulletTex, tower.Pos, new Rectangle(0, 0, 0, 0), slimeEnemyList);

        }
    }
}
