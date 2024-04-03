using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BeardDefender_Monogame.GameObjects;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace BeardDefender_Monogame
{
    public class Game1 : Game
    {
        // Important shit
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        const int MapWidth = 1320;
        const int MapHeight = 720;

        //Background object
        Background background;

        // Unit objects
        Shark shark;
        Hedgehog hedgehog;

        //Player object
        Player player;

        // Obstacles/Ground
        Ground groundLower;
        Ground groundLower2;
        List<Ground> groundList;
        
        Ground groundUpper;
        Ground groundUpper2;
        //List<Ground> upperGroundList;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Grafikinställningar.
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = MapHeight;
            _graphics.PreferredBackBufferWidth = MapWidth;
            _graphics.ApplyChanges();

            // Unit objects
            background = new Background();
            shark = new(new Vector2(100, 100));
            hedgehog = new Hedgehog(new Vector2(100, 100), Content.Load<Texture2D>("Hedgehog_Right"), 0.03f);
            
            
            player = new Player(new RectangleF(100, 400, 25, 36));

            // Obstacle/Ground. Kunde inte använda texturens Height/Width värden här,
            // 80 representerar Height, width är 640. Får klura ut hur man skulle kunna göra annars.
            groundLower = new (new RectangleF(
                0,
                _graphics.PreferredBackBufferHeight - 80,
                _graphics.PreferredBackBufferWidth / 2,
                80));
            groundLower2 = new (new RectangleF(
                groundLower.Position.Right,
                _graphics.PreferredBackBufferHeight - 80,
                640 + 20,
                80));

            groundUpper = new(new RectangleF(
                _graphics.PreferredBackBufferWidth / 4,
                _graphics.PreferredBackBufferHeight - 80 * 2 + 50,
                _graphics.PreferredBackBufferWidth - 640,
                80));
            groundUpper2 = new(new RectangleF(
                _graphics.PreferredBackBufferWidth / 3,
                _graphics.PreferredBackBufferHeight - 80 * 3 + 50,
                _graphics.PreferredBackBufferWidth - 640,
                80
                ));

            //upperGroundList = new()
            //{
                
            //};
            groundList = new()
            {
                groundLower,
                groundLower2,
                groundUpper,
                groundUpper2
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Laddar texturer för Background.
            background.LoadContent(Content);

            // Laddar texturer och animationer för Player.
            player.LoadContent(Content);

            // Texturer för shark
            shark.LoadContent(Content);

            // Ground
            //foreach (var item in upperGroundList)
            //{
            //    item.LoadContent(Content);
            //}
            foreach (Ground ground in groundList)
            {
                ground.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            // Player pos Y för att stå på marken.
            player.position.Y = groundLower.Position.Y - (player.Texture.Height / 4);

            // Shark movement, returnerar rätt frame index som används i Update.
            shark.CurrentFrameIndex = shark.Update(_graphics, gameTime);
            // Hedgehog movement.
            hedgehog.Update(gameTime, new Vector2(player.position.X, player.position.Y));

            // Player movement, sätter players variabel IsFacingRight till returvärdet av
            // metoden, som håller koll på vilket håll spelaren är riktad åt.
            player.IsFacingRight = 
                player.MovePlayer(
                    keyboardState,
                    groundList);

            player.CurrentAnimation.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin();
            
            background.DrawBackground(_spriteBatch, MapWidth, MapHeight);

            //Ground
            foreach (var item in groundList)
            {
                item.Draw(_spriteBatch);
            }
            //foreach (var item in upperGroundList)
            //{
            //    item.Draw(_spriteBatch);
            //}
            // SHAAAARKs draw metod sköter animationer beroende på åt vilket håll hajen rör sig.
            shark.Draw(_spriteBatch);
            hedgehog.Draw(_spriteBatch);

            player.DrawPlayer(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}