using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TowerDefense
{
    public abstract class GameObject
    {
        public Vector2 Position;
        
        protected Size2 size;

        public GameObject(Vector2 pos, Size2 size)
        {
            this.Position = pos;
            this.size = size;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public RectangleF HitBox
        {
            get { return new RectangleF(Position, size); }
        }
    }
}
