using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardDefender_Monogame
{
    internal class WinnerScene
    {
        //Olika layers för att skapa bakgrunden i spelet.
        private Texture2D winnerbackground;

        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.
            this.Winnerbackground = Content.Load<Texture2D>("winner_screen");

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

            _spriteBatch.Draw(winnerbackground, destinationRectangle, Color.White);


        }

        // Get/Set
        public Texture2D Winnerbackground
        {
            get { return winnerbackground; }
            set { winnerbackground = value; }
        }


    }
}
