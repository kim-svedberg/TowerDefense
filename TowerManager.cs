using System.Collections.Generic;

namespace TowerDefense
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
    }
}
