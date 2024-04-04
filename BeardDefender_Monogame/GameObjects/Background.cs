using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Background
    {
        private Texture2D background1;
        private Texture2D background2;
        private Texture2D background3;
        private Texture2D background4;
        private int posX;
        private int posY;
        private bool isPlayerMoving; 
        private int playerSpeed; 

        public Background(int startX)
        {
            this.posX = startX;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Background1 = Content.Load<Texture2D>("Bakgrund1");
            this.Background2 = Content.Load<Texture2D>("Bakgrund2");
            this.Background3 = Content.Load<Texture2D>("Bakgrund3");
            this.Background4 = Content.Load<Texture2D>("Bakgrund4");
        }

        public void Update(GameTime gameTime, int screenWidth, bool isPlayerMoving, int playerSpeed = 0)
        {
            this.isPlayerMoving = isPlayerMoving;
            this.playerSpeed = playerSpeed;

            if (this.isPlayerMoving)
            {
                // Adjust the scrolling speed based on player speed or a fixed value
                int scrollSpeedAdjustment = Math.Max(1, playerSpeed); 
                this.posX -= scrollSpeedAdjustment;
            }

            // Check if the backgrounds have scrolled beyond the screen width
            if (this.posX <= -screenWidth)
            {
                // Reset the position to the right side of the screen
                this.posX = screenWidth; 
            }
        }

        public void DrawBackground(SpriteBatch _spriteBatch, int MapWidth, int MapHeight)
        {
            Vector2 desiredSize = new Vector2(MapWidth, MapHeight);
            Rectangle destinationRectangle = new Rectangle(posX, posY, (int)desiredSize.X, (int)desiredSize.Y);

            _spriteBatch.Draw(Background1, destinationRectangle, Color.White);
            _spriteBatch.Draw(Background2, destinationRectangle, Color.White);
            _spriteBatch.Draw(Background3, destinationRectangle, Color.White);
            _spriteBatch.Draw(Background4, destinationRectangle, Color.White);
        }

        // Get/Set
        public Texture2D Background1 { get; set; }
        public Texture2D Background2 { get; set; }
        public Texture2D Background3 { get; set; }
        public Texture2D Background4 { get; set; }
    }
}
