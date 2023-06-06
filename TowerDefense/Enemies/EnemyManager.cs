using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Spline;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TowerDefense.Particles;
using CurrencyManager = TowerDefense.Currencies.CurrencyManager;


namespace TowerDefense.Enemies
{
    public class EnemyManager
    {
        public List<SlimeEnemy> slimeEnemyList = new();
        public List<SlimeEnemy> waveList = new();
        
        ParticleSystem particleSystem;
        CurrencyManager currencyManager;


        float slimeSpawnTimer = 0f;
        float slimeSpawnDelay = 3f;
        float tankSpawnTimer = 0f;
        float tankSpawnDelay = 5f;

        public int enemiesWaveOne;
        public int enemiesWaveTwo;

        bool stopSpawningSlimes = false;
        bool stopSpawningTanks = false;

        int slimesSpawned = 0;
        int tankSpawned = 0;

        public bool slimesWin;


        public void AddEnemy(SlimeEnemy slime)
        {
            slimeEnemyList.Add(slime);
            waveList.Add(slime);
        }

        public void Update(float deltaTime, SimplePath path, ParticleSystem particleSystem, CurrencyManager currencyManager)
        {
            for (int i = 0; i < slimeEnemyList.Count; i++)
            {
                SlimeEnemy enemy = slimeEnemyList[i];

                enemy.Update(deltaTime, path);

                if (!enemy.IsAlive)
                {
                    currencyManager.GainCurrency(enemy.DropCurrency());
                    for (int j = 0; j < 10; j++)
                    {
                        particleSystem.GenerateNewParticle(enemy.Position);
                    }

                    // Swap last enemy into current slot
                    slimeEnemyList[i] = slimeEnemyList[slimeEnemyList.Count - 1];
                    slimeEnemyList.RemoveAt(slimeEnemyList.Count - 1);
                }

                if (enemy.OutsideOfBounds(path))
                {
                    slimesWin = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (SlimeEnemy slime in slimeEnemyList)
            {
                slime.Draw(spriteBatch);

                for (int i = 0; i < slime.health; i++)
                {
                    spriteBatch.Draw(AssetManager.heartTex, new Vector2(slime.Position.X + i * 20, slime.Position.Y - 50), Color.White);
                }

            }

        }

        internal void SpawnFirstWaveEnemies(float deltaTime, SimplePath path)
        {
            int slimesInWave = 10;
            enemiesWaveOne = slimesInWave;
            UpdateSlimeSpawnTimer(deltaTime);
            if (slimeSpawnTimer <= 0 && waveList.Count < enemiesWaveOne)
            {
                SpawnSlimeEnemy(path.GetPos(0), AssetManager.slimeRunTex.Size);
                slimesSpawned++;
                ResetSlimeSpawnTimer();
            }
        }


        public void SpawnSecondWaveEnemies(float deltaTime, SimplePath path)
        {
            int slimesInWave = 10;
            int tanksInWave = 5;
            enemiesWaveTwo = slimesInWave + tanksInWave;

            if (!stopSpawningSlimes)
            {
                UpdateSlimeSpawnTimer(deltaTime);

                if (slimeSpawnTimer <= 0)
                {
                    SpawnSlimeEnemy(path.GetPos(0), AssetManager.slimeRunTex.Size);
                    slimesSpawned++;
                    ResetSlimeSpawnTimer();
                }
                if(slimesSpawned == slimesInWave)
                {
                    stopSpawningSlimes = true;
                }

            }
            if (!stopSpawningTanks)
            {
                UpdateTankSpawnTimer(deltaTime);
                if (tankSpawnTimer <= 0)
                {
                    SpawnTankEnemy(path.GetPos(0), AssetManager.tankSlimeTex.Size);
                    tankSpawned++;
                    ResetTankSpawnTimer();
                }
                if (tankSpawned == tanksInWave)
                {
                    stopSpawningTanks = true;
                }

            }



        }

        private void SpawnSlimeEnemy(Vector2 position, Vector2 size)
        {
            SlimeEnemy slimeEnemy = new SlimeEnemy(AssetManager.slimeRunTex, position, size);
            AddEnemy(slimeEnemy);
        }
        private void SpawnTankEnemy(Vector2 position, Vector2 size)
        {
            TankSlimeEnemy tankEnemy = new(AssetManager.tankSlimeTex, position, size);
            AddEnemy(tankEnemy);
        }
        private void UpdateSlimeSpawnTimer(float deltaTime)
        {
            slimeSpawnTimer -= deltaTime;
        }
        public void UpdateTankSpawnTimer(float deltaTime)
        {
            tankSpawnTimer -= deltaTime;
        }
        private void ResetSlimeSpawnTimer()
        {
            slimeSpawnTimer = slimeSpawnDelay;
        }
        private void ResetTankSpawnTimer()
        {
            tankSpawnTimer = tankSpawnDelay;
        }
        public bool IsFirstWaveComplete()
        {
            return waveList.Count >= enemiesWaveOne && slimeEnemyList.Count == 0;
        }

        public bool IsSecondWaveComplete()
        {
            return waveList.Count == 0 && slimeEnemyList.Count == 0;
        }

        public void ClearWaveList()
        {
            waveList.Clear();
        }

        public void ResetWave()
        {
            slimesSpawned = 0;
            tankSpawned = 0;
        }
    }
}

