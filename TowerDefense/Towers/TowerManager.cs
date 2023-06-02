using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefense.Bulets;
using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class TowerManager
    {
        public List<Tower> towerList = new();

        public void AddTower(Tower tower)
        {
            towerList.Add(tower);
        }

        public void Update(
            float deltaTime,
            BulletManager bulletManager,
            EnemyManager enemyManager)
        {
            foreach (Tower tower in towerList)
            {
                tower.Update(deltaTime, bulletManager, enemyManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Tower towerToPlace)
        {
            foreach (Tower tower in towerList)
            {
                tower.Draw(spriteBatch);

            }

            towerToPlace?.Draw(spriteBatch);

        }
    }
}
