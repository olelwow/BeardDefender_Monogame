using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardDefender_Monogame.GameObjects
{
    internal interface Enemy
    {
        void Update(GameTime gameTime, Vector2 playerPosition);
        void Draw(SpriteBatch spriteBatch);
    }
}
