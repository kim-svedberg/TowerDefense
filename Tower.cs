using MarioTest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TowerDefense
{
    internal class Tower : GameObject
    {
        public Color color;
        public bool placed;
        BulletManager bulletManager;
        public Tower(Texture2D tex, Vector2 pos, Rectangle hitBox, bool placed, BulletManager bulletManager) : base(tex, pos, hitBox)
        {
            this.placed = placed;
            this.bulletManager = bulletManager;
        }

        public void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }

        internal void CreateBullet(List<SlimeEnemy> slimeEnemyList, GameTime gameTime)
        {
            if (slimeEnemyList.Count > 0 && placed)
            {
                Vector2 bulletStartPos = Pos;
                Bullet bullet = new Bullet(AssetManager.bulletTex, bulletStartPos, new Rectangle(0, 0, AssetManager.bulletTex.Width / 6, AssetManager.bulletTex.Height), slimeEnemyList);
                bulletManager.bulletList.Add(bullet);

                SlimeEnemy? lastSlime = null;
                float lastDistSqr = float.MaxValue;

                foreach (SlimeEnemy slime in slimeEnemyList)
                {
                    float distSqr = Vector2.DistanceSquared(slime.Pos, bullet.Pos);
                    if (distSqr <= lastDistSqr)
                    {
                        lastSlime = slime;
                        lastDistSqr = distSqr;
                    }
                }

                if (lastSlime != null)
                {
                    bullet.direction = lastSlime.Pos;
                }
            }


        }


    }
}
