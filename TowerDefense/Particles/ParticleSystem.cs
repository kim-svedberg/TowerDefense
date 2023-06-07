using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.TextureAtlases;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TowerDefense.Particles
{
    public class ParticleSystem
    {
        /// <summary>
        /// This class represents a system for managing and updating particles in the game. 
        /// It provides methods for generating new particles, updating their state over time, and rendering them on the screen.
        /// </summary>
        private Random random;

        private List<Particle> particles;
        private List<TextureRegion2D> textures;
        public ParticleSystem(List<TextureRegion2D> textures)
        {
            random = new Random();
            particles = new List<Particle>();
            this.textures = textures;
        }

        /// <summary>
        /// Generates a new particle with a random texture, velocity, angle, angular velocity, color, size, and time-to-live (TTL).
        /// Adds the generated particle to the list of particles.
        /// Returns the generated particle.
        /// </summary>
        public Particle GenerateNewParticle(Vector2 position, List<Color> colorList)
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

        /// <summary>
        /// Updates the state of all particles in the particle system.
        /// Decreases the TTL(time-to-live) of each particle.
        /// Removes particles from the list when their TTL reaches zero.
        /// </summary>
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
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
