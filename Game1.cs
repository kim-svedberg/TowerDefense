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

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        SimplePath path;
        MouseState mouseState, oldMouseState;
        
        //List<Vector2> pointList = new List<Vector2>();
        List<string> stringofPoints = new List<string>();
        List<Vector2> points;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            KeyMouseReader.Update();

            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                path.AddPoint(mousePos);
                points.Add(mousePos); //Improvement idea: Kunna ta bort punkter med högerklick. Det svåra: ta bort punkterna från filen?
            }
            else if (KeyMouseReader.KeyPressed(Keys.S))
            {
                SaveToFile();
            }
         

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Vector2 point in points)
            {
                path.Draw(spriteBatch);
                path.DrawPoints(spriteBatch);
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
        List<Vector2> LoadPointsForMap()
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


        //Imp id: Inte kunna sätta ut på samma punkt 2 ggr 

    }
}