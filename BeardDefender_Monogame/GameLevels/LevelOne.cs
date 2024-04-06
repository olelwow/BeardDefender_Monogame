using BeardDefender_Monogame.GameObjects.Powerups;
using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BeardDefender_Monogame.GameLevels
{
    internal abstract class LevelOne
    {
        private static Rectangle[] powerUpPositions = new Rectangle[3] 
        {   
            new Rectangle(250, 400, 60, 60),
            new Rectangle(700, 520, 60, 60),
            new Rectangle(1200, 340, 60, 60) 
        };
        public static void Update (
            KeyboardState keyboardState,
            GameTime gameTime,
            GraphicsDevice graphicsDevice,
            GraphicsDeviceManager _graphics,
            List<Background> backgroundList,
            Player player,
            List<Ground> groundList,
            Hedgehog hedgehog,
            Crabman crabman,
            Shark shark,
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
            player.position.Y = groundList[0].Position.Y - (player.Texture.Height / 4);
            foreach (Ground ground in groundList)
            {
                ground.Update(gameTime, graphicsDevice.Viewport.Width);
            }
            // Player movement, sätter players variabel IsFacingRight till returvärdet av
            // metoden, som håller koll på vilket håll spelaren är riktad åt.
            player.IsFacingRight =
                player.MovePlayer(
                    keyboardState,
                    hedgehog,
                    groundList,
                    (JumpBoost)powerUpList[1]);

            //returnerar rätt frame index som används i Update.
            crabman.CurrentFrameIndex = crabman.Update(_graphics, gameTime);

            // Shark movement, returnerar rätt frame index som används i Update.
            shark.CurrentFrameIndex = shark.Update(_graphics, gameTime);

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
            Shark shark,
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
            shark.Draw(_spriteBatch);
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
        public static void ChangeLevel (Game1 game, List<PowerUp> powerUpList)
        {
            game.ActiveScenes = Scenes.LEVEL_TWO;
            game.LevelTimer = 0;
            int counter = 0;
            // Tilldelar nya positioner till powerups inför level 2.
            foreach (PowerUp powerUp in powerUpList)
            {
                powerUp.Taken = false;
                powerUp.Position = powerUpPositions[counter];
                counter++;
            }
        }
    }
}
