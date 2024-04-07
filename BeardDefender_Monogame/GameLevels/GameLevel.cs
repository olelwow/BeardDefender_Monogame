

using BeardDefender_Monogame.GameObjects;
using static System.Formats.Asn1.AsnWriter;
using System;
using System.IO;
using System.Drawing;
using BeardDefender_Monogame.GameObjects.Powerups;
using Microsoft.Xna.Framework;

namespace BeardDefender_Monogame.GameLevels
{
    internal abstract class GameLevel
    {
        public static void ShowMainMenu (Game1 game)
        {
            game.LastPlayedLevel = game.ActiveScenes;
            game.ActiveScenes = Scenes.MAIN_MENU;
        }
        public static void DeathByCrabman (GameTime gameTime, Game1 game, string filePath)
        {
            File.AppendAllText(filePath, $"\nScore: {((int)Math.Ceiling(Game1.score)).ToString()} points");
            game.ActiveScenes = Scenes.DEATH;

            // Återställer allt till "0" för att kunna påbörja nytt spel
            game.LevelTimer = 0;
            game.player.HP = 1;
            game.healthCounter.Update(gameTime, game.player);
            game.gemScore.Taken = false;
            game.heart.Taken = false;
            game.jumpBoost.Taken = false;
            game.gemScore.Position = new Microsoft.Xna.Framework.Rectangle(1200, 540, 60, 60);
            game.heart.Position = new Microsoft.Xna.Framework.Rectangle(900, 600, 60, 60);
            game.jumpBoost.Position = new Microsoft.Xna.Framework.Rectangle(700, 600, 60, 60);
            game.player.Position = new RectangleF(600, 400, 25, 36);
        }
    }
}
