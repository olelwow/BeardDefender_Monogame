using BeardDefender_Monogame.GameObjects;
using System;
using System.IO;
using System.Drawing;
using BeardDefender_Monogame.GameObjects.Powerups;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BeardDefender_Monogame.GameLevels
{
    internal abstract class GameLevel
    {
        private static Rectangle[] powerUpPositions = new Rectangle[3] 
        {
            new Rectangle(1200, 540, 60, 60),
            new Rectangle(900, 600, 60, 60),
            new Rectangle(700, 600, 60, 60) 
        };
        public static void ShowMainMenu (Game1 game)
        {
            game.LastPlayedLevel = game.ActiveScenes;
            game.ActiveScenes = Scenes.MAIN_MENU;
        }
        //public static void DeathByCrabman (
        //    GameTime gameTime,
        //    Game1 game,
        //    string filePath,
        //    List<PowerUp> powerUpList,
        //    Player player,
        //    HealthCounter healthCounter,
        //    List<Shark> sharkList)
        //{
        //    //File.AppendAllText(filePath, $"\nScore: {((int)Math.Ceiling(Game1.score)).ToString()} points");
        //    //game.ActiveScenes = 
        //    //                    Scenes.DEATH;


        //    //// Återställer allt till "0" för att kunna påbörja nytt spel
        //    //game.LevelTimer = 0;
        //    //game.player.HP = 1;
        //    //game.healthCounter.Update(gameTime, game.player);
        //    //int counter = 0;
        //    //foreach (PowerUp powerUp in powerUpList) 
        //    //{
        //    //    powerUp.Taken = false;
        //    //    powerUp.Position = powerUpPositions[counter];
        //    //    counter++;
        //    //}

        //    //game.player.Position = new RectangleF(600, 400, 25, 36);
        //    ResetGame(game, gameTime, filePath, player, healthCounter, powerUpList, sharkList);
        //}
        public static void ResetGame(
            Game1 game,
            GameTime gameTime,
            string filePath,
            Player player,
            HealthCounter healthCounter,
            List<PowerUp> powerUpList,
            List<Shark> sharkList)
        {
            File.AppendAllText(filePath, $"\nScore: {((int)Math.Ceiling(Game1.score)).ToString()} points");
            game.ActiveScenes = player.HP > 0 ? Scenes.WIN : Scenes.DEATH;
            game.LevelTimer = 0;
            player.HP = 1;

            healthCounter.Update(gameTime, player);

            int counter = 0;
            foreach (PowerUp powerUp in powerUpList)
            {
                powerUp.Taken = false;
                powerUp.Position = powerUpPositions[counter];
                counter++;
            }
            sharkList[0].Position = new Vector2(500, 600);
            sharkList[0].DrawShark = true;
            sharkList[0].SharkIsLeft = false;
            sharkList[1].Position = new Vector2(300, 450);
            sharkList[1].DrawShark = true;
            sharkList[1].SharkIsLeft = false;
            player.Position = new RectangleF(600, 400, 25, 36);
        }
    }
}
