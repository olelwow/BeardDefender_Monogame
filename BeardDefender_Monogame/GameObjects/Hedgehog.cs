using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BeardDefender_Monogame.GameLevels;
using BeardDefender_Monogame.GameObjects.Powerups;
using System.Collections.Generic;
using System.Drawing;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Hedgehog : Enemy
    {
        private float speed = 50f;
        public Vector2 position;
        private Texture2D texture;
        private Texture2D[] textureLeft;
        private Texture2D[] textureRight;
        private bool hedghogIsLeft;
        int currentFrameIndex; // Variabel som väljer vilken frame man tar ur arrayen.
        float frameDuration = 0.2f; // Hastighet på animationen.
        float frameTimer = 0f;
        private bool drawHedgehog;
        public Hedgehog(Vector2 position)
        {
            this.position = position;
            textureLeft = new Texture2D[2];
            textureRight = new Texture2D[2];
            drawHedgehog = true;
        }

        public void LoadContent(ContentManager contentManager)
        {
            this.textureLeft[0] = contentManager.Load<Texture2D>("hedgehog1");
            this.textureLeft[1] = contentManager.Load<Texture2D>("hedgehog2");
            this.textureRight[0] = contentManager.Load<Texture2D>("hedgehog3");
            this.textureRight[1] = contentManager.Load<Texture2D>("hedgehog4");
            this.texture = this.textureLeft[0];
        }

        public override int Update(
            GraphicsDeviceManager _graphics,
            GameTime gameTime,
            Player player,
            Game1 game,
            string filePath,
            List<PowerUp> powerUpList,
            List<Shark> sharkList,
            Hedgehog hedgehog,
            HealthCounter healthCounter)
        {
            // Uppdatera frameTimer
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= frameDuration)
            {
                // Nästa frame
                currentFrameIndex = (currentFrameIndex + 1) % textureLeft.Length;

                // Reset timer.
                frameTimer = 0f;
            }

            // Kollar om shark inte är till vänster, och på rätt position av skärmen. När shark har kommit
            // hela vägen till vänster ändras sharkIsLeft till true och då blir nästa if-condition uppfyllt.
            if (!hedghogIsLeft && position.X <= _graphics.PreferredBackBufferWidth - texture.Width / 2)
            {
                // Flyttar shark
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Kontrollerar om shark har kommit till vänstra brytpunkten.
                if (position.X <= texture.Width / 2 + 5)
                {
                    // Sätter shark position så den inte åker utanför skärmen, samt sätter sharkIsLeft till true
                    // vilket gör så att den yttre if-satsen blir falsk, och nästa if-sats blir sann.
                    position.X = texture.Width / 2;
                    hedghogIsLeft = true;
                }
            }
            if (hedghogIsLeft && position.X >= texture.Width / 2)
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (position.X >= _graphics.PreferredBackBufferWidth - texture.Width / 2 - 5)
                {
                    position.X = _graphics.PreferredBackBufferWidth - texture.Width / 2;
                    hedghogIsLeft = false;
                }
            }
            RectangleF hedgPos = new RectangleF(this.Position.X, this.Position.Y, this.Texture.Height, this.Texture.Width);

            if (player.Position.IntersectsWith(hedgPos))
            {
                player.HP--;
                drawHedgehog = false;
                this.position.X = 0;
                this.position.Y = 0;
            }
            if (player.HP < 1)
            {
                GameLevel.ResetGame(game, gameTime, filePath, player, healthCounter, powerUpList, sharkList, hedgehog);
            }
            return currentFrameIndex;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.hedghogIsLeft)
            {
                spriteBatch.Draw(
                    this.TextureLeft[CurrentFrameIndex],
                    this.Position,
                    null,
                    Microsoft.Xna.Framework.Color.White,
                    0f,
                    new Vector2(
                        this.Texture.Width / 2,
                        this.Texture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }
            else
            {
                spriteBatch.Draw(
                    this.TextureRight[CurrentFrameIndex],
                    this.Position,
                    null,
                    Microsoft.Xna.Framework.Color.White,
                    0f,
                    new Vector2(
                        this.Texture.Width / 2,
                        this.Texture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }

        }

        // Get/Set
        public bool DrawHedgehog
        {
            get { return drawHedgehog; }
            set { drawHedgehog = value; }
        }
        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set { currentFrameIndex = value; }
        }
        public bool HedgehogIsLeft
        {
            get { return hedghogIsLeft; }
            set { hedghogIsLeft = value; }
        }
        public Texture2D[] TextureLeft
        {
            get { return textureLeft; }
            set { textureLeft = value; }
        }
        public Texture2D[] TextureRight
        {
            get { return textureRight; }
            set { textureRight = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position.X = value.X;
                position.Y = value.Y;
            }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}