using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BeardDefender_Monogame.GameObjects.Powerups
{
    internal class GemScore : PowerUp
    {
        public GemScore(Rectangle position)
        {
            this.TextureAnimations = new Texture2D[1];
            this.Position = position;
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
                this.Texture,
                new Vector2(
                    Position.X,
                    Position.Y),
                Color.White);
        }

        public override void LoadContent(ContentManager Content)
        {
            this.TextureAnimations[0] = Content.Load<Texture2D>("Gem");
            this.Texture = TextureAnimations[0];
        }

        public override void Use(Player player)
        {
            this.Taken = true;
            Game1.score += 15;
        }
    }
}
