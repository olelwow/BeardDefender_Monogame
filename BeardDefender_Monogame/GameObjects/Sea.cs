using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Sea
    {
        private Texture2D texture;
        private Vector2 position;
        private Texture2D[] waves;
        private int currentFrameIndex;
        float frameDuration = 0.2f; // Hastighet på animationen.
        float frameTimer = 0f;

        public Sea(Vector2 position)
        {
            this.position = position;
            this.waves = new Texture2D[4];
        }

        public void LoadContent(ContentManager Content)
        {
            this.waves[0] = Content.Load<Texture2D>("sea_tile1");
            this.waves[1] = Content.Load<Texture2D>("sea_tile2");
            this.waves[2] = Content.Load<Texture2D>("sea_tile3");
            this.waves[3] = Content.Load<Texture2D>("sea_tile4");
        }

        public int Update(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= frameDuration)
            {
                currentFrameIndex = (currentFrameIndex + 1) % waves.Length;
                frameTimer = 0f;
            }
            return currentFrameIndex;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
                waves[currentFrameIndex],
                new Vector2(position.X, position.Y),
                Microsoft.Xna.Framework.Color.White);
        }



        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set { currentFrameIndex = value; }
        }
    }
}
