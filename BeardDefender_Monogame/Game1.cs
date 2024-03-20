using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace BeardDefender_Monogame
{
    public class Game1 : Game
    {
        private Animation idleAnimation;
        private Animation runAnimation;
        private Animation currentAnimation;
        private bool isFacingRight;

        // Olle :*
        Shark shark;
        int sharkFrameIndex;

        Texture2D player;
        Vector2 playerPosition;
        float playerSpeed;

        //Texture2D player;

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
            shark = new (new Vector2(100, 100));

            playerSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.Content.RootDirectory = "Content";

            idleAnimation = new Animation(Content.Load<Texture2D>("Idle-Left"), 0.1f, true);
            runAnimation = new Animation(Content.Load<Texture2D>("Run-LEFT"), 0.1f, true);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Texturer för shark
            shark.TextureLeft[0] = Content.Load<Texture2D>("wackShark1_left");
            shark.TextureLeft[1] = Content.Load<Texture2D>("wackShark2_left");
            shark.TextureRight[0] = Content.Load<Texture2D>("wackShark1_right");
            shark.TextureRight[1] = Content.Load<Texture2D>("wackShark2_right");
            shark.Texture = shark.TextureLeft[0];

            player = Content.Load<Texture2D>("Run-Right");
            ground = Content.Load<Texture2D>("ground 10tiles");
            groundCon = Content.Load<Texture2D>("ground 10tiles");
            groundNext = Content.Load<Texture2D>("ground 10tiles");

            currentAnimation = idleAnimation;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Shark movement, returnerar rätt frame index som används i Update.
            sharkFrameIndex = shark.MoveShark(_graphics, gameTime);

            // Movement settings
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                playerPosition.X -= 5f;
                isFacingRight = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                playerPosition.X += 5f;
                isFacingRight = false;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
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

            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
            {
                currentAnimation = runAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

            currentAnimation.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            SpriteEffects spriteEffects = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            //Vector2 scale = new Vector2(20f, 20f);

            _spriteBatch.Draw(
                texture: currentAnimation.Texture,
                position: playerPosition,
                sourceRectangle: currentAnimation.CurrentFrameSourceRectangle(),
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(currentAnimation.FrameWidth / 2, currentAnimation.FrameHeight / 2),
                scale: Vector2.One,
                effects: spriteEffects,
                layerDepth: 0f
            );

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

            // SHAAAARK, beroende på värdet i SharkIsLeft så används rätt sprites.
            if (!shark.SharkIsLeft)
            {
                _spriteBatch.Draw(
                    shark.TextureLeft[sharkFrameIndex],
                    shark.Position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(
                        shark.Texture.Width / 2,
                        shark.Texture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
            }
            else
            {
                _spriteBatch.Draw(
                shark.TextureRight[sharkFrameIndex],
                shark.Position,
                null,
                Color.White,
                0f,
                new Vector2(
                    shark.Texture.Width / 2,
                    shark.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}