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
    /// <summary>
    /// This class derives from the regular Bullet. The ice bullet is fired by ice towers and has some special properties.
    /// It slows the enemy down, making it slower each time it collides with it. 
    /// </summary>
    internal class IceBullet : Bullet
    {
        public IceBullet(TextureRegion2D[] frames, Vector2 pos, Size2 size) : base(frames, pos, size)
        {
            speed = 5f;
            existingTime = 4f;
        }

        public override bool OnHit(SlimeEnemy enemy)
        {
            enemy.speedFactor = enemy.speedFactor * 0.5f;
            return base.OnHit(enemy);
        }



    }
}
