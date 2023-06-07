using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.TextureAtlases;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace TowerDefense
{
    public static class AssetManager
    {
        public static SpriteFont font;
        
        public static Texture2D backgroundTex;
        public static Texture2D badEndBgTex;
        public static Texture2D goodEndBgTex;
        public static Texture2D renderBackGroundTex;
        public static Texture2D potTex;
        public static Texture2D cutScene;
       
        public static TextureRegion2D slimeRunTex;
        public static TextureRegion2D tankSlimeTex;
        public static TextureRegion2D towerTex;
        public static TextureRegion2D iceTowerTex;
        public static TextureRegion2D towerMenuTex;
        public static TextureRegion2D coinTex;
        public static TextureRegion2D heartTex;

        public static TextureRegion2D[] bulletTex;
        public static TextureRegion2D[] iceBulletTex;

        public static List<TextureRegion2D> particleTextures;

        public static Song gameMusic;
        public static Song winMusic;
        public static Song lossMusic;
        public static Song menuMusic;


        public static void LoadTextures(ContentManager content)
        {
            backgroundTex = content.Load<Texture2D>("truebg");
            renderBackGroundTex = content.Load<Texture2D>("truetransparentBG");
            slimeRunTex = new TextureRegion2D(content.Load<Texture2D>("Slime_Spiked_Run"), new Rectangle(38, 54, 46, 41));
            tankSlimeTex = new TextureRegion2D(content.Load<Texture2D>("pinkSlimeSheetRight"), new Rectangle(145, 15, 58, 43));
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

            towerMenuTex = new TextureRegion2D(content.Load<Texture2D>("ui"));

            font = content.Load<SpriteFont>("File");

            coinTex = new TextureRegion2D(content.Load<Texture2D>("coin"));

            heartTex = new TextureRegion2D(content.Load<Texture2D>("heart"));

            badEndBgTex = content.Load<Texture2D>("badending");
            goodEndBgTex = content.Load<Texture2D>("goodend");

            gameMusic = content.Load<Song>("gameMusic");
            winMusic = content.Load<Song>("winMusic");
            lossMusic = content.Load<Song>("lossMusic");
            menuMusic = content.Load<Song>("menuMusic");

            cutScene = content.Load<Texture2D>("opscene");

        }
    }
}
