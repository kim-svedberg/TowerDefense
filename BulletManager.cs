using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    internal class BulletManager
    {
        List<Bullet> bulletList = new List<Bullet>();
        List<SlimeEnemy> slimeEnemyList = new List<SlimeEnemy>();
        //Game1 game1;

        bool hit;

        public void HitTarget(Bullet bullet, SlimeEnemy slimeEnemy)
        {
            if (bullet.HitBox.Intersects(slimeEnemy.HitBox))
            {
                slimeEnemyList.Remove(slimeEnemy);
            }
            //if (slimeEnemyList.Count == 0)
            //{
            //}
        }
    }
}
