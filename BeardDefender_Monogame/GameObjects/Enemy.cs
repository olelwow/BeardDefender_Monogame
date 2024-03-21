using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeardDefender_Monogame.GameObjects
{
    internal abstract class Enemy
    {
        // Update metod för Hedgehog.
        public virtual void Update(GameTime gameTime, Vector2 playerPosition) { }
        // Universal Draw metod.
        public virtual void Draw(SpriteBatch spriteBatch) { }
        // Update metod för Shark.
        public virtual int Update(GraphicsDeviceManager _graphics, GameTime gameTime) { return 0; }
    }
}
