﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Spline;
using System;
using System.Collections.Generic;
using System.IO;
using TowerDefense.Bullets;
using TowerDefense.Enemies;
using TowerDefense.Particles;
using TowerDefense.Towers;
using TowerDefense.UI;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using CurrencyManager = TowerDefense.Currencies.CurrencyManager;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private RenderTarget2D renderTarget;

        Texture2D[] gameStateTex;

        TowerManager towerManager = new();
        EnemyManager enemyManager = new();
        BulletManager bulletManager = new();
        CurrencyManager currencyManager = new();

        Tower towerToPlace;
        TowerMenu towerMenu;
        ParticleSystem particleSystem;

        Form1 form1;

        public SimplePath path;

        List<string> stringofPoints = new();
        List<Vector2> points;

        Vector2 towerPos;

        Size2 slimeSize;

        int renderWidth = 800;
        int renderHeight = 500;

        enum Wave { Wave1, Wave2 };
        Wave wave;

        enum GameState { Menu, Game, Win, Loss }
        GameState gameState;

        bool win;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            form1 = new Form1();
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

            gameState = GameState.Menu;
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

            particleSystem = new ParticleSystem(AssetManager.particleTextures);

            towerMenu = new TowerMenu(currencyManager);
            TexBgForStates();


        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Input.IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = gameTime.GetElapsedSeconds();

            switch (gameState)
            {
                case GameState.Menu:
                    form1.Show();
                    if (form1.quitClicked)
                    {
                        form1.Close();
                        Exit();
                    }
                    if (form1.startClicked)
                    {
                        form1.Close();
                        gameState = GameState.Game;

                    }
                    break;

                case GameState.Game:
                    slimeSize = new Size2(AssetManager.slimeRunTex.Width / 4, AssetManager.slimeRunTex.Height);
                    towerPos = new Vector2(Input.mouseState.X, Input.mouseState.Y);

                    switch (wave)
                    {
                        case Wave.Wave1:
                            enemyManager.SpawnFirstWaveEnemies(deltaTime, path);
                            if (enemyManager.IsFirstWaveComplete())
                            {
                                wave = Wave.Wave2;
                                enemyManager.ResetWave();
                            }
                            break;

                        case Wave.Wave2:
                            enemyManager.ClearWaveList();
                            enemyManager.SpawnSecondWaveEnemies(deltaTime, path);
                            if (enemyManager.IsSecondWaveComplete())
                            {
                                win = true;
                            }
                            break;
                    }

                    if (towerToPlace != null && TryPlaceTower(towerToPlace))
                    {
                        towerToPlace = null;
                    }


                    //Pressing 1 = regular tower. 2 = Ice tower. 0 = no tower. 
                    if (Input.KeyPressed(Keys.D1))
                    {
                        Tower regularTower;

                        currencyManager.TryToPurchaseTower(regularTower = new Tower(AssetManager.towerTex, towerPos, AssetManager.towerTex.Size));
                        if (currencyManager.purchased)
                        {
                            towerToPlace = regularTower;

                        }


                    }
                    else if (Input.KeyPressed(Keys.D2))
                    {
                        IceTower iceTower;
                        currencyManager.TryToPurchaseTower(iceTower = new IceTower(AssetManager.iceTowerTex, towerPos, AssetManager.iceTowerTex.Size));
                        if (currencyManager.purchased)
                        {
                            towerToPlace = iceTower;

                        }
                    }
                    else if (Input.KeyPressed(Keys.D0))
                    {
                        towerToPlace = null;
                    }

                    if (towerToPlace != null)
                    {
                        towerToPlace.Position = towerPos;
                    }

                    towerManager.Update(deltaTime, bulletManager, enemyManager);

                    enemyManager.Update(deltaTime, path, particleSystem, currencyManager);

                    bulletManager.Update(deltaTime, enemyManager);

                    particleSystem.Update();

                    if (enemyManager.slimesWin)
                    {
                        gameState = GameState.Loss;
                    }
                    if (win)
                    {
                        gameState = GameState.Win;
                    }
                    break;

                case GameState.Win:
                    if (Input.KeyPressed(Keys.Enter))
                    {
                        Exit();
                    }
                    break;

                case GameState.Loss:
                    if (Input.KeyPressed(Keys.Enter))
                    {
                        Exit();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawOnRenderTarget();
            spriteBatch.Begin(blendState: BlendState.NonPremultiplied);

            spriteBatch.Draw(gameStateTex[(int)gameState],
                new Vector2(0, 0),
                null, Color.White);

            if (gameState == GameState.Game)
            {
                spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
                //using FileStream file = new("rendertarget.png", FileMode.Create);
                //renderTarget.SaveAsPng(file, renderTarget.Width, renderTarget.Height);
                //spriteBatch.Draw(AssetManager.backgroundTex, Vector2.Zero, Color.White);

                //path.Draw(spriteBatch);
                //path.DrawPoints(spriteBatch);

                towerManager.Draw(spriteBatch, towerToPlace);

                enemyManager.Draw(spriteBatch);

                bulletManager.Draw(spriteBatch);

                particleSystem.Draw(spriteBatch);

                towerMenu.Draw(spriteBatch);
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

            towerManager.Draw(spriteBatch, null);

            spriteBatch.Draw(AssetManager.renderBackGroundTex, Vector2.Zero, Color.White); //Equivelent till bakgrunden här

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        public bool CanPlace(Tower tower)
        {
            TextureRegion2D reg = tower.Texture;
            Color[] targetPixels = new Color[reg.Width * reg.Height];
            Color[] pixels2 = new Color[targetPixels.Length];
            reg.Texture.GetData(0, reg.Bounds, pixels2, 0, pixels2.Length);
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

        public void TexBgForStates()
        {
            gameStateTex = new Texture2D[4];
            gameStateTex[(int)GameState.Menu] = AssetManager.backgroundTex;
            gameStateTex[(int)GameState.Game] = AssetManager.backgroundTex;
            gameStateTex[(int)GameState.Loss] = AssetManager.badEndBgTex;
            gameStateTex[(int)GameState.Win] = AssetManager.goodEndBgTex;
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
