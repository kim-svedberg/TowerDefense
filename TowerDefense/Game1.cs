using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Spline;
using System;
using System.Collections.Generic;
using System.IO;
using TowerDefense.Bulets;
using TowerDefense.Enemies;
using TowerDefense.Towers;
using WinForm;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private RenderTarget2D renderTarget;

        TowerManager towerManager = new();
        EnemyManager enemyManager = new();
        BulletManager bulletManager = new();

        Tower towerToPlace;

        Form1 form1;

        public SimplePath path;

        List<string> stringofPoints = new();
        List<Vector2> points;

        Size2 slimeSize; //Size2 = en width och en height
        Size2 towerSize;

        Vector2 towerPos;

        int renderWidth = 800;
        int renderHeight = 500;

        float spawnTimer = 0f;
        float spawnDelay = 3f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            form1 = new Form1();
            form1.Show();
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
                foreach (Vector2 point in points)
                {
                    path.AddPoint(point);
                }
            }

            foreach (SlimeEnemy slime in enemyManager.slimeEnemyList)
            {
                slime.SlimePosForPath(path);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Input.IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = gameTime.GetElapsedSeconds();

            slimeSize = new Size2(AssetManager.slimeRunTex.Width / 4 - 80, AssetManager.slimeRunTex.Height - 80);

            towerSize = new Size2(AssetManager.towerTex.Width, AssetManager.towerTex.Height);
            towerPos = new Vector2(Input.mouseState.X, Input.mouseState.Y);

            //if(keyState.IsKeyDown(Keys.B) && oldKeyState == keyState.IsKeyUp(Keys.B))
            {
                //Leta upp hur man gör det här. När man trycker på B = spawna towern som ska följa musen. 
                //tower = new Tower(AssetManager.towerTex, towerPos, towerHitBox, placed, bulletManager);
            }

            SpawnEnemies(deltaTime);

            if (towerToPlace != null && TryPlaceTower(towerToPlace))
            {
                towerToPlace = null;
            }

            if (towerToPlace == null)
            {
                towerToPlace = new Tower(AssetManager.towerTex, towerPos, towerSize);
            }
            else
            {
                towerToPlace.Position = towerPos;
            }

            towerManager.Update(deltaTime, bulletManager, enemyManager);

            enemyManager.Update(deltaTime, path);

            bulletManager.Update(deltaTime, enemyManager);

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

            path.Draw(spriteBatch);
            path.DrawPoints(spriteBatch);

            towerManager.Draw(spriteBatch, towerToPlace);

            enemyManager.Draw(spriteBatch);

            bulletManager.Draw(spriteBatch);

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
                Vector2 point = new(
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

            foreach (Tower tower in towerManager.towerList)
            {
                tower.Draw(spriteBatch);
            }

            spriteBatch.Draw(AssetManager.renderBackGroundTex, Vector2.Zero, Color.White); //Equivelent till bakgrunden här

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        public bool CanPlace(Tower tower)
        {
            Color[] targetPixels = new Color[tower.Texture.Width * tower.Texture.Height];
            Color[] pixels2 = new Color[targetPixels.Length];
            tower.Texture.GetData(pixels2);
            try
            {
                renderTarget.GetData(0, tower.HitBox.ToRectangle(), targetPixels, 0, targetPixels.Length);
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
        internal void SpawnEnemies(float deltaTime)
        {
            spawnTimer -= deltaTime;
            if (spawnTimer <= 0)
            {
                SlimeEnemy slimeEnemy = new(AssetManager.slimeRunTex, path.GetPos(0), slimeSize);
                enemyManager.AddEnemy(slimeEnemy);
                spawnTimer = spawnDelay;
            }
        }

        internal bool TryPlaceTower(Tower tower)
        {
            bool canPlace = CanPlace(tower);
            if (!canPlace)
            {
                tower.color = Color.MediumVioletRed;
                return false;
            }

            if (Input.LeftClick())
            {
                tower.color = Color.White;
                tower.placed = true;

                towerManager.AddTower(tower);
                return true;
            }

            tower.color = Color.LightGreen;
            return false;
        }
    }
}

//Sätt in detta i Update för att få punkt/path editorn:
//Vector2 mousePos = new Vector2(mouseState.Position.X, mouseState.Position.Y);

//if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
//{
//    path.AddPoint(mousePos);
//    points.Add(mousePos); //Improvement idea: Kunna ta bort punkter med högerklick. Det svåra: ta bort punkterna från filen?
//}
//else if (KeyMouseReader.KeyPressed(Keys.S))
//{
//    SaveToFile();
//}
