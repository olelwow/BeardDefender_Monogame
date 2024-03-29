﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Player
    {
        public Rectangle position;
        private Rectangle positionNew;
        private bool jumping;
        private int jumpHeight;
        private int speed = 5;
        private Texture2D texture;
        private bool isFacingRight;
        private Animation currentAnimation;
        private Animation idleAnimation;
        private Animation runAnimation;

        public Player(Rectangle position)
        {
            this.position = position;
            this.positionNew = position;
        }
        
        public void LoadContent (ContentManager Content)
        {
            this.idleAnimation = new Animation(Content.Load<Texture2D>("Idle-Left"), 0.1f, true);
            this.runAnimation = new Animation(Content.Load<Texture2D>("Run-LEFT"), 0.1f, true);
            this.texture = Content.Load<Texture2D>("Run-Right");
            this.currentAnimation = runAnimation;
        }

        public bool MovePlayer(
            KeyboardState keyboardState, 
            List<Ground> upperGroundList,
            List<Ground> lowerGroundList)
        {
            GameMechanics.ApplyGravity(lowerGroundList, this);

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                GameMechanics.CheckForCollisionsLeft(upperGroundList, keyboardState, this);
                isFacingRight = true;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                GameMechanics.CheckForCollisionsRight(upperGroundList, keyboardState, this);
                isFacingRight = false;
            }
            //if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            //{
            //    position.Y += 0;
            //}
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (!jumping)
                {
                    jumping = true;
                    jumpHeight = 100;
                }
            }

            if (jumping)
            {
                Jump();
            }

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
            else
            {
                currentAnimation = idleAnimation;
            }
            return isFacingRight;
        }

        public void Jump ()
        {
            if (jumping)
            {
                speed = 3;
                if (jumpHeight > -5)
                {
                    // Minskar spelarens Y värde med värdet på jumpHeight,
                    // minskar värdet på jumpHeight med -2 vid varje check.
                    // Spelaren flyttas snabbt upp till högsta punkten i nuvarande kod,
                    // får kollas på senare... Jag trött.
                    position.Y -= jumpHeight; 
                    jumpHeight -= 2; 
                }
                else 
                {
                    jumpHeight = 0;
                    jumping = false;
                    speed = 5;
                }
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
                color: Color.White,
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
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool Jumping
        {
            get { return this.jumping; }
            set { this.jumping = value; }
        }
        public bool IsFacingRight
        {
            get { return isFacingRight; }
            set { isFacingRight = value; }
        }
        public Rectangle PositionNew
        {
            get { return positionNew; }
            set { positionNew = value; }
        }
        public Rectangle Position
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
