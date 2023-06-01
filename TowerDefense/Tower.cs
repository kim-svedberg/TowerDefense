using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TowerDefense
{
    public class Tower : GameObject
    {
        protected Texture2D tex;

        public Color color;
        public bool placed;

        float shootDelay;

        public Texture2D Texture => tex;

        public Tower(Texture2D tex, Vector2 pos, Size2 size) : base(pos, size)
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
                shootDelay = 1f;

                for (int i = 0; i < 1; i++)
                {
                    CreateBullet(bulletManager, enemyManager);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, Position, color);
        }

        internal void CreateBullet(BulletManager bulletManager, EnemyManager enemyManager)
        {
            if (!placed)
            {
                return;
            }
            if (enemyManager.slimeEnemyList.Count <= 0)
            {
                return;
            }

            Vector2 bulletStartPos = Position;
            Bullet bullet = new(AssetManager.bulletTex, bulletStartPos, new Size2(AssetManager.bulletTex.Width / 6, AssetManager.bulletTex.Height));
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
    }
}
