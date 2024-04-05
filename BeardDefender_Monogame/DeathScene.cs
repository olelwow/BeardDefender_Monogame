using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardDefender_Monogame
{
    internal class DeathScene
    {
        //Olika layers för att skapa bakgrunden i spelet.
        private Texture2D deathSceneBackground;
        private Texture2D currentTexture;
        private Texture2D[] textureArray;
        float frameDuration = 0.2f; // Hastighet på animationen.
        float frameTimer = 0f;
        int currentFrameIndex; // Variabel som väljer vilken frame man tar ur arrayen.
        private int positionX = 1320/2;
        private int positionY = 720/2;


        public DeathScene() 
        {
            this.textureArray = new Texture2D[4];
        }

        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.

            //this.deathSceneBackground = Content.Load<Texture2D>("highscore_screen");
            this.TextureArray[0] = Content.Load<Texture2D>("CrabmandDeath1");
            this.TextureArray[1] = Content.Load<Texture2D>("CrabmandDeath2");
            this.TextureArray[2] = Content.Load<Texture2D>("CrabmandDeath3");
            this.TextureArray[3] = Content.Load<Texture2D>("CrabmandDeath4");
            this.CurrentTexture = this.TextureArray[0];

        }

        public int Update(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
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

        public void DrawBackground(SpriteBatch _spriteBatch, int MapWidth, int MapHeight, double score)
        {
            //Skapar en rektangel i storlek med spelrutan och förstorar bakgrundsbilderna-
            //till samma storlek som spelytan.
            Vector2 desiredSize = new Vector2(MapWidth, MapHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, (int)desiredSize.X, (int)desiredSize.Y);
            //Vector2 scale = new Vector2(20f, 20f);

            //_spriteBatch.Draw(deathSceneBackground, destinationRectangle, Color.White);

            _spriteBatch.Draw(
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

        // Get/Set
       

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

