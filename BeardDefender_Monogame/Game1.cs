using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using BeardDefender_Monogame.GameObjects;

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
        Player player1;
        int sharkFrameIndex;

        Texture2D player;
        Vector2 playerPosition;

        //Texture2D player;

        Texture2D ground;
        Vector2 groundPosition;
        Texture2D groundCon;
        Vector2 groundPositionCon;
        Texture2D groundNext;
        Vector2 groundPositionNext;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Grafik inställningar
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1320;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            // Objekt positioner
            groundPosition = new Vector2(0, 640);
            groundPositionCon = new Vector2();
            groundPositionNext = new Vector2();
            
            shark = new (new Vector2(100, 100));
            player1 = new Player(new Vector2(200, 200));
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.Content.RootDirectory = "Content";

            idleAnimation = new Animation(Content.Load<Texture2D>("Idle-Left"), 0.1f, true);
            runAnimation = new Animation(Content.Load<Texture2D>("Run-LEFT"), 0.1f, true);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            //Texturer för player1
            player1.CurrentAnimation = idleAnimation;
            player1.RunAnimation = runAnimation;
            player1.IdleAnimation = idleAnimation;
            // Texturer för shark
            shark.TextureLeft[0] = Content.Load<Texture2D>("wackShark1_left");
            shark.TextureLeft[1] = Content.Load<Texture2D>("wackShark2_left");
            shark.TextureRight[0] = Content.Load<Texture2D>("wackShark1_right");
            shark.TextureRight[1] = Content.Load<Texture2D>("wackShark2_right");
            shark.Texture = shark.TextureLeft[0];

            player1.Texture = Content.Load<Texture2D>("Run-Right");
            
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
            isFacingRight = player1.MovePlayer(keyboardState,ground,groundPosition,groundPositionNext,groundNext);

            player1.CurrentAnimation.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            player1.DrawPlayer(_spriteBatch,player1);

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
            // Skapa gärna metoder för utritningen av objekt. 
            shark.DrawShark(_spriteBatch, shark, sharkFrameIndex);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}