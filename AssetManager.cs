using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using Microsoft.Xna.Framework.Content;
using System.Drawing.Drawing2D;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace MarioTest
{
    public static class AssetManager
    {
        public static Texture2D backgroundTex;
        public static Texture2D renderBackGroundTex;
        public static Texture2D slimeRunTex;
        public static Texture2D towerTex;
        public static Texture2D bulletTex;
        public static Texture2D potTex;

        public static void LoadTextures(ContentManager content)
        {
            backgroundTex = content.Load<Texture2D>("truebg");
            renderBackGroundTex = content.Load<Texture2D>("truebgPOTStrans");
            slimeRunTex = content.Load<Texture2D>("Slime_Spiked_Run");
            towerTex = content.Load<Texture2D>("flower8BIG");
            bulletTex = content.Load<Texture2D>("sparklesprite");
            potTex = content.Load<Texture2D>("pottrans");


        }
    }
}
