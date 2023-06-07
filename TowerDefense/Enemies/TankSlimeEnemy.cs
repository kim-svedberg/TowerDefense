using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Enemies
{
    internal class TankSlimeEnemy : SlimeEnemy
    {
        public override bool IsAlive => base.IsAlive;
        public TankSlimeEnemy(TextureRegion2D tex, Vector2 pos, Size2 size) : base(tex, pos, size)
        {
            health = 5;
            speedFactor = 2f;
            currencyValue = 8;
            colorList = new List<Color>();
        }

        public override int DropCurrency()
        {
            base.DropCurrency();
            return currencyValue;
        }

        public override bool OutsideOfBounds(SimplePath path)
        {
            return base.OutsideOfBounds(path);
        }

        public override List<Color> ParticleColor()
        {
            colorList.Add(new Color(0xff_fc_6c_85));
            colorList.Add(new Color(0xff_fc_8e_ac));
            colorList.Add(new Color(0xff_ff_c1_cc));

            return colorList;
        }

    }
}
