using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BeardDefender_Monogame.GameObjects.Powerups
{
    internal class Heart : PowerUp
    {
        public Heart (Rectangle position)
        {
            this.TextureAnimations = new Texture2D[2];
            this.Position = position;
        }

        public Heart(System.Drawing.Rectangle rectangle)
        {
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            Vector2 scale = new(0.15f, 0.15f);
            _spriteBatch.Draw(
                this.Texture,
                new Vector2(
                    Position.X,
                    Position.Y),
                null,
                Color.White,
                0f,
                new Vector2(
                    this.Texture.Width / 2,
                    this.Texture.Height /2),
                scale,
                SpriteEffects.None,
                0f);
        }

        public override void LoadContent(ContentManager Content)
        {
            this.TextureAnimations[0] = Content.Load<Texture2D>("PowerUp_Hearts1");
            this.TextureAnimations[1] = Content.Load<Texture2D>("PowerUp_Hearts2");
            this.Texture = TextureAnimations[0];
        }

        public override void Use(Player player)
        {
            player.HP++;
            this.Taken = true;
        }
    }
}
