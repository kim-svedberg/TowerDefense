using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Spline;
using System.Collections.Generic;
using TowerDefense.Particles;
using CurrencyManager = TowerDefense.Currencies.CurrencyManager;


namespace TowerDefense.Enemies
{
    /// <summary>
    /// Manages the spawning, updating, and drawing of enemies in the game. 
    /// It keeps track of the enemy lists, spawn timers, wave information, and handles the logic for enemy interactions.
    /// </summary>
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


        /// <summary>
        /// Adds a SlimeEnemy instance to the list of active enemies and the wave list.
        /// </summary>
        public void AddEnemy(SlimeEnemy slime)
        {
            slimeEnemyList.Add(slime);
            waveList.Add(slime);
        }

        /// <summary>
        /// Updates the positions of all active enemies.
        /// Checks if enemies are defeated and handles the currency gain and particle effects.
        /// Removes defeated enemies from the active enemy list.
        /// Checks if any enemies have reached the end of the path, indicating a loss for the player.
        /// </summary>
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
                        particleSystem.GenerateNewParticle(enemy.Position, enemy.ParticleColor());
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

        /// <summary>
        /// Spawns the enemies for the first wave.
        /// Updates the spawn timer and checks if it's time to spawn a new enemy.
        /// Resets the spawn timer when necessary.
        /// </summary>
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

        /// <summary>
        /// Spawns the enemies for the second wave, which includes slimes and slime-tanks.
        ///Updates the spawn timers for both slimes and tanks and checks if it's time to spawn a new enemy.
        ///Stops spawning slimes or tanks when the desired number of each enemy type has been spawned.
        /// </summary>
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
                if (slimesSpawned == slimesInWave)
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

        /// <summary>
        /// Checks if the first wave is complete by verifying if all enemies from the first wave have been defeated and no active enemies remain.
        /// </summary>
        public bool IsFirstWaveComplete()
        {
            return waveList.Count >= enemiesWaveOne && slimeEnemyList.Count == 0;
        }

        /// <summary>
        /// Same as the method above, but for the second wave.
        /// </summary>
        public bool IsSecondWaveComplete()
        {
            return waveList.Count == 0 && slimeEnemyList.Count == 0;
        }

        /// <summary>
        /// Clears the wave list, removing all enemies from the current wave.
        /// </summary>
        public void ClearWaveList()
        {
            waveList.Clear();
        }

        /// <summary>
        /// Resets the counters for the number of slimes and tanks spawned in the waves.
        /// </summary>
        public void ResetWave()
        {
            slimesSpawned = 0;
            tankSpawned = 0;
        }
    }
}

