using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Bullets;
using TowerDefense.Bullets;
using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class IceTower : Tower
    {

        public IceTower(TextureRegion2D tex, Vector2 pos, Size2 size) : base(tex, pos, size)
        {
            shootDelayTime = 2f;
        }

        protected override Bullet CreateBullet(Vector2 startPos)
        {
            IceBullet bullet = new(AssetManager.iceBulletTex, startPos, AssetManager.iceBulletTex[0].Size);
            return bullet;
        }
    }
}
