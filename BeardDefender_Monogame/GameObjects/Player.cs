using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Player
    {
        //private RectangleF positionNew;
        //private bool jumping;
        //private bool isOnBlock;
        //private int jumpHeight;
        //private float speed = 4.05f;
        //private float jumpingSpeed;

        private Hedgehog hedgehog;

        public Vector2 position;
        public Vector2 velocity;
        private bool isFacingRight;
        private Texture2D texture;
        private Animation currentAnimation;
        private Animation idleAnimation;
        private Animation runAnimation;
        private Animation jumpAnimation;
        private SpriteBatch spriteBatch;
        private bool isOnGround;

        private const float MoveAcceleration = 1000.0f; // Minskad för långsammare acceleration
        private const float MaxMoveSpeed = 200.0f; // Minskad för lägre maxhastighet
        private const float GroundDragFactor = 0.58f;
        private const float AirDragFactor = 0.65f;
        private const float MaxJumpTime = 0.25f; // Justera för att påverka hur länge spelaren kan påverka hoppet uppåt
        private const float JumpLaunchVelocity = -1000.0f; // Högre värde för högre hopp
        private const float GravityAcceleration = 1500.0f; // Öka för snabbare fall, minska för långsammare
        private const float MaxFallSpeed = 450.0f; // Justera max fallhastighet
        private const float JumpControlPower = 0.14f; // Justera för att påverka spelarens kontroll under hoppet

        float jumpTime;
        bool isJumping;

        public Player(Vector2 position, SpriteBatch spriteBatch)
        {
            this.position = position;
            this.spriteBatch = spriteBatch;
            isFacingRight = true;
        }

        public void LoadContent(ContentManager Content)
        {
            idleAnimation = new Animation(Content.Load<Texture2D>("Idle-Left"), 0.1f, true);
            runAnimation = new Animation(Content.Load<Texture2D>("Run-LEFT"), 0.1f, true);
            jumpAnimation = new Animation(Content.Load<Texture2D>("Jump-Left"), 0.1f, false);
            texture = Content.Load<Texture2D>("Run-Right"); // Byt ut mot din spelartextur

            currentAnimation = idleAnimation;


        }

        public void MovePlayer(GameTime gameTime, KeyboardState keyboardState, List<Ground> groundList)
        {
            // Hantera inmatning från användaren
            GetInput(keyboardState);

            // Tillämpa rörelse, gravitation och hopplogik
            ApplyPhysics(gameTime);

            // Hantera kollisioner med marken för att uppdatera `isOnGround` och justera `position` vid behov
            HandleCollisions(groundList);

            // Uppdatera spelarens animation baserat på dess tillstånd (springer, står still, hoppar)
            UpdateAnimations(gameTime);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, List<Ground> groundList)
        {
            GetInput(keyboardState);
            ApplyPhysics(gameTime);
            HandleCollisions(groundList);
            UpdateAnimations(gameTime);
            currentAnimation.Update(gameTime);
        }

        private void GetInput(KeyboardState keyboardState)
        {
            isJumping = keyboardState.IsKeyDown(Keys.Space);

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                isFacingRight = false;
                velocity.X = -MoveAcceleration;
            }
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                isFacingRight = true;
                velocity.X = MoveAcceleration;
            }
            else
            {
                velocity.X = 0;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && isOnGround)
            {
                isOnGround = false;
                isJumping = true;
                jumpTime = 0.0f;
            }
            else if (keyboardState.IsKeyUp(Keys.Space))
            {
                // Kontrollera hoppet genom att släppa hoppknappen
                isJumping = false;
            }
        }

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

            position += velocity * elapsed;
            position = new Vector2((float)Math.Round(position.X), (float)Math.Round(position.Y));

            // Återställ hoppet när spelaren landar på marken
            if (position.Y > 720) 
            {
                position.Y = 720;
                isOnGround = true;
                isJumping = false;
                velocity.Y = 0;
            }
        }

        private void HandleCollisions(List<Ground> groundList)
        {
            isOnGround = false; 
            foreach (var ground in groundList)
            {
                if (position.Y + texture.Height > ground.Position.Y &&
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
                        position.Y = groundTop - texture.Height;
                    }
                }
            }
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            if (!isOnGround)
            {
                currentAnimation = jumpAnimation;
            }
            else if (/*Math.Abs(velocity.X) > 0*/Math.Abs(velocity.X) - 0.02f > 0)
            {
                currentAnimation = runAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

            if (isJumping)
            {
                currentAnimation = jumpAnimation;
            }
            else if (Math.Abs(velocity.X) > 0)
            {
                currentAnimation = runAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

            currentAnimation.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (spriteBatch == null) throw new InvalidOperationException("SpriteBatch has not been initialized.");

            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrameSourceRectangle(), Color.White, 0f, Vector2.Zero, 1f, isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
        }
    }
}