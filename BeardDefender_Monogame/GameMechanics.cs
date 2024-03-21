using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BeardDefender_Monogame
{
    internal abstract class GameMechanics
    {
        public static void CheckForCollisionsRight(
            List<Ground> upperGroundList,
            KeyboardState key,
            Player player)
        {
            foreach (Ground ground in upperGroundList)
            {
                if (player.position.X + (player.position.Width / 2) <= ground.Position.X
                    && key.IsKeyDown(Keys.Right))
                {
                    player.position.X += player.Speed;
                    player.PositionNew = player.position;
                    break;
                }
                else if (player.PositionNew.X + player.position.Width == ground.Position.X
                    || player.PositionNew.X + player.position.Width > ground.Position.X)
                {
                    player.position.X -= 2;
                    break;
                }
            }
        }
        public static void CheckForCollisionsLeft(
            List<Ground> upperGroundList,
            KeyboardState key,
            Player player)
        {
            foreach (Ground ground in upperGroundList)
            {
                if ((player.position.X + (player.position.Width / 2) <= ground.Position.X
                && key.IsKeyDown(Keys.Left)))
                {
                    player.position.X -= player.Speed;
                    player.PositionNew = player.position;
                }
                else if ((player.PositionNew.X + player.position.Width == ground.Position.X
                    || player.PositionNew.X + player.position.Width > ground.Position.X))
                {
                    player.position.X -= 1;
                }
            }
        }
        public static void CheckForCollisionsUp(
            List<Ground> upperGroundList,
            KeyboardState key,
            Player player)
        {

        }
        public static void ApplyGravity(List<Ground> lowerGroundList, Player player)
        {
            // Kollar varje ground objekt i listan, säkerställer att spelaren inte får ett högre Y-värde
            // än ground-objektets Y-värde.
            foreach (var ground in lowerGroundList)
            {
                if (player.Position.Y + player.position.Height >= ground.Position.Y && !player.Jumping)
                {
                    // Ifall spelarens Y är större eller lika med ground Y så sätts spelarens Y till marknivå och loopen avslutas.
                    player.position.Y = ground.Position.Y - player.position.Height / 2;
                    return;
                }
                else if (!player.Jumping)
                {
                    // Annars ökas spelarens Y med 2 för varje varv vilket simulerar gravitation.
                    player.position.Y += 2;
                }
            }
        }
    }
}
