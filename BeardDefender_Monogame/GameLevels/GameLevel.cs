

using BeardDefender_Monogame.GameObjects;
using static System.Formats.Asn1.AsnWriter;
using System;
using System.IO;

namespace BeardDefender_Monogame.GameLevels
{
    internal abstract class GameLevel
    {
        public static void ShowMainMenu (Game1 game)
        {
            game.LastPlayedLevel = game.ActiveScenes;
            game.ActiveScenes = Scenes.MAIN_MENU;
        }
        public static void DeathByCrabman (Game1 game, string filePath)
        {
            game.ActiveScenes = Scenes.DEATH;
            File.AppendAllText(filePath, $"\nScore: {((int)Math.Ceiling(Game1.score)).ToString()} points");
            game.LastPlayedLevel = Scenes.LEVEL_ONE;
            game.LevelTimer = 0;
            Game1.score = 0;
        }
    }
}
