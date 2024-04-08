using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BeardDefender_Monogame.GameObjects.Powerups
{
    internal class HealthCounter
    {
        private int counter;
        private Texture2D hpBox;
        private Dictionary<Vector2, Texture2D> hpBar;
        private Texture2D emptyHeart;
        private Texture2D fullHeart;
        private Vector2 hpBoxPosition;
        private Vector2 healthPosition1;
        private Vector2 healthPosition2;
        private Vector2 healthPosition3;

        public HealthCounter (Vector2 position)
        {
            this.hpBoxPosition = position;
            this.counter = 1;
            hpBar = new();
            healthPosition1 = new Vector2(hpBoxPosition.X + 80, hpBoxPosition.Y + 35);
            healthPosition2 = new Vector2(hpBoxPosition.X + 130, hpBoxPosition.Y + 35);
            healthPosition3 = new Vector2(hpBoxPosition.X + 180, hpBoxPosition.Y + 35);
        }

        public void LoadContent (ContentManager Content)
        {
            fullHeart = Content.Load<Texture2D>("PowerUp_Hearts1");
            emptyHeart = Content.Load<Texture2D>("Heart_Unfilled");
            hpBox = Content.Load<Texture2D>("ScoreBox");
            
            hpBar.Add(healthPosition1, fullHeart);
            hpBar.Add(healthPosition2, emptyHeart);
            hpBar.Add(healthPosition3, emptyHeart);
        }
        public void Update(GameTime gameTime, Player player)
        {
            if (player.HP == 0)
            {
                hpBar[healthPosition1] = emptyHeart;
                hpBar[healthPosition2] = emptyHeart;
                hpBar[healthPosition3] = emptyHeart;
            }
            if (player.HP == 1)
            {
                hpBar[healthPosition1] = fullHeart;
                hpBar[healthPosition2] = emptyHeart;
                hpBar[healthPosition3] = emptyHeart;
            }
            if (player.HP == 2)
            {
                hpBar[healthPosition1] = fullHeart;
                hpBar[healthPosition2] = fullHeart;
                hpBar[healthPosition3] = emptyHeart;
            }
            if (player.HP == 3)
            {
                hpBar[healthPosition1] = fullHeart;
                hpBar[healthPosition2] = fullHeart;
                hpBar[healthPosition3] = fullHeart;
            }
        }
        public void Draw (SpriteBatch _spriteBatch)
        {
            Vector2 scale = new(0.15f, 0.15f);
            _spriteBatch.Draw(hpBox, hpBoxPosition, Color.White);
            // Loopar igenom dictionary, där Key representeras av vektorn för position, och value är texturen.
            foreach (var texture in hpBar) 
            {
                _spriteBatch.Draw(
                    texture.Value,
                    texture.Key,
                    null,
                    Color.White,
                    0f,
                    new Vector2(
                        texture.Value.Width / 2,
                        texture.Value.Height / 2),
                    scale,
                    SpriteEffects.None,
                    0f);
            }
        }
    }
}
