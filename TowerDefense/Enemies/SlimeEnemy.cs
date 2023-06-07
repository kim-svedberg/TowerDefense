using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Spline;
using System.Collections.Generic;

namespace TowerDefense.Enemies
{
    public class SlimeEnemy : GameObject
    {
        TextureRegion2D tex;

        public float steps;

        public int health = 3;
        public float damageCooldown = 3f;
        protected int currencyValue = 5;

        public float speedFactor = 1f;
        
        protected List<Color> colorList;



        public virtual bool IsAlive => health > 0;

        public SlimeEnemy(TextureRegion2D tex, Vector2 pos, Size2 size)
            : base(pos, size)
        {
            this.tex = tex;
            colorList = new List<Color>();
        }

        /// <summary>
        /// It increments the steps variable based on the elapsed time and speed, and then updates the enemy's position using the path.GetPos method.
        /// </summary>
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

        /// <summary>
        /// Sets the steps variable of the slime enemy to the beginning of the provided path. 
        /// It is used to reset the enemy's position to the start of the path.
        /// </summary>
        public virtual void SlimePosForPath(SimplePath path)
        {
            steps = path.beginT;
        }

        /// <summary>
        /// Determines the amount of currency that the enemy drops when defeated. 
        /// It returns the currency value associated with the enemy. 
        /// It is marked as virtual, allowing derived classes to override it and provide different currency values.
        /// </summary>
        public virtual int DropCurrency()
        {
            return currencyValue;
        }

        /// <summary>
        /// Checks if the slime enemy has moved outside the bounds of the provided path. 
        /// It compares the current steps value with the end of the path plus an offset. 
        /// If the steps value is greater than or equal to the end of the path plus the offset, 
        /// it returns true to indicate that the enemy is outside the bounds.
        /// </summary>
        public virtual bool OutsideOfBounds(SimplePath path)
        {
            if (steps >= path.endT + 50)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list of colors used for particles when the slime is destroyed. 
        /// Marking it virtual allows for other enemy-types to have differently colored particles when 
        /// they're destroyed.
        /// </summary>
        public virtual List<Color> ParticleColor()
        {
            colorList.Add(new Color(0xff_21_62_27));
            colorList.Add(new Color(0xff_c9_e7_cc));
            colorList.Add(new Color(0xff_50_b4_5b));

            return colorList;
        }

    }
}
