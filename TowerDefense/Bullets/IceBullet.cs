using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Enemies;

namespace TowerDefense.Bullets
{
    internal class IceBullet : Bullet
    {
        public IceBullet(TextureRegion2D[] frames, Vector2 pos, Size2 size) : base(frames, pos, size)
        {
            speed = 5f;
            existingTime = 4f;
        }

        public override bool OnHit(SlimeEnemy enemy)
        {
            enemy.speedFactor = 0.5f;
            return base.OnHit(enemy);
        }



    }
}
