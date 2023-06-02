﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;

namespace TowerDefense
{
    public static class AssetManager
    {
        public static Texture2D backgroundTex;
        public static Texture2D renderBackGroundTex;
        public static Texture2D slimeRunTex;
        public static TextureRegion2D towerTex;
        public static TextureRegion2D[] bulletTex;
        public static Texture2D potTex;
        public static TextureRegion2D iceTowerTex;
        public static TextureRegion2D[] iceBulletTex;
        public static List<TextureRegion2D> particleTextures;



        public static void LoadTextures(ContentManager content)
        {
            backgroundTex = content.Load<Texture2D>("truebg");
            renderBackGroundTex = content.Load<Texture2D>("truetransparentBG");
            slimeRunTex = content.Load<Texture2D>("Slime_Spiked_Run");
            towerTex = new TextureRegion2D(content.Load<Texture2D>("flower8BIG"));
            var sparkleAtlas = content.Load<Texture2D>("sparklesprite");
            bulletTex = new TextureRegion2D[]
            {
                new(sparkleAtlas, new Rectangle(sparkleAtlas.Width / 6 * 0, 0, sparkleAtlas.Width / 6, sparkleAtlas.Height)),
                new(sparkleAtlas, new Rectangle(sparkleAtlas.Width / 6 * 1, 0, sparkleAtlas.Width / 6, sparkleAtlas.Height)),
                new(sparkleAtlas, new Rectangle(sparkleAtlas.Width / 6 * 2, 0, sparkleAtlas.Width / 6, sparkleAtlas.Height)),
                new(sparkleAtlas, new Rectangle(sparkleAtlas.Width / 6 * 3, 0, sparkleAtlas.Width / 6, sparkleAtlas.Height)),
                new(sparkleAtlas, new Rectangle(sparkleAtlas.Width / 6 * 4, 0, sparkleAtlas.Width / 6, sparkleAtlas.Height)),
                new(sparkleAtlas, new Rectangle(sparkleAtlas.Width / 6 * 5, 0, sparkleAtlas.Width / 6, sparkleAtlas.Height))
            };
            potTex = content.Load<Texture2D>("pottrans");

            iceTowerTex = new TextureRegion2D(content.Load<Texture2D>("flower"), new Rectangle(12, 105, 54, 72));
            iceBulletTex = new TextureRegion2D[] { new(content.Load<Texture2D>("snowball")) };
            
            particleTextures = new List<TextureRegion2D>();
            particleTextures.Add(new TextureRegion2D(content.Load<Texture2D>("circle")));
            particleTextures.Add(new TextureRegion2D(content.Load<Texture2D>("star")));
            particleTextures.Add(new TextureRegion2D(content.Load<Texture2D>("diamond")));

        }
    }
}
