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
        //private CollisionComponent _collisionComponent;
        const int MapWidth = 1320;
        const int MapHeight = 720;

        Hedgehog hedgehog;

        // Olle :*
        Shark shark;
        Player player1;
        int sharkFrameIndex;

        //Texture2D player;
        //Rectangle playerPositionPrevious;
        //Rectangle playerPositionNew;
        //Rectangle currentPosition;
        float playerSpeed;
        Vector2 playerPosition;

        //Texture2D player;

        Texture2D ground;
        Rectangle groundPosition;
        Texture2D groundCon;
        Rectangle groundPositionCon;
        Texture2D groundNext;
        Rectangle groundPositionNext;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_collisionComponent = new CollisionComponent(new RectangleF(0, 0, MapWidth, MapHeight));
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = MapHeight;
            _graphics.PreferredBackBufferWidth = MapWidth;
            _graphics.ApplyChanges();

            groundPosition = new Rectangle(0, _graphics.PreferredBackBufferHeight, 400, 80);
            groundPositionCon = new Rectangle();
            groundPositionNext = new Rectangle();
            shark = new (new Vector2(100, 100));
            player1 = new Player(new Rectangle(100, 400, 25, 64));

            hedgehog = new Hedgehog(new Vector2(100, 100), Content.Load<Texture2D>("Hedgehog_Right"), 0.03f);


            //_collisionComponent.Insert();   //Titta vidare

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
            groundPosition.Y = _graphics.PreferredBackBufferHeight - ground.Height;
            groundPositionCon = new Rectangle(
                    _graphics.PreferredBackBufferWidth / 4,
                    _graphics.PreferredBackBufferHeight - ground.Height * 2,
                    _graphics.PreferredBackBufferWidth - groundCon.Width,
                    groundCon.Height
                    );

            player1.position.Y = groundPosition.Y - (player1.Texture.Height / 4);

            var kstate = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Shark movement, returnerar rätt frame index som används i Update.
            sharkFrameIndex = shark.MoveShark(_graphics, gameTime);



            // Movement settings
            KeyboardState keyboardState = Keyboard.GetState();
            isFacingRight = player1.MovePlayer(keyboardState, ground, groundPosition, groundPositionNext, groundNext, groundPositionCon);

            player1.CurrentAnimation.Update(gameTime);

            //player1.DrawPlayer(_spriteBatch, player1);

            hedgehog.Update(gameTime, new Vector2(player1.position.X, player1.position.Y));

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void CheckForCollisions(Rectangle playerPositionPrevious, Rectangle playerPositionNew, Rectangle ground)
        {
            if (playerPositionPrevious.X + playerPositionPrevious.Width != ground.X)
            {
                playerPositionPrevious.X += 5;
                playerPositionNew = playerPositionPrevious;
            }
            else
            {
                playerPositionPrevious.X += 0 ;
            }
        }

        //public static Collisions()


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin();

            player1.DrawPlayer(_spriteBatch,player1);

            //Ground
            _spriteBatch.Draw(
                ground,
                groundPosition = new Rectangle(
                    0,
                    _graphics.PreferredBackBufferHeight - ground.Height,
                    _graphics.PreferredBackBufferWidth / 2,
                    ground.Height
                    ),
                Color.White);

            _spriteBatch.Draw(
                ground,
                groundPositionCon = new Rectangle(
                    _graphics.PreferredBackBufferWidth / 4,
                    _graphics.PreferredBackBufferHeight - ground.Height * 2,
                    _graphics.PreferredBackBufferWidth - groundCon.Width,
                    groundCon.Height
                    ),
                Color.White);

            _spriteBatch.Draw(
                ground,
                groundPositionNext = new Rectangle(
                    groundPosition.Right,
                    _graphics.PreferredBackBufferHeight - groundNext.Height,
                    ground.Width + 20,
                    groundNext.Height
                    ),
                Color.White);

            // SHAAAARK, beroende på värdet i SharkIsLeft så används rätt sprites.
            // Skapa gärna metoder för utritningen av objekt. 
            shark.DrawShark(_spriteBatch, shark, sharkFrameIndex);

            hedgehog.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}