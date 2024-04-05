using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BeardDefender_Monogame.GameObjects.Powerups
{
    internal abstract class PowerUp
    {
        private bool addHp;
        private bool increaseJumpHeight;
        private Texture2D texture;
        private Texture2D[] textureAnimations;
        private Rectangle position;
        private int currentFrameIndex; // Variabel som väljer vilken frame man tar ur arrayen.
        private float frameDuration = 0.2f; // Hastighet på animationen.
        private float frameTimer = 0f;
        private bool taken = false;

        public abstract void Use(Player player);
        public abstract void LoadContent(ContentManager Content);
        public void Update(GameTime gameTime, Player player)
        {

            this.Texture = this.TextureAnimations[this.CurrentFrameIndex];
            // Uppdatera frameTimer
            this.FrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.FrameTimer >= this.FrameDuration)
            {
                // Nästa frame
                this.CurrentFrameIndex = (this.CurrentFrameIndex + 1) % this.TextureAnimations.Length;

                // Reset timer.
                this.FrameTimer = 0f;
            }
            Rectangle playerRect = GameMechanics.ConvertRectangleFToRectangle(player.Position);

            if (playerRect.Intersects(this.Position) && !this.Taken)
            {
                this.Use(player);
            }
        }
        public abstract void Draw(SpriteBatch _spriteBatch);

        // Get/Set
        public bool Taken
        {
            get { return this.taken; }
            set { this.taken = value; }
        }
        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set { currentFrameIndex = value; }
        }
        public float FrameDuration
        {
            get { return frameDuration; }
            set { frameDuration = value; }
        }
        public float FrameTimer
        {
            get { return frameTimer; }
            set { frameTimer = value; }
        }
        public bool AddHp
        {
            get { return addHp; }
            set { addHp = value; }
        }
        public bool IncreaseJumpHeight
        {
            get { return increaseJumpHeight; }
            set { increaseJumpHeight = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Texture2D[] TextureAnimations
        {
            get { return textureAnimations; }
            set { textureAnimations = value; }
        }
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
