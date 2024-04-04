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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        const int MapWidth = 1320;
        const int MapHeight = 720;
        bool startGameSelected = true;
        bool exitGameSelected = false;
        bool highscoreSelected = false;
        private bool previousUpPressed = false;
        private bool previousDownPressed = false;
        private bool previousEnterPressed = false;

        //// MainMenu object
        MainMenu mainmenu;
        SpriteFont buttonFont;

        ////Highscore object
        Highscore highscore;

        ////Background object
        Background background;

        //// Unit objects
        Shark shark;
        Hedgehog hedgehog;

        ////Player object
        Player player;

        //// Obstacles/Ground
        Ground groundLower, groundLower2;
        List<Ground> groundList;

        public Game1()
        {
            activeScenes = Scenes.MAIN_MENU;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //// Grafikinställningar.
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = MapHeight;
            _graphics.PreferredBackBufferWidth = MapWidth;
            _graphics.ApplyChanges();

            //// Unit objects
            mainmenu = new MainMenu();
            highscore = new Highscore();
            background = new Background();
            shark = new Shark(new Vector2(100, 100));
            hedgehog = new Hedgehog(new Vector2(400, 400), Content.Load<Texture2D>("Hedgehog_Right"), 0.03f);
            player = new Player(new Vector2(100, 400), _spriteBatch);

            //// Obstacle/Ground. Kunde inte använda texturens Height/Width värden här,
            //// 80 representerar Height, width är 640. Får klura ut hur man skulle kunna göra annars.
            groundLower = new Ground(new RectangleF(0, _graphics.PreferredBackBufferHeight - 80, _graphics.PreferredBackBufferWidth / 2, 80));
            groundLower2 = new Ground(new RectangleF(groundLower.Position.Right, _graphics.PreferredBackBufferHeight - 80, 640 + 20, 80));
            groundList = new List<Ground> { groundLower, groundLower2 };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //texture = Content.Load<Texture2D>("BeardDefender_MainMenu");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            buttonFont = Content.Load<SpriteFont>("Font");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Laddar texturer för MainMenu.
            mainmenu.LoadContent(Content);

            //Laddar texturer för Background.
            background.LoadContent(Content);

            //laddar texturer för Highscore
            highscore.LoadContent(Content);

            // Laddar texturer och animationer för Player.
            player = new Player(new Vector2(100, 400), _spriteBatch);
            player.LoadContent(Content);

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
            KeyboardState state = Keyboard.GetState();
            switch (activeScenes)
            {
                case Scenes.MAIN_MENU:

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

                    player.MovePlayer(gameTime, state, groundList); 
                    shark.Update(_graphics, gameTime);
                    hedgehog.Update(gameTime, new Vector2(player.position.X, player.position.Y));

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
            //player.position.Y = groundLower.Position.Y - (player.Texture.Height / 4);

            // Shark movement, returnerar rätt frame index som används i Update.
            shark.CurrentFrameIndex = shark.Update(_graphics, gameTime);

            // Hedgehog movement.
            hedgehog.Update(gameTime, new Vector2(player.position.X, player.position.Y));

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
                    player.Draw(gameTime);
                    shark.Draw(_spriteBatch);
                    hedgehog.Draw(_spriteBatch);
                    foreach (var ground in groundList)
                    {
                        ground.Draw(_spriteBatch);
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