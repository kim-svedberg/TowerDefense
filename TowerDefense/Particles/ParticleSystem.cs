using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Particles
{
    public class ParticleSystem
    {
        private Random random;

        private List<Particle> particles;
        private List<TextureRegion2D> textures;
        List<Color> colorList;

        public ParticleSystem(List<TextureRegion2D> textures)
        {
            random = new Random();
            particles = new List<Particle>();
            this.textures = textures;

            colorList = new List<Color>();
            colorList.Add(new Color(0xff_21_62_27));
            colorList.Add(new Color(0xff_c9_e7_cc));
            colorList.Add(new Color(0xff_50_b4_5b));
        }

        public Particle GenerateNewParticle(Vector2 position)
        {
            TextureRegion2D texture = textures[random.Next(textures.Count)];

            Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            Color color = colorList[random.Next(colorList.Count)];

            float size = (float)random.NextDouble();
            int ttl = 20 + random.Next(40);

            Particle particle = new(texture, position, velocity, angle, angularVelocity, color, new Vector2(size), ttl);

            particles.Add(particle);

            return particle;
        }

        public void Update()
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            //spriteBatch.End();
        }
    }
}
