using BeardDefender_Monogame.GameObjects.Powerups;
using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BeardDefender_Monogame.GameLevels
{
    internal abstract class LevelTwo
    {
        public static void Update(
            Game1 game,
            string filePath,
            KeyboardState keyboardState,
            GameTime gameTime,
            GraphicsDevice graphicsDevice,
            GraphicsDeviceManager _graphics,
            List<Background> backgroundList,
            Player player,
            List<Ground> groundList,
            Hedgehog hedgehog,
            Crabman crabman,
            List<Shark> sharkList,
            List<PowerUp> powerUpList,
            HealthCounter healthCounter)
        {
            // Kontrollera spelarens rörelse för att uppdatera bakgrunden
            bool isPlayerMoving = keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Right);
            int playerDirection = keyboardState.IsKeyDown(Keys.Right) ? 1 : -1;
            int playerSpeed = 5;

            // Only update the background if the player is actually moving
            if (isPlayerMoving)
            {
                foreach (Background background in backgroundList)
                {
                    background.Update(gameTime, isPlayerMoving, playerSpeed, playerDirection);
                }
            }
            // Uppdatera spelarposition, fiender, osv.
            foreach (Ground ground in groundList)
            {
                ground.Update(gameTime, graphicsDevice.Viewport.Width);
            }
            // Player movement, sätter players variabel IsFacingRight till returvärdet av
            // metoden, som håller koll på vilket håll spelaren är riktad åt.
            player.IsFacingRight =
                player.MovePlayer(
                    keyboardState,
                    gameTime,
                    hedgehog,
                    groundList,
                    (JumpBoost)powerUpList[1]);

            //returnerar rätt frame index som används i Update.
            crabman.CurrentFrameIndex = crabman.Update(_graphics, gameTime, player, game, filePath, powerUpList, sharkList, hedgehog, healthCounter);

            // Shark movement, returnerar rätt frame index som används i Update.
            foreach (Shark shark in sharkList)
            {
                shark.CurrentFrameIndex = shark.Update(_graphics, gameTime, player, game, filePath, powerUpList, sharkList, hedgehog, healthCounter);
            }

            hedgehog.CurrentFrameIndex = hedgehog.Update(_graphics, gameTime, player, game, filePath, powerUpList, sharkList, hedgehog, healthCounter);

            //Updaterar score i sammaband med spelets timer
            Game1.score += (double)gameTime.ElapsedGameTime.TotalSeconds;

            player.CurrentAnimation.Update(gameTime);

            // Powerups
            foreach (PowerUp powerUp in powerUpList)
            {
                powerUp.Update(gameTime, player);
            }

            healthCounter.Update(gameTime, player);
        }
        public static void Draw(
            SpriteBatch _spriteBatch,
            List<Background> backgroundList,
            List<Ground> groundList,
            List<PowerUp> powerUpList,
            Player player,
            Crabman crabman,
            List<Shark> sharkList,
            Hedgehog hedgehog,
            HealthCounter healthCounter)
        {
            foreach (Background background in backgroundList)
            {
                background.DrawBackground(_spriteBatch, Game1.MapWidth, Game1.MapHeight);
            }

            //ScoreBox Texturer rittas här
            _spriteBatch.Draw(Game1.ScoreBox, Game1.ScoreBoxPosition, Color.White);
            _spriteBatch.DrawString(Game1.ScoreFont, "Score : ", new Vector2(28, 30), Color.Black);
            _spriteBatch.DrawString(Game1.ScoreFont, ((int)Game1.score).ToString(), new Vector2(138, 30), Color.Black);

            player.DrawPlayer(_spriteBatch);

            // SHAAAARKs draw metod sköter animationer beroende på åt vilket håll hajen rör sig.
            crabman.Draw(_spriteBatch);
            foreach (Shark shark in sharkList)
            {
                if (shark.DrawShark)
                {
                    shark.Draw(_spriteBatch);
                }
            }
            hedgehog.Draw(_spriteBatch);

            //Ground
            foreach (var item in groundList)
            {
                item.Draw(_spriteBatch);
            }
            foreach (PowerUp powerUp in powerUpList)
            {
                if (!powerUp.Taken)
                {
                    powerUp.Draw(_spriteBatch);
                }
            }
            healthCounter.Draw(_spriteBatch);
        }
    }
}
