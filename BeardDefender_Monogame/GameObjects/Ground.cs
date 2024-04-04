using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Ground
    {
        Texture2D texture;
        RectangleF position;
        int posX;

        public Ground(RectangleF position) 
        {
            this.position = position;
            this.posX = (int)position.X;
        }

        public void LoadContent (ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>("ground 10tiles");
        }
        public void Update (GameTime gameTime, int screenWidth)
        {

            this.posX -= 1; // Justera tiden för scroll-hastigheten

            // Kollar ifall bakgrunden har scrollat förbi skärmens bredd.
            if (this.posX <= -screenWidth)
            {

                // Nollställer positionen till höger sida om skärmen
                this.posX = 1320;
            }
        }
        public void Draw (SpriteBatch _spriteBatch) 
        {
            _spriteBatch.Draw(
                this.texture,
                new Vector2 (this.posX, this.position.Y),
                Color.White);
        }

        // Get/Set
        public RectangleF Position
        {
            get { return position; }
            set
            {
                position.X = value.X;
                position.Y = value.Y;
                position.Width = value.Width;
                position.Height = value.Height;
            }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

    }
}
