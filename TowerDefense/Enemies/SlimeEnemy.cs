using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Spline;

namespace TowerDefense.Enemies
{
    public class SlimeEnemy : GameObject
    {
        TextureRegion2D tex;

        public float steps;

        public int health = 3;
        public float damageCooldown = 3f;
        protected int currencyValue = 10;

        public float speedFactor = 1f;


        public virtual bool IsAlive => health > 0;

        public SlimeEnemy(TextureRegion2D tex, Vector2 pos, Size2 size)
            : base(pos, size)
        {
            this.tex = tex;
        }

        public virtual void Update(float deltaTime, SimplePath path)
        {
            steps += deltaTime * 50 * speedFactor;

            Position = path.GetPos(steps);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, Position, Color.White);

            //spriteBatch.DrawRectangle(HitBox, Color.Red, 1);
            //spriteBatch.DrawRectangle(srcRec, Color.AliceBlue, 1);
        }

        public virtual void SlimePosForPath(SimplePath path)
        {
            steps = path.beginT;
        }

        public virtual int DropCurrency()
        {
            return currencyValue;
        }

        public virtual bool OutsideOfBounds(SimplePath path)
        {
            if (steps >= path.endT + 50)
            {
                return true;
            }
            return false;
        }

    }
}
