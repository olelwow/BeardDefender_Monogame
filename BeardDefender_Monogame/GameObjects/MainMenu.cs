using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace BeardDefender_Monogame.GameObjects
{
    internal class MainMenu
    {
        public enum MenuOption
        {
            PLAY,
            SCORE,
            EXIT
        };
        //Olika layers för att skapa bakgrunden i spelet.
        private Texture2D background1;        
        private List<Texture2D> _backgroundImages = new List<Texture2D>();
        private int _currentImageIndex = 1;
        private double _timer = 0;
        private double _interval = 100;
        private MenuOption currentMenuOption = MenuOption.PLAY;
        private SpriteFont buttonFont;
        private bool previousUpPressed = false;
        private bool previousDownPressed = false;
        private bool previousEnterPressed = false;


        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.            
            for (int i = 1; i <= 100; i++)
            {
                _backgroundImages.Add(Content.Load<Texture2D>("MainMenuBackground/BeardDefender" + i.ToString()));
            }
            buttonFont = Content.Load<SpriteFont>("Font");
        }
        public void Update(GameTime gameTime, Game1 game, KeyboardState keyboardState)
        {
            // Uppdatera timern
            _timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            
            // Om timern överskrider intervallen så ändras bakgrunden
            if (_timer > _interval)
            {
                _currentImageIndex = (_currentImageIndex + 1) % _backgroundImages.Count;
                _timer = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Down) && !previousDownPressed)
            {
                currentMenuOption = (MenuOption)(((int)currentMenuOption + 1) % 3);
            }
            else if (keyboardState.IsKeyDown(Keys.Up) && !previousUpPressed)
            {
                currentMenuOption = (MenuOption)(((int)currentMenuOption + 2) % 3);
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousEnterPressed)
            {
                switch (currentMenuOption)
                {
                    case MenuOption.PLAY:
                        game.ActiveScenes = game.LastPlayedLevel;
                        break;
                    case MenuOption.SCORE:

                        game.ActiveScenes = Scenes.HIGHSCORE;

                        break;
                    case MenuOption.EXIT:
                        game.Exit();
                        break;
                }
            }
            previousUpPressed = keyboardState.IsKeyDown(Keys.Up);
            previousDownPressed = keyboardState.IsKeyDown(Keys.Down);
            previousEnterPressed = keyboardState.IsKeyDown(Keys.Enter);

        }
        public void DrawMainMenu(SpriteBatch _spriteBatch, int MapWidth, int MapHeight)
        {
            //Skapar en rektangel i storlek med spelrutan och förstorar bakgrundsbilderna-1
            //till samma storlek som spelytan.
            Vector2 desiredSize = new Vector2(MapWidth, MapHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, (int)desiredSize.X, (int)desiredSize.Y);

            _spriteBatch.Draw(_backgroundImages[_currentImageIndex], destinationRectangle, Color.White);
            //// Ritar "Starta spelet"-val
            _spriteBatch.DrawString(buttonFont, "PLAY", new Vector2(616, 370), currentMenuOption == MenuOption.PLAY ? Color.Red : Color.Black);

            ////Ritar "Highscore"-val
            _spriteBatch.DrawString(buttonFont, "SCORE", new Vector2(590, 470), currentMenuOption == MenuOption.SCORE ? Color.Red : Color.Black);

            //// Ritar "Avsluta spelet"-val
            _spriteBatch.DrawString(buttonFont, "EXIT", new Vector2(618, 575), currentMenuOption == MenuOption.EXIT ? Color.Red : Color.Black);
        }

        // Get/Set
        public Texture2D Background1
        {
            get { return background1; }
            set { background1 = value; }
        }
    }
}
