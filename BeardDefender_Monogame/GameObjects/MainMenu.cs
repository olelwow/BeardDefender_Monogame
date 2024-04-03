using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardDefender_Monogame.GameObjects
{
    internal class MainMenu
    {
        //Olika layers för att skapa bakgrunden i spelet.
        private Texture2D background1;
        //private Texture2D background2;
        //private Texture2D background3;
        //private Texture2D background4;




        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.
            this.Background1 = Content.Load<Texture2D>("BeardDefender_MainMenu");
            //this.Background2 = Content.Load<Texture2D>("Bakgrund2");
            //this.Background3 = Content.Load<Texture2D>("Bakgrund3");
            //this.Background4 = Content.Load<Texture2D>("Bakgrund4");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void DrawMainMenu(SpriteBatch _spriteBatch, int MapWidth, int MapHeight)
        {
            //Skapar en rektangel i storlek med spelrutan och förstorar bakgrundsbilderna-1
            //till samma storlek som spelytan.
            Vector2 desiredSize = new Vector2(MapWidth, MapHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, (int)desiredSize.X, (int)desiredSize.Y);
            //Vector2 scale = new Vector2(20f, 20f);

            _spriteBatch.Draw(Background1, destinationRectangle, Color.White);
            //_spriteBatch.Draw(Background2, destinationRectangle, Color.White);
            //_spriteBatch.Draw(Background3, destinationRectangle, Color.White);
            //_spriteBatch.Draw(Background4, destinationRectangle, Color.White);

        }



        // Get/Set

        public Texture2D Background1
        {
            get { return background1; }
            set { background1 = value; }
        }

        //public Texture2D Background2
        //{
        //    get { return background2; }
        //    set { background2 = value; }
        //}

        //public Texture2D Background3
        //{
        //    get { return background3; }
        //    set { background3 = value; }
        //}

        //public Texture2D Background4
        //{
        //    get { return background4; }
        //    set { background4 = value; }
        //}
    }
}
