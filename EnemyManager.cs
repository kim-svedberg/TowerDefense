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

        public void AddEnemies(Vector2 pos, Rectangle hitBox)
        {
            for (int i = 0; i < 3; i++)
            {
                slimeEnemyList.Add(new SlimeEnemy(AssetManager.slimeRunTex, pos, hitBox));

            }
        }
    }
}

