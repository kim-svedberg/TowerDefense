using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Spline;
using TowerDefense.Particles;

namespace TowerDefense.Enemies
{
    public class EnemyManager
    {
        public List<SlimeEnemy> slimeEnemyList = new();
        ParticleSystem particleSystem;


        public void AddEnemy(SlimeEnemy slime)
        {
            slimeEnemyList.Add(slime);
        }

        public void Update(float deltaTime, SimplePath path, ParticleSystem particleSystem)
        {
            for (int i = 0; i < slimeEnemyList.Count; i++)
            {
                SlimeEnemy enemy = slimeEnemyList[i];

                enemy.Update(deltaTime, path);

                if (!enemy.IsAlive)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        particleSystem.GenerateNewParticle(enemy.Position);
                    }

                    // Swap last enemy into current slot
                    slimeEnemyList[i] = slimeEnemyList[slimeEnemyList.Count - 1];
                    slimeEnemyList.RemoveAt(slimeEnemyList.Count - 1);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (SlimeEnemy slime in slimeEnemyList)
            {
                slime.Draw(spriteBatch);
            }
        }
    }
}

