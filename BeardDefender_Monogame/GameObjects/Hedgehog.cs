using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Hedgehog : Enemy
    {
        public Vector2 position;
        private Texture2D texture;
        private float speed = 150f;
        private float detectionRange = 2000f; // Adjust this value to change the detection range
        private float scale;
        //private Vector2 lastPlayerPosition = Vector2.Zero;

        public Hedgehog(Vector2 position, Texture2D texture, float scale)
        {
            this.position = position;
            this.texture = texture;
            this.scale = scale;
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Calculate direction towards the player
            Vector2 direction = Vector2.Normalize(playerPosition - position);

            // Update position towards the player
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Calculate the destination rectangle based on the position and scaled size
            Rectangle destinationRect = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));

            spriteBatch.Draw(texture, destinationRect, Color.LightSkyBlue);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}