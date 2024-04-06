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
        private int screenWidth;

        public Background(int startX, int screenWidth)
        {
            this.posX = startX;
            this.screenWidth = screenWidth; // Keep track of the screen width
        }

        public void LoadContent(ContentManager Content)
        {
            // Load background textures
            this.Background1 = Content.Load<Texture2D>("Bakgrund1");
            this.Background2 = Content.Load<Texture2D>("Bakgrund2");
            this.Background3 = Content.Load<Texture2D>("Bakgrund3");
            this.Background4 = Content.Load<Texture2D>("Bakgrund4");
        }

        public void Update(GameTime gameTime, bool isPlayerMoving, int playerSpeed = 1, int playerDirection = 1)
        {
            this.isPlayerMoving = isPlayerMoving;
            this.playerSpeed = playerSpeed;

            if (this.isPlayerMoving)
            {
                // Assuming playerDirection is 1 for right and -1 for left
                int backgroundMovement = playerSpeed * playerDirection;

                // Move the background in relation to the player's speed and direction
                this.posX -= backgroundMovement;
            }

            // Loop the background
            if (this.posX <= -screenWidth)
            {
                this.posX = 0; 
            }
            else if (this.posX >= screenWidth)
            {
                this.posX = 0; 
            }

        }


        public void DrawBackground(SpriteBatch spriteBatch, int mapWidth, int mapHeight)
        {
            Vector2 desiredSize = new Vector2(mapWidth, mapHeight);
            Rectangle destinationRectangle = new Rectangle(posX, posY, (int)desiredSize.X, (int)desiredSize.Y);
            Rectangle secondaryRectangle = new Rectangle(posX + screenWidth, posY, (int)desiredSize.X, (int)desiredSize.Y); // Offset by screen width
            Rectangle leftRectangle = new Rectangle(posX - screenWidth, posY, (int)desiredSize.X, (int)desiredSize.Y);

            // Draw each background twice for looping
            spriteBatch.Draw(Background1, destinationRectangle, Color.White);
            spriteBatch.Draw(Background1, secondaryRectangle, Color.White);
            spriteBatch.Draw(Background1, leftRectangle, Color.White);

            spriteBatch.Draw(Background2, destinationRectangle, Color.White);
            spriteBatch.Draw(Background2, secondaryRectangle, Color.White);
            spriteBatch.Draw(Background2, leftRectangle, Color.White);

            spriteBatch.Draw(Background3, destinationRectangle, Color.White);
            spriteBatch.Draw(Background3, secondaryRectangle, Color.White);
            spriteBatch.Draw(Background3, leftRectangle, Color.White);

            spriteBatch.Draw(Background4, destinationRectangle, Color.White);
            spriteBatch.Draw(Background4, secondaryRectangle, Color.White);
            spriteBatch.Draw(Background4, leftRectangle, Color.White);
        }

        // Properties
        public Texture2D Background1 { get; set; }
        public Texture2D Background2 { get; set; }
        public Texture2D Background3 { get; set; }
        public Texture2D Background4 { get; set; }
    }
}
