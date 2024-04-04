using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

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

        public static void ApplyGravity(List<Ground> groundList, Player player, Hedgehog hedgehog)
        {
            // Kollar varje ground objekt i listan, säkerställer att spelaren inte får ett högre Y-värde
            // än ground-objektets Y-värde.
            foreach (var ground in groundList)
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
