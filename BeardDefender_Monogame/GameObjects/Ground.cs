using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text.RegularExpressions;

namespace BeardDefender_Monogame.GameObjects
{
    internal class Ground
    {
        Texture2D texture;
        Rectangle position;

        public Ground(Rectangle position) 
        {
            this.position = position;
        }

        public void LoadContent (ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>("ground 10tiles");
        }
        public void Draw (SpriteBatch _spriteBatch) 
        {
            _spriteBatch.Draw(
                this.texture,
                this.position,
                Color.White);
        }

        // Get/Set
        public Rectangle Position
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
