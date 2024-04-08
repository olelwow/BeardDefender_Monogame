using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeardDefender_Monogame
{
    internal class Animation
    {
        Texture2D texture;
        float frameTime;

        float elapsedFrameTime;
        int currentFrameIndex;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public int FrameCount
        {
            get { return Texture.Width / FrameHeight; }
        }

        public int FrameWidth
        {
            get { return FrameHeight; }
        }

        public int FrameHeight
        {
            get { return Texture.Height; }
        }

        public Animation(Texture2D texture, float frameTime, bool isLooping)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.elapsedFrameTime = 0f;
            this.currentFrameIndex = 0;
        }

        public void Update(GameTime gameTime)
        {
            elapsedFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedFrameTime >= frameTime)
            {
                elapsedFrameTime -= frameTime;
                currentFrameIndex = (currentFrameIndex + 1) % FrameCount;
            }
        }

        public Rectangle CurrentFrameSourceRectangle()
        {
            return new Rectangle(FrameWidth * currentFrameIndex, 0, FrameWidth, FrameHeight);
        }
    }
}