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

        public static void LoadTextures(ContentManager content)
        {
            backgroundTex = content.Load<Texture2D>("truebg");

        }
    }
}
