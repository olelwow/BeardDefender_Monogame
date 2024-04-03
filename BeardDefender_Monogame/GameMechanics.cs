using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BeardDefender_Monogame
{
    internal abstract class GameMechanics
    {
        public static void CheckForCollisionsRight(
            List<Ground> groundList,
            KeyboardState key,
            Player player)
        {
            foreach (Ground ground in groundList)
            {
                if (player.position.IntersectsWith(ground.Position)
                    //player.position.X + (player.position.Width / 2) <= ground.Position.X
                    && key.IsKeyDown(Keys.Right))
                {
                    
                    player.position.X += player.Speed;
                    player.PositionNew = player.position;
                    break;
                }
                //else if (player.PositionNew.X + player.position.Width == ground.Position.X
                //    || player.PositionNew.X + player.position.Width > ground.Position.X)
                else
                {
                    player.position.X -= 2;
                    break;
                }
            }
        }
        public static void CheckForCollisionsLeft(
            List<Ground> groundList,
            KeyboardState key,
            Player player)
        {
            foreach (Ground ground in groundList)
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

        public static void CheckForCollisionsGeneral (
            List<Ground> groundList,
            Player player)
        {
            foreach (Ground ground in groundList)
            {
                if (player.position.IntersectsWith(ground.Position))
                {
                    player.IsOnBlock = true;
                }
                else
                {
                    player.IsOnBlock = false;

                }
                if (!player.IsOnBlock)
                {
                    
                }
                else
                {
                    if (player.Position.X <= ground.Position.X)
                    {
                        player.position.X -= 4;
                    }
                    else
                    {
                        player.position.Y = ground.Position.Top - (player.Texture.Height / 4);
                    }
                    
                }
            }
            //foreach (Ground ground in groundList)
            //{
            //    //if (!player.IsOnBlock)
            //    //{
            //    //    player.position.Y = 720 - 80 - (player.Texture.Height / 4);
            //    //}
            //    //else
            //    //{
            //    //    player.position.Y = 610 - (player.Texture.Height / 4);
            //    //}
            //}

            //foreach (Ground ground in groundList)
            //{
            //    if (player.position.Intersects(ground.Position))
            //    {
            //        int overlapX = Math.Min(player.position.Right, ground.Position.Right) - Math.Max(player.position.Left, ground.Position.Left);
            //        int overlapY = Math.Min(player.position.Bottom, ground.Position.Bottom) - Math.Max(player.position.Top, ground.Position.Top);

            //        if (overlapX > overlapY)
            //        {
            //            if (player.position.Top < ground.Position.Top)
            //            {
            //                player.position.Y -= overlapY;
            //            }
            //            else
            //            {
            //                player.position.Y += overlapY;
            //            }
            //        }
            //        else
            //        {
            //            if (player.position.Left < ground.Position.Left)
            //            {
            //                player.position.X -= overlapX;
            //            }
            //            else
            //            {
            //                player.position.X += overlapX;
            //            }
            //        }
            //    }
            //}
        }
        public static void ApplyGravity(List<Ground> groundList, Player player)
        {
            // Kollar varje ground objekt i listan, säkerställer att spelaren inte får ett högre Y-värde
            // än ground-objektets Y-värde.
            foreach (var ground in groundList)
            {
                if (player.Jumping)
                {
                    player.Speed = 5.2f;
                    CheckForCollisionsGeneral(groundList, player);
                }
                if (player.Position.Y + player.position.Height >= ground.Position.Y && !player.Jumping)
                {
                    // Ifall spelarens Y är större eller lika med ground Y så sätts spelarens Y till marknivå och loopen avslutas.
                    player.position.Y = ground.Position.Y - player.position.Height / 2;
                    player.Speed = 4.05f;
                    CheckForCollisionsGeneral(groundList, player);
                    return;
                }
                else if (!player.Jumping)
                {
                    // Annars ökas spelarens Y med 2 för varje varv vilket simulerar gravitation.
                    player.position.Y += 2;
                    CheckForCollisionsGeneral(groundList, player);
                }
            }
        }
    }
}
