using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using BeardDefender_Monogame.GameLevels;
using System.Collections.Generic;
using BeardDefender_Monogame.GameObjects.Powerups;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Shark : Enemy
    {
        private float speed = 150f;
        private Vector2 position;
        private Texture2D texture; // Den textur som visas
        private Texture2D[] textureLeft; // Innehåller texturer för rörelse till vänster.
        private Texture2D[] textureRight; // Innehåller texturer för rörelse till höger.
        private bool sharkIsLeft = false;
        int currentFrameIndex; // Variabel som väljer vilken frame man tar ur arrayen.
        float frameDuration = 0.2f; // Hastighet på animationen.
        float frameTimer = 0f;
        private bool drawShark;

        public Shark(Vector2 position)
        {
            this.position = position;

            // Array MÅSTE intieras innan man försöker lägga in textures i den.
            textureLeft = new Texture2D[2];
            textureRight = new Texture2D[2];
            drawShark = true;
        }

        // Laddar in bilder till Shark objektet.
        public void LoadContent (ContentManager Content)
        {
            this.textureLeft[0] = Content.Load<Texture2D>("wackShark1_left");
            this.TextureLeft[1] = Content.Load<Texture2D>("wackShark2_left");
            this.TextureRight[0] = Content.Load<Texture2D>("wackShark1_right");
            this.TextureRight[1] = Content.Load<Texture2D>("wackShark2_right");
            this.Texture = this.TextureLeft[0];
        }
        // Denna metod räknar ut vilken frame man är på, och sköter hajens movement mellan vissa punkter.
        // Returnerar en int som representerar frame index.
        public override int Update(
            GraphicsDeviceManager _graphics,
            GameTime gameTime,
            Player player,
            Game1 game,
            string filePath,
            List<PowerUp> powerUpList,
            List<Shark> sharkList,
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
            if (!sharkIsLeft && position.X <= _graphics.PreferredBackBufferWidth - texture.Width / 2)
            {
                // Flyttar shark
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Kontrollerar om shark har kommit till vänstra brytpunkten.
                if (position.X <= texture.Width / 2 + 5)
                {
                    // Sätter shark position så den inte åker utanför skärmen, samt sätter sharkIsLeft till true
                    // vilket gör så att den yttre if-satsen blir falsk, och nästa if-sats blir sann.
                    position.X = texture.Width / 2;
                    sharkIsLeft = true;
                }
            }
            if (sharkIsLeft && position.X >= texture.Width / 2)
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (position.X >= _graphics.PreferredBackBufferWidth - texture.Width / 2 - 5)
                {
                    position.X = _graphics.PreferredBackBufferWidth - texture.Width / 2;
                    sharkIsLeft = false;
                }
            }
            RectangleF sharkPos = new RectangleF(this.Position.X, this.Position.Y, this.Texture.Height, this.Texture.Width);

            if (player.Position.IntersectsWith(sharkPos))
            {
                player.HP--;
                drawShark = false;
                sharkPos = new RectangleF(100, 100, 100, 100);
                this.position.X = 0;
                this.position.Y = 0;
            }
            if (player.HP < 1)
            {
                GameLevel.ResetGame(game, gameTime, filePath, player, healthCounter, powerUpList, sharkList);
            }
            return currentFrameIndex;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.SharkIsLeft)
            {
                spriteBatch.Draw(
                    this.TextureLeft[CurrentFrameIndex],
                    this.Position,
                    null,
                    Color.White,
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
                    Color.White,
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
        public bool DrawShark
        {
            get { return drawShark; }
            set { drawShark = value; }
        }
        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set { currentFrameIndex = value; }
        }
        public bool SharkIsLeft
        {
            get { return sharkIsLeft; }
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
            set { position.X = value.X;
                  position.Y = value.Y; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}
