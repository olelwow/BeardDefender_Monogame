using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace BeardDefender_Monogame
{
    public class Game1 : Game
    {
        Texture2D player;
        Vector2 playerPosition;
        Vector2 playerPositionNew;

        Texture2D ground;
        Vector2 groundPosition;
        Texture2D groundCon;
        Vector2 groundPositionCon;
        Texture2D groundNext;
        Vector2 groundPositionNext;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        bool jumping = false;
        int jumpSpeed = 12;
        float gravity = 12;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            groundPosition = new Vector2(0, 400);
            groundPositionCon = new Vector2();
            groundPositionNext = new Vector2();
            playerPosition = new Vector2(10, 365);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = Content.Load<Texture2D>("Run-Right");
            ground = Content.Load<Texture2D>("ground 10tiles");
            groundCon = Content.Load<Texture2D>("ground 10tiles");
            groundNext = Content.Load<Texture2D>("ground 10tiles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Movement settings
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                playerPosition.X -= 5f;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                playerPosition.X += 5f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                playerPosition.Y += 0f;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                jumping = true;
            }

            playerPosition.Y += jumpSpeed; 

            if (jumping == true && gravity < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -12; //pushing the player upwards
                gravity -= 1; //when this value results < 0 then the player goes downwards again
            }
            else
            {
                jumpSpeed = 12;
            }

            //Resets the jump and the position of the player
            if (playerPosition.Y > groundPositionNext.Y && jumping == false)
            {
                gravity = 12;
                playerPosition.Y = 365;
                jumpSpeed = 0;
            }

            //Check for collision
            if (playerPosition.X == groundPositionNext.X)
            {
                playerPosition.X = groundPositionNext.X;
            }

            if (playerPosition.Y - player.Height < ground.Height)
            {
                playerPosition.Y = ground.Bounds.Top - player.Height;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //Player
            _spriteBatch.Draw(
                player,
                playerPosition,
                new Rectangle(16, 16, 35, 47),
                Color.White);

            //Ground
            _spriteBatch.Draw(
                ground,
                groundPosition,
                Color.White);
            _spriteBatch.Draw(
                groundCon,
                groundPositionCon = new Vector2(ground.Width, 400),
                Color.White);
            _spriteBatch.Draw(
                groundNext,
                groundPositionNext = new Vector2(ground.Width - 100, 365),
                Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
