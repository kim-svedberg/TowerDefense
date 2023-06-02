﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;

namespace TowerDefense.Particles
{
    public class Particle
    {
        public TextureRegion2D Texture { get; }        // The texture that will be drawn to represent the particle
        public Vector2 Position;        // The current position of the particle        
        public Vector2 Velocity;        // The speed of the particle at the current instance
        public float Angle;            // The current angle of rotation of the particle
        public float AngularVelocity;    // The speed that the angle is changing
        public Color Color;            // The color of the particle
        public Vector2 Size;                // The size of the particle
        public int TTL;                // The 'time to live' of the particle

        public Particle(TextureRegion2D texture, Vector2 position, Vector2 velocity, 
            float angle, float angularVelocity, Color color, Vector2 size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, Color,
                Angle, origin, Size, SpriteEffects.None, 0f);

        }
    }
}
