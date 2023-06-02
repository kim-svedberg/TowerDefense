using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Spline;

namespace TowerDefense.Enemies
{
    public class SlimeEnemy : GameObject
    {
        Texture2D tex;
        Rectangle srcRec;
        SpriteEffects spriteEffects;

        public float steps;

        private int frame = 100;
        private double frameTimer, frameInterval = 100;

        public int health = 3;
        public float damageCooldown = 3f;

        public float speedFactor = 1f;

        public bool IsAlive => health > 0;

        public SlimeEnemy(Texture2D tex, Vector2 pos, Size2 size)
            : base(pos, size)
        {
            this.tex = tex;
            srcRec = new Rectangle(0, 0, AssetManager.slimeRunTex.Width / 4, AssetManager.slimeRunTex.Height);
        }

        public void Update(float deltaTime, SimplePath path)
        {
            steps += deltaTime * 50 * speedFactor;

            Position = path.GetPos(steps);

            //Animation(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (floatPosition < path.endT)
            {
                spriteBatch.Draw(
                    tex,
                    Position,
                    srcRec,
                    Color.White,
                    0,
                    new Vector2(35, 50),
                    1,
                    spriteEffects,
                    0);
            }

            //spriteBatch.DrawRectangle(HitBox, Color.Red, 1);
            //spriteBatch.DrawRectangle(srcRec, Color.AliceBlue, 1);
        }

        public void SlimePosForPath(SimplePath path)
        {
            steps = path.beginT;
        }

        public void Animation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srcRec.X = frame % 4 * (AssetManager.slimeRunTex.Width / 4);
            }
        }
    }
}
