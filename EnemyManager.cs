using MarioTest;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    internal class EnemyManager
    {
        public List<SlimeEnemy> slimeEnemyList = new List<SlimeEnemy>();

        public void AddEnemy(SlimeEnemy slime)
        {
            slimeEnemyList.Add(slime);
        }
    }
}

