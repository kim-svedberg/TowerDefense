using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using TowerDefense.Bullets;
using TowerDefense.Bullets;
using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class Tower : GameObject
    {
        protected TextureRegion2D tex;

        public Color color;
        public bool placed;

        protected float shootDelay;
        protected float shootDelayTime = 1f;

        public TextureRegion2D Texture => tex;

        public Tower(TextureRegion2D tex, Vector2 pos, Size2 size) : base(pos, size)
        {
            this.tex = tex;
        }

        public virtual void Update(
            float deltaTime,
            BulletManager bulletManager,
            EnemyManager enemyManager)
        {
            shootDelay -= deltaTime;
            if (shootDelay <= 0)
            {
                shootDelay = shootDelayTime;

                for (int i = 0; i < 1; i++)
                {
                    TryAttack(bulletManager, enemyManager);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, Position, color);
        }

        protected virtual void TryAttack(BulletManager bulletManager, EnemyManager enemyManager)
        {
            if (!placed)
            {
                return;
            }
            if (enemyManager.slimeEnemyList.Count <= 0)
            {
                return;
            }

            Bullet bullet = CreateBullet(Position);
            bulletManager.bulletList.Add(bullet);

            SlimeEnemy lastSlime = null;
            float lastDistSqr = float.MaxValue;

            foreach (SlimeEnemy slime in enemyManager.slimeEnemyList)
            {
                float distSqr = Vector2.DistanceSquared(slime.Position, bullet.Position);
                if (distSqr <= lastDistSqr)
                {
                    lastSlime = slime;
                    lastDistSqr = distSqr;
                }
            }

            if (lastSlime != null)
            {
                bullet.direction = lastSlime.Position;
            }
        }

        protected virtual Bullet CreateBullet(Vector2 startPos)
        {
            Bullet bullet = new(AssetManager.bulletTex, startPos, AssetManager.bulletTex[0].Size);
            return bullet;
        }
    }
}
