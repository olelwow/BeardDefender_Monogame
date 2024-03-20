using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardDefender_Monogame
{
    public class GroundEntity : IEntity
    {
        private readonly Game1 _game;
        public Vector2 Velocity;
        public IShapeF Bounds { get; }
        public Texture2D groundTexture;

        public GroundEntity(Game1 game, RectangleF rectangleF)
        {
            _game = game;
            Bounds = rectangleF;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
        }

        public virtual void Update(GameTime gameTime)
        {
            Bounds.Position += Velocity * gameTime.GetElapsedSeconds() * 50;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            Velocity.X *= -1;
            Velocity.Y *= -1;
            Bounds.Position -= collisionInfo.PenetrationVector;
        }

    }
}
