using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Player
    {
        public Rectangle position;
        private Rectangle positionNew;
        private bool jumping;
        private int jumpSpeed;
        private float gravity;
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
        public Rectangle PositionNew
        {
            get { return positionNew; } 
            set { positionNew = value; }
        }
        public Rectangle Position
        {
            get { return position; }
            set { 
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

        private void CheckForCollisionsRight(Rectangle ground, KeyboardState key)
        {
            if (this.position.X + this.position.Width <= ground.X
                && key.IsKeyDown(Keys.Right))
            {
                this.position.X += 5;
                this.positionNew = this.position;
            }
            else if (this.positionNew.X + this.position.Width == ground.X
                || this.positionNew.X + this.position.Width > ground.X)
            {
                this.position.X -= 3;
            }
        }
        private void CheckForCollisionsLeft(Rectangle ground, KeyboardState key)
        {
            if (this.position.X + this.position.Width <= ground.X
                && key.IsKeyDown(Keys.Left))
            {
                this.position.X -= 5;
                this.positionNew = this.position;
            }
            else if (this.positionNew.X + this.position.Width == ground.X
                || this.positionNew.X + this.position.Width > ground.X)
            {
                this.position.X -= 1;
            }
        }
        public bool MovePlayer(KeyboardState keyboardState, 
            Texture2D ground, 
            Rectangle groundPosition, 
            Rectangle groundPositionNext, 
            Texture2D groundNext,
            Rectangle groundPositionCon)
        {
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                CheckForCollisionsLeft(groundPositionCon, keyboardState);
                //position.X -= 5;
                isFacingRight = true;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                CheckForCollisionsRight(groundPositionCon, keyboardState);
                //position.X += 5;
                isFacingRight = false;
            }
            //if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            //{
            //    position.Y += 0;
            //}
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                jumping = true;
            }

            //position.Y += jumpSpeed;

            //if (jumping == true && gravity < 0)
            //{
            //    jumping = false;
            //}
            //if (jumping == true)
            //{
            //    jumpSpeed = -12; //pushing the player upwards
            //    gravity -= 1; //when this value results < 0 then the player goes downwards again
            //}
            //else
            //{
            //    jumpSpeed = 12;
            //}

            ////Resets the jump and the position of the player
            //if (position.Y > groundPositionNext.Y && jumping == false)
            //{
            //    gravity = 12;
            //    position.Y = 365;
            //    jumpSpeed = 0;
            //}

            //Check for collision
            if (position.X == groundPositionNext.X)
            {
                position.X = groundPositionNext.X;
            }

            if (position.Y - texture.Height < ground.Height)
            {
                position.Y = ground.Bounds.Top - texture.Height;
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

        public void DrawPlayer(SpriteBatch _spriteBatch, Player player1 )
        {
            SpriteEffects spriteEffects = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            //Vector2 scale = new Vector2(20f, 20f);

            _spriteBatch.Draw(
                texture: player1.CurrentAnimation.Texture,
                position: new Vector2(player1.Position.X, player1.Position.Y),
                sourceRectangle: player1.CurrentAnimation.CurrentFrameSourceRectangle(),
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(player1.CurrentAnimation.FrameWidth / 2, player1.CurrentAnimation.FrameHeight / 2),
                scale: Vector2.One,
                effects: spriteEffects,
                layerDepth: 0f
            );
        }

    }
}
