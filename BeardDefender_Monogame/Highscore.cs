using BeardDefender_Monogame.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BeardDefender_Monogame
{
    internal class Highscore : Background
    {    
        //Olika layers för att skapa bakgrunden i spelet.
        private Texture2D highscorebackground;
       

        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.
            this.Highscorebackground = Content.Load<Texture2D>("highscore_screen");    
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
            //Vector2 scale = new Vector2(20f, 20f);

            _spriteBatch.Draw(Highscorebackground, destinationRectangle, Color.White);
            

        }

        // Get/Set
        public Texture2D Highscorebackground
        {
            get { return highscorebackground; }
            set { highscorebackground = value; }
        }
               
        
    }
}