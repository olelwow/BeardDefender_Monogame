using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Crabman : Enemy
    {
        //RectangleF position = new RectangleF();
        private Texture2D currentTexture;
        private Texture2D[] textureArray; // Innehåller texturer för rörelse till höger.
        float frameDuration = 0.2f; // Hastighet på animationen.
        float frameTimer = 0f;
        int currentFrameIndex; // Variabel som väljer vilken frame man tar ur arrayen.
        private int positionX = 40;
        private int positionY = 586;

        public Crabman()
        {
            // Array MÅSTE intieras innan man försöker lägga in textures i den.
            this.textureArray = new Texture2D[3];
        }



        public void LoadContent(ContentManager Content)
        {
            this.TextureArray[0] = Content.Load<Texture2D>("Crabman1");
            this.TextureArray[1] = Content.Load<Texture2D>("Crabman2");
            this.TextureArray[2] = Content.Load<Texture2D>("Crabman3");
            this.CurrentTexture = this.TextureArray[0];
        }

        public override int Update(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
            // Uppdatera frameTimer
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= frameDuration)
            {
                // Nästa frame
                currentFrameIndex = (currentFrameIndex + 1) % TextureArray.Length;

                // Reset timer.
                frameTimer = 0f;
            }
            return currentFrameIndex;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                    this.textureArray[currentFrameIndex],
                    new Vector2(
                        positionX, //position.X
                        positionY), //position.Y

                    null,
                    Color.White,
                    0f,
                    new Vector2(
                        this.CurrentTexture.Width / 2,
                        this.CurrentTexture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
        }


        public Texture2D[] TextureArray
        {
            get { return textureArray; }
            set { textureArray = value; }
        }

        public Texture2D CurrentTexture
        {
            get { return currentTexture; }
            set { currentTexture = value; }
        }

        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set { currentFrameIndex = value; }
        }

        public int PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }

        public int PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }
    }
}
