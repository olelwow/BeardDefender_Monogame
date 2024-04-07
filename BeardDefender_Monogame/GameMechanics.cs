using BeardDefender_Monogame.GameObjects;
using System;
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BeardDefender_Monogame
{
    internal abstract class GameMechanics
    {
        public static void EnemyCollision(Hedgehog hedgehog, Player player)
        {
            if (player.position.Y == hedgehog.position.Y 
             && player.position.X == hedgehog.position.X)
            {
                Environment.Exit(0);
            }
        }

        public static Rectangle ConvertRectangleFToRectangle (RectangleF rect)
        {
            Rectangle rectangle = new((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            return rectangle;
        }
    }
}
