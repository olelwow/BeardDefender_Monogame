using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using System.Collections.Immutable;

namespace BeardDefender_Monogame
{
    internal class Highscore //: Background
    {
        //Olika layers för att skapa bakgrunden i spelet.
        private Texture2D highscorebackground;
        private Texture2D ScoreBackground;
        private SpriteFont spriteFont;
       
        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.
            this.Highscorebackground = Content.Load<Texture2D>("highscore_screen");
            this.spriteFont = Content.Load<SpriteFont>("ScoreFont");    
            this.ScoreBackground = Content.Load<Texture2D>("ScoreBackground");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void DrawBackground(SpriteBatch _spriteBatch, int MapWidth, int MapHeight)
        {
            //Skapar en rektangel i storlek med spelrutan och förstorar bakgrundsbilderna-
            //till samma storlek som spelytan.
            Vector2 desiredSize = new Vector2(MapWidth, MapHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, (int)desiredSize.X, (int)desiredSize.Y);
            Vector2 scale = new Vector2(20f, 20f);

            int vertical = MapHeight / 2 - 200;
            _spriteBatch.Draw(highscorebackground, destinationRectangle, Color.White);
            _spriteBatch.Draw(ScoreBackground, new Vector2(900, 155), Color.White);

            // Läsa highscore-fil
            string[] lines = File.ReadAllLines(
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.Desktop),
                            "Game highscore.txt"));

            if (lines == null
                || lines.Length == 0)
            {
                // Meddela ifall filen inte finns
                string message = "        No highscore available.\n\n      What are you waiting for\n             BeardDefender?\n\n       GO PLAY THE GAME!!!";
                _spriteBatch.DrawString(spriteFont, message, new Vector2(MapWidth / 2 + 160, vertical), Color.Red);
            }
            else
            {
                if (lines.Length > 1)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        for (int j = i + 1; j < lines.Length; j++)
                        {
                            if (int.Parse(lines[j].Split(' ')[1]) > int.Parse(lines[i].Split(' ')[1]))
                            {
                                string temp = lines[i];
                                lines[i] = lines[j];
                                lines[j] = temp;
                            }
                        }
                    }
                }

                // Rita varje rad i filen
                if (lines.Length > 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        _spriteBatch.DrawString(spriteFont, lines[i], new Vector2(MapWidth / 2 + 260, vertical), Color.Black);

                        // Justera för nästa rad
                        vertical += 35;
                    }
                }
                else
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        _spriteBatch.DrawString(spriteFont, lines[i], new Vector2(MapWidth / 2 + 260, vertical), Color.Black);

                        // Justera för nästa rad
                        vertical += 35;
                    }
                }
            }
        }

        // Get/Set
        public Texture2D Highscorebackground
        {
            get { return highscorebackground; }
            set { highscorebackground = value; }
        }
    }
}