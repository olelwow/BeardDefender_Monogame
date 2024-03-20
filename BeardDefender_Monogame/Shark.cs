using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BeardDefender_Monogame
{
    internal class Shark
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

        public Shark (Vector2 position)
        {
            this.position = position;

            // Array MÅSTE intieras innan man försöker lägga in textures i den.
            this.textureLeft = new Texture2D[2];
            this.textureRight = new Texture2D[2];
        }

        // Denna metod räknar ut vilken frame man är på, och sköter hajens movement mellan vissa punkter.
        // Returnerar en int som representerar frame index.
        public int MoveShark (GraphicsDeviceManager _graphics, GameTime gameTime)
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
            if (!sharkIsLeft && this.position.X <= _graphics.PreferredBackBufferWidth - this.texture.Width / 2)
            {
                // Flyttar shark
                this.position.X -= this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Kontrollerar om shark har kommit till vänstra brytpunkten.
                if (this.position.X <= this.texture.Width / 2 + 5)
                {
                    // Sätter shark position så den inte åker utanför skärmen, samt sätter sharkIsLeft till true
                    // vilket gör så att den yttre if-satsen blir falsk, och nästa if-sats blir sann.
                    this.position.X = this.texture.Width / 2;
                    sharkIsLeft = true;
                }
            }
            if (sharkIsLeft && this.position.X >= this.texture.Width / 2)
            {
                this.position.X += this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (this.position.X >= _graphics.PreferredBackBufferWidth - this.texture.Width / 2 - 5)
                {
                    this.position.X = _graphics.PreferredBackBufferWidth - this.texture.Width / 2;
                    sharkIsLeft = false;
                }
            }
            return currentFrameIndex;
        }

        // Getters och Setters
        public bool SharkIsLeft
        {
            get { return this.sharkIsLeft; }
        }
        public Texture2D[] TextureLeft
        {
            get { return this.textureLeft; }
            set { this.textureLeft = value; }
        }
        public Texture2D[] TextureRight
        {
            get { return this.textureRight; }
            set { this.textureRight = value; }
        }
        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public float Speed 
        { 
            get { return this.speed; } 
            set { this.speed = value; } 
        }
    }
}
