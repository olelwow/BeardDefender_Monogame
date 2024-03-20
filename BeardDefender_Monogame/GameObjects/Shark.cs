using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BeardDefender_Monogame.GameObjects
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

        public Shark(Vector2 position)
        {
            this.position = position;

            // Array MÅSTE intieras innan man försöker lägga in textures i den.
            textureLeft = new Texture2D[2];
            textureRight = new Texture2D[2];
        }

        // Denna metod räknar ut vilken frame man är på, och sköter hajens movement mellan vissa punkter.
        // Returnerar en int som representerar frame index.
        public int MoveShark(GraphicsDeviceManager _graphics, GameTime gameTime)
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
            return currentFrameIndex;
        }

        public void DrawShark(SpriteBatch _spriteBatch, Shark shark, int sharkFrameIndex)
        {
            if (!shark.SharkIsLeft)
            {
                _spriteBatch.Draw(
                    shark.TextureLeft[sharkFrameIndex],
                    shark.Position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(
                        shark.Texture.Width / 2,
                        shark.Texture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
            }
            else
            {
                _spriteBatch.Draw(
                shark.TextureRight[sharkFrameIndex],
                shark.Position,
                null,
                Color.White,
                0f,
                new Vector2(
                    shark.Texture.Width / 2,
                    shark.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            }
        }

        // Getters och Setters
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
            set { position = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}
