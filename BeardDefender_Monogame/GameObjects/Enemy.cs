using BeardDefender_Monogame.GameObjects.Powerups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BeardDefender_Monogame.GameObjects
{
    internal abstract class Enemy
    {
        // Update metod för Hedgehog.
        public virtual void Update(GameTime gameTime, Vector2 playerPosition) { }
        // Universal Draw metod.
        public virtual void Draw(SpriteBatch spriteBatch) { }
        // Update metod för Shark.
        public virtual int Update(
            GraphicsDeviceManager _graphics,
            GameTime gameTime,
            Player player,
            Game1 game,
            string filePath,
            List<PowerUp> powerUpList,
            List<Shark> sharkList,
            Hedgehog hedgehog,
            HealthCounter healthCounter) { return 0; }
    }
}
