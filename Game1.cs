using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using Spline;
using System;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using json_reader;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MarioTest;
using MonoGame.Extended.Gui.Serialization;
using System.Diagnostics;
using SharpDX.Direct3D9;

namespace TowerDefense
{
    public class Game1 : Game
    {
        //Decreasement for-loop istället för incr. 
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private RenderTarget2D renderTarget;

        EnemyManager enemyManager = new EnemyManager();
        BulletManager bulletManager = new BulletManager();
        Tower tower;

        public SimplePath path;
        MouseState mouseState, oldMouseState = Mouse.GetState();
        KeyboardState keyState, oldKeyState = Keyboard.GetState();
        
        List<string> stringofPoints = new List<string>();
        List<Vector2> points;
        List<GameObject> objectList = new List<GameObject>();


        Rectangle slimeHitBox;
        Rectangle towerHitBox;

        Vector2 slimePos;
        Vector2 towerPos;

        int renderWidth = 800;
        int renderHeight = 500;

        float shootDelay;
        float bulletExistingTime;
        bool placed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);

        }

        protected override void Initialize()
        {

            base.Initialize();
            graphics.ToggleFullScreen();
            graphics.ApplyChanges();


        }

        protected override void LoadContent()
        {    
            spriteBatch = new SpriteBatch(GraphicsDevice);
            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, renderWidth, renderHeight);


            AssetManager.LoadTextures(Content);
            path = new SimplePath(graphics.GraphicsDevice);
            path.Clean();

            if (File.Exists("Points.csv"))
            {
                points = LoadPointsForMap();
                foreach(Vector2 point in points)
                {
                    path.AddPoint(point);
                }
            }

            enemyManager.AddEnemies(slimePos, slimeHitBox);

            foreach(SlimeEnemy slime in enemyManager.slimeEnemyList) 
            {
                slime.SlimePosForPath(path);
            }

           

            tower = new Tower(AssetManager.towerTex, towerPos, towerHitBox);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = gameTime.GetElapsedSeconds();

            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            tower.Pos = new Vector2(mouseState.X, mouseState.Y);
           
            foreach(SlimeEnemy slime in enemyManager.slimeEnemyList)
            {

                slime.HitBox = new Rectangle((int)slimePos.X, (int)slimePos.Y, AssetManager.slimeRunTex.Width / 4 - 80 , AssetManager.slimeRunTex.Height - 80);

            }

            tower.HitBox = new Rectangle(mouseState.X, mouseState.Y, AssetManager.towerTex.Width, AssetManager.towerTex.Height);

            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released && CanPlace(tower))
            {
                tower.color = Color.White;
                objectList.Add(tower);
                tower = new Tower(AssetManager.towerTex, towerPos, towerHitBox);
                placed = true;

            }

            if (CanPlace(tower))
            {
                tower.color = Color.LightGreen;

            }
            else if (!CanPlace(tower))
            {
                tower.color = Color.MediumVioletRed;
            }

            oldMouseState = Mouse.GetState();

            foreach(SlimeEnemy slime in enemyManager.slimeEnemyList)
            {

                slime.Update(gameTime, path);

            }

            foreach(Bullet bullet in bulletManager.bulletList)
            {

                bullet.Update(gameTime);

            }

            foreach(Tower tower in objectList)
            {
                if (placed)
                {
                    shootDelay -= deltaTime;
                    if (shootDelay <= 0)
                    {
                        shootDelay = 2;

                        for (int i = 0; i < 100; i++)
                        {
                            bulletManager.CreateBullet(tower, enemyManager.slimeEnemyList, gameTime);
                        }
                    }

                }
            }

          
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawOnRenderTarget();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(blendState: BlendState.NonPremultiplied);

            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            //using FileStream file = new("rendertarget.png", FileMode.Create);
            //renderTarget.SaveAsPng(file, renderTarget.Width, renderTarget.Height);
            spriteBatch.Draw(AssetManager.backgroundTex, Vector2.Zero, Color.White);


            foreach (Vector2 point in points)
            {
                path.Draw(spriteBatch);
                path.DrawPoints(spriteBatch);
            }

            foreach (GameObject gameObject in objectList)
            {
                gameObject.Draw(spriteBatch);
            }

            foreach(SlimeEnemy slime in enemyManager.slimeEnemyList)
            {

                slime.Draw(spriteBatch, path);

            }

            tower.Draw(spriteBatch);

            foreach (Bullet bullet in bulletManager.bulletList)
            {
                bullet.Draw(spriteBatch);

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SaveToFile()
        {
            foreach (Vector2 point in points)
            {
                stringofPoints.Add($"{point.X}, {point.Y}");
            }
            File.WriteAllLines("Points.csv", stringofPoints);
        }
        public void WriteToFile(string csv)
        {
            string previous = File.ReadAllText(csv);

        }
        public List<Vector2> LoadPointsForMap()
        {

            string[] pointStrings = File.ReadAllLines("Points.csv");
            points = new List<Vector2>();

            foreach (string pointString in pointStrings)
            {
                string[] tokens = pointString.Split(",");
                Vector2 point = new Vector2(
                            Convert.ToInt32(tokens[0]),
                            Convert.ToInt32(tokens[1]));
                points.Add(point);
            }

            return points;

        }
        private void DrawOnRenderTarget()
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            foreach (GameObject gameObject in objectList)
            {
                gameObject.Draw(spriteBatch);
            }
            spriteBatch.Draw(AssetManager.renderBackGroundTex, Vector2.Zero, Color.White); //Equivelent till bakgrunden här


            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }
        public bool CanPlace(GameObject gameObject)
        {
            Color[] targetPixels = new Color[gameObject.Texture.Width * gameObject.Texture.Height];
            Color[] pixels2 = new Color[targetPixels.Length];
            gameObject.Texture.GetData<Color>(pixels2);
            try
            {
                renderTarget.GetData(0, gameObject.HitBox, targetPixels, 0, targetPixels.Length);
            }
            catch
            {
                return false;
            }
            for (int i = 0; i < targetPixels.Length; ++i)
            {
                if (targetPixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                    return false;
            }
            return true;

        } 

    }
}

//Sätt in detta i Update för att få punkt/path editorn:
//oldMouseState = mouseState;
//mouseState = Mouse.GetState();

//Vector2 mousePos = new Vector2(mouseState.Position.X, mouseState.Position.Y);
//KeyMouseReader.Update();

//if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
//{
//    path.AddPoint(mousePos);
//    points.Add(mousePos); //Improvement idea: Kunna ta bort punkter med högerklick. Det svåra: ta bort punkterna från filen?
//}
//else if (KeyMouseReader.KeyPressed(Keys.S))
//{
//    SaveToFile();
//}
