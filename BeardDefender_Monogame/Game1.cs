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
    enum Scenes
    {
        MAIN_MENU,
        GAME,
        HIGHSCORE
    };
    public class Game1 : Game
    {
        private Scenes activeScenes;
        //private Texture2D texture;

        // Important shit
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        const int MapWidth = 1320;
        const int MapHeight = 720;

        bool startGameSelected = true; // Starta spelet är förvalt
        bool exitGameSelected = false;
        bool highscoreSelected = false;

        private bool previousUpPressed = false;
        private bool previousDownPressed = false;
        private bool previousEnterPressed = false;



        // MainMenu object
        MainMenu mainmenu;
        SpriteFont buttonFont;

        //Highscore object
        Highscore highscore;

        //Background object
        Background background;
        Background background2;

        // Unit objects
        Shark shark;
        Hedgehog hedgehog;
        Crabman crabman;

        //Player object
        Player player;

        // Obstacles/Ground
        Ground groundLower;
        Ground groundLower2;
        Ground groundLower3;
        Ground groundLower4;
        Ground groundLower5;

        List<Ground> groundList;
       

        public Game1()
        {
            activeScenes = Scenes.MAIN_MENU;

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
            mainmenu = new MainMenu();
            highscore = new Highscore();
            background = new Background(0);
            background2 = new Background(1320);
            shark = new(new Vector2(100, 100));
            crabman = new Crabman();

            player = new Player(new RectangleF(100, 400, 25, 36));

            // Obstacle/Ground. Kunde inte använda texturens Height/Width värden här,
            // 80 representerar Height, width är 640. Får klura ut hur man skulle kunna göra annars.
            groundLower = new (new RectangleF(

                0,
                _graphics.PreferredBackBufferHeight - 80,
                _graphics.PreferredBackBufferWidth / 2,
                80));

            groundLower2 = new (new RectangleF(

                groundLower.Position.Right - 20,
                _graphics.PreferredBackBufferHeight - 80,
                640 + 20,
                80));
            groundLower3 = new(new RectangleF(
                groundLower2.Position.Right - 20,
                _graphics.PreferredBackBufferHeight - 80,
                groundLower2.Position.Width,
                groundLower2.Position.Height
                ));
            groundLower4 = new(new RectangleF(
                groundLower3.Position.Right - 20,
                _graphics.PreferredBackBufferHeight - 80,
                groundLower3.Position.Width,
                groundLower3.Position.Height
                ));
            groundLower5 = new(new RectangleF(
                groundLower4.Position.Right - 20,
                _graphics.PreferredBackBufferHeight - 80,
                groundLower4.Position.Width,
                groundLower4.Position.Height
                ));

            groundList = new()
            {
                groundLower,
                groundLower2,
                groundLower3,
                groundLower4,
                groundLower5,
            };
            hedgehog = new Hedgehog(new Vector2(400, groundLower.Position.Y - 50), Content.Load<Texture2D>("Hedgehog_Right"), 0.03f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            buttonFont = Content.Load<SpriteFont>("Font");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Laddar texturer för MainMenu.
            mainmenu.LoadContent(Content);

            //Laddar texturer för Background.
            background.LoadContent(Content);
            background2.LoadContent(Content);

            //laddar texturer för Highscore
            highscore.LoadContent(Content);

            // Laddar texturer och animationer för Player.
            player.LoadContent(Content);

            //Texturer för crabman
            crabman.LoadContent(Content);

            // Texturer för shark
            shark.LoadContent(Content);
            // Ground
            foreach (Ground ground in groundList)
            {
                ground.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            switch (activeScenes)
            {
                case Scenes.MAIN_MENU:

                    KeyboardState state = Keyboard.GetState();
                    mainmenu.Update(gameTime);

                    bool upDownPressed = state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Down);
                    if (upDownPressed && !previousUpPressed && !previousDownPressed)
                    {
                        startGameSelected = !startGameSelected;
                        highscoreSelected = !highscoreSelected;
                        exitGameSelected = !exitGameSelected;
                    }

                    if (state.IsKeyDown(Keys.Enter) && !previousEnterPressed)
                    {
                        if (startGameSelected)
                        {
                            activeScenes = Scenes.GAME;
                        }
                        else if(state.IsKeyDown(Keys.Enter) && !previousEnterPressed )
                        {
                            activeScenes = Scenes.HIGHSCORE;
                        }
                        else if (exitGameSelected)
                        {
                            Exit();
                        }
                    }

                    previousUpPressed = state.IsKeyDown(Keys.Up);
                    previousDownPressed = state.IsKeyDown(Keys.Down);
                    previousEnterPressed = state.IsKeyDown(Keys.Enter);


                    break;

                case Scenes.GAME:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        activeScenes = Scenes.MAIN_MENU;
                    }

                    break;

                    case Scenes.HIGHSCORE:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        activeScenes = Scenes.MAIN_MENU;
                    }
                    break;

            }

            KeyboardState keyboardState = Keyboard.GetState();

            // Player pos Y för att stå på marken.
            player.position.Y = groundLower.Position.Y - (player.Texture.Height / 4);
            background.Update(gameTime, GraphicsDevice.Viewport.Width);
            background2.Update(gameTime, GraphicsDevice.Viewport.Width);
            foreach (Ground ground in groundList)
            {
                ground.Update(gameTime, GraphicsDevice.Viewport.Width);
            }

            crabman.CurrentFrameIndex = crabman.Update(_graphics, gameTime);
            // Shark movement, returnerar rätt frame index som används i Update.
            shark.CurrentFrameIndex = shark.Update(_graphics, gameTime);
            // Hedgehog movement.
            hedgehog.Update(gameTime, new Vector2(player.position.X, player.position.Y));

            // Player movement, sätter players variabel IsFacingRight till returvärdet av
            // metoden, som håller koll på vilket håll spelaren är riktad åt.
            player.IsFacingRight =
                player.MovePlayer(
                    keyboardState,
                    hedgehog,
                    groundList);

            player.CurrentAnimation.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin();

            switch (activeScenes)

            {
                case Scenes.MAIN_MENU:

                    //_spriteBatch.Draw(texture, new Rectangle(0, 0, MapWidth, MapHeight), Color.White);
                    mainmenu.DrawMainMenu(_spriteBatch, MapWidth, MapHeight);

                    // Ritar "Starta spelet"-val
                    _spriteBatch.DrawString(buttonFont, "PLAY", new Vector2(616, 370), startGameSelected ? Color.Red : Color.Black);

                    //Ritar "Highscore"-val
                    _spriteBatch.DrawString(buttonFont, "SCORE", new Vector2(590, 470), startGameSelected ? Color.Red : Color.Black);

                    // Ritar "Avsluta spelet"-val
                    _spriteBatch.DrawString(buttonFont, "EXIT", new Vector2(618, 575), exitGameSelected ? Color.Red : Color.Black);


                    break;

                case Scenes.GAME:

                    background.DrawBackground(_spriteBatch, MapWidth, MapHeight);
                    background2.DrawBackground(_spriteBatch, MapWidth, MapHeight);

                    player.DrawPlayer(_spriteBatch);

                    // SHAAAARKs draw metod sköter animationer beroende på åt vilket håll hajen rör sig.
                    crabman.Draw(_spriteBatch);
                    shark.Draw(_spriteBatch);
                    hedgehog.Draw(_spriteBatch);

                    //Ground
                    foreach (var item in groundList)
                    {
                        item.Draw(_spriteBatch);
                    }
                    
                    break;

                case Scenes.HIGHSCORE:
                    highscore.DrawBackground(_spriteBatch, MapWidth, MapHeight);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}