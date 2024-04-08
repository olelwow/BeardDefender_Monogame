using BeardDefender_Monogame.GameObjects.Powerups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Player
    {
        public RectangleF position;
        private RectangleF velocity;
        private Texture2D texture;
        private bool isFacingRight;
        private Animation currentAnimation;
        private Animation idleAnimation;
        private Animation runAnimation;
        private int hP = 1;

        private Animation jumpAnimation;
        //private SpriteBatch spriteBatch;
        private bool isOnGround;

        private const float MoveAcceleration = 1000.0f; // Minskad för långsammare acceleration
        private const float MaxMoveSpeed = 200.0f; // Minskad för lägre maxhastighet
        private const float GroundDragFactor = 0.58f;
        private const float AirDragFactor = 0.65f;
        private float maxJumpTime = 0.25f; // Justera för att påverka hur länge spelaren kan påverka hoppet uppåt
        private const float JumpLaunchVelocity = -1000.0f; // Högre värde för högre hopp
        private const float GravityAcceleration = 1500.0f; // Öka för snabbare fall, minska för långsammare
        private const float MaxFallSpeed = 550.0f; // Justera max fallhastighet
        private const float JumpControlPower = 0.14f; // Justera för att påverka spelarens kontroll under hoppet

        float jumpTime;
        bool isJumping;

        public Player(RectangleF position)
        {
            this.position = position;
            this.velocity = position;
        }

        public void LoadContent(ContentManager Content)
        {
            this.idleAnimation = new Animation(Content.Load<Texture2D>("Idle-Left"), 0.1f, true);
            this.runAnimation = new Animation(Content.Load<Texture2D>("Run-LEFT"), 0.1f, true);
            this.jumpAnimation = new Animation(Content.Load<Texture2D>("Jump-Left"), 0.1f, true);
            this.texture = Content.Load<Texture2D>("Run-Right");
            this.currentAnimation = runAnimation;
        }

        public bool MovePlayer(
            KeyboardState keyboardState,
            GameTime gameTime,
            Hedgehog hedgehog,
            List<Ground> groundList,
            PowerUp jumpBoosts
            )
        {
            Update(keyboardState, gameTime, groundList);


            if (keyboardState.IsKeyDown(Keys.W) ||
                keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.S) ||
                keyboardState.IsKeyDown(Keys.Down) ||
                keyboardState.IsKeyDown(Keys.A) ||
                keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.D) ||
                keyboardState.IsKeyDown(Keys.Right))
            {
                currentAnimation = runAnimation;
            }
            else if (keyboardState.IsKeyDown(Keys.Space))
            {
                currentAnimation = jumpAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
                this.position.X -= 1;
            }
            return isFacingRight;
        }

        public void Update(KeyboardState keyBoardstate, GameTime gameTime, List<Ground> groundList)
        {
            GetInput(keyBoardstate);
            ApplyPhysics(gameTime);
            HandleCollisions(groundList);
        }

        //Metod som kontrollerar om det finns kollision med ground/marken
        private void HandleCollisions(List<Ground> groundList)
        {
            isOnGround = false;
            foreach (var ground in groundList)
            {
                if (position.Y + texture.Height > ground.Position.Y
                    &&
                    position.X < ground.Position.X + ground.Position.Width &&
                    position.X + texture.Width > ground.Position.X &&
                    position.Y < ground.Position.Y + ground.Position.Height)
                {
                    float groundTop = ground.Position.Y;
                    float playerBottom = position.Y + texture.Height;
                    if (playerBottom <= groundTop + velocity.Y)
                    {
                        isOnGround = true;
                        velocity.Y = 0;
                        position.Y = groundTop - (texture.Height / 4);
                    }
                }
            }
        }

        //Metod som lägger fysik till spellarens rörelse
        private void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isJumping)
            {
                // Om spelaren fortfarande håller i hoppknappen men inte har hoppat för länge, fortsätt att ge hoppkraft uppåt
                if (jumpTime > 0.0f)
                {
                    velocity.Y += JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                }
                jumpTime += elapsed;
            }

            velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);
            velocity.X *= (isOnGround ? GroundDragFactor : AirDragFactor);

            //Update positionen X och Y...
            position.Y += velocity.Y * elapsed;
            position.X += velocity.X * elapsed;
            //...och lagra den till nya positionen
            position = new RectangleF((float)Math.Round(position.X), (float)Math.Round(position.Y), position.Width, position.Height);

            // Återställ hoppet när spelaren landar på marken
            if (position.Y > 720)
            {
                position.Y = 720;
                isOnGround = true;
                isJumping = false;
                velocity.Y = 0;
            }
        }

        //Metod som registrerar tanget input för spelarens rörelse
        private void GetInput(KeyboardState keyboardState)
        {
            isJumping = keyboardState.IsKeyDown(Keys.Space);

            //If-satsen som påverkar rörelse väster/höger
            if (keyboardState.IsKeyDown(Keys.A)
                || keyboardState.IsKeyDown(Keys.Left))
            {
                IsFacingRight = true;
                velocity.X = -MoveAcceleration;
            }
            else if (keyboardState.IsKeyDown(Keys.D)
                || keyboardState.IsKeyDown(Keys.Right))
            {
                isFacingRight = false;
                velocity.X = MoveAcceleration;
            }
            else
            {
                velocity.X = 0;
            }

            //If-satsen som påverkar jump
            if (keyboardState.IsKeyDown(Keys.Space)
                && isOnGround)
            {
                isOnGround = false;
                isJumping = true;
                jumpTime = 0.0f;
            }
            else if (keyboardState.IsKeyUp(Keys.Space))
            {
                isJumping = false;
            }

        }

        public void DrawPlayer(SpriteBatch _spriteBatch)
        {
            SpriteEffects spriteEffects = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            //Vector2 scale = new Vector2(20f, 20f);

            _spriteBatch.Draw(
                texture: this.CurrentAnimation.Texture,
                position: new Vector2(
                    this.Position.X,
                    this.Position.Y),
                sourceRectangle: this.CurrentAnimation.CurrentFrameSourceRectangle(),
                color: Microsoft.Xna.Framework.Color.White,
                rotation: 0f,
                origin: new Vector2(
                    this.CurrentAnimation.FrameWidth / 2,
                    this.CurrentAnimation.FrameHeight / 2),
                scale: Vector2.One,
                effects: spriteEffects,
                layerDepth: 0f
            );
        }

        // Get/Set
        public float MaxJumpTime
        {
            get { return maxJumpTime; }
            set { maxJumpTime = value; }
        }
        public int HP
        {
            get { return hP; }
            set { hP = value; } 
        }
        public bool IsFacingRight
        {
            get { return isFacingRight; }
            set { isFacingRight = value; }
        }
        public RectangleF PositionNew
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public RectangleF Position
        {
            get { return position; }
            set
            {
                position.X = value.X;
                position.Y = value.Y;
                position.Width = value.Width;
                position.Height = value.Height;
            }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Animation CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public Animation IdleAnimation
        {
            get { return idleAnimation; }
            set { idleAnimation = value; }
        }

        public Animation RunAnimation
        {
            get { return runAnimation; }
            set { runAnimation = value; }
        }

    }
}
