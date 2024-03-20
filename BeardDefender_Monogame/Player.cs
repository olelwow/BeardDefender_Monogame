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

namespace BeardDefender_Monogame
{
    internal class Player
    {
        private Vector2 position;
        private bool jumping;
        private int jumpSpeed;
        private float gravity;
        private Texture2D texture;
        private bool isFacingRight;
        private Animation currentAnimation;
        private Animation idleAnimation;
        private Animation runAnimation;
       
        public Vector2 Positions
        {
            get { return position; }
            set { position = value; }
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

        public Player(Vector2 position)
        {
            this.position = position;

        }

        public bool MovePlayer(KeyboardState keyboardState, Texture2D ground, Vector2 groundPosition, Vector2 groundPositionNext, Texture2D groundNext)
        {
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= 5f;
                isFacingRight = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position.X += 5f;
                isFacingRight = false;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += 0f;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                jumping = true;
            }

            position.Y += jumpSpeed;

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
            if (position.Y > groundPositionNext.Y && jumping == false)
            {
                gravity = 12;
                position.Y = 365;
                jumpSpeed = 0;
            }

            //Check for collision
            if (position.X == groundPositionNext.X)
            {
                position.X = groundPositionNext.X;
            }

            if (position.Y - this.texture.Height < ground.Height)
            {
                position.Y = ground.Bounds.Top - this.texture.Height;
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

    }
}
