using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace BeardDefender_Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _playerTexture;
        private Vector2 _playerPosition;
        private float _playerSpeed = 5f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // jag fick ändra så jag kan lada upp bilden men kanske ni ska ändra sen?
            using (FileStream fileStream = new FileStream("Content/playerTexture.png", FileMode.Open))
            {
                _playerTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }

            // startposition blir till botten
            _playerPosition = new Vector2(100, GraphicsDevice.Viewport.Height - _playerTexture.Height);
        }

                protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Flytta spelaren med tangentbordet
            KeyboardState keyboardState = Keyboard.GetState();  
            if (keyboardState.IsKeyDown(Keys.Left) && _playerPosition.X > 0) // ser till att spelare inte går utanfär på vänstra sidan
            {
                _playerPosition.X -= _playerSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) && _playerPosition.X + _playerTexture.Width < GraphicsDevice.Viewport.Width) // ser till att spelare inte går utanfär på högra sidan 
            {
                _playerPosition.X += _playerSpeed;
            }
            
            if (keyboardState.IsKeyDown(Keys.Up) && _playerPosition.Y > 0 && _playerPosition.Y == GraphicsDevice.Viewport.Height - _playerTexture.Height)
            {
                _playerPosition.Y -= _playerSpeed * 30;// här bestämmer man hur hög ska hoppa 
            }

            if (_playerPosition.Y < GraphicsDevice.Viewport.Height - _playerTexture.Height)
            {
                _playerPosition.Y += _playerSpeed * 2; // här var den ska landa
            }

            base.Update(gameTime);
}


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Rita spelaren
            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
