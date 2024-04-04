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
        private List<Texture2D> _backgroundImages = new List<Texture2D>();
        private int _currentImageIndex = 1;
        private double _timer = 0;
        private double _interval = 100;



        public void LoadContent(ContentManager Content)
        {
            //Hämtar in respektive bild i variabler.            
            for (int i = 1; i <= 100; i++)
            {
                _backgroundImages.Add(Content.Load<Texture2D>("MainMenuBackground/BeardDefender" + i.ToString()));
            }
        }

        public void Update(GameTime gameTime)
        {
            // Uppdatera timern
            _timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            
            // Om timern överskrider intervallen så ändras bakgrunden
            if (_timer > _interval)
            {
                _currentImageIndex = (_currentImageIndex + 1) % _backgroundImages.Count;
                _timer = 0;
            }



        }

        public void DrawMainMenu(SpriteBatch _spriteBatch, int MapWidth, int MapHeight)
        {
            //Skapar en rektangel i storlek med spelrutan och förstorar bakgrundsbilderna-1
            //till samma storlek som spelytan.
            Vector2 desiredSize = new Vector2(MapWidth, MapHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, (int)desiredSize.X, (int)desiredSize.Y);

            
            _spriteBatch.Draw(_backgroundImages[_currentImageIndex], destinationRectangle, Color.White);
        }



        // Get/Set

        public Texture2D Background1
        {
            get { return background1; }
            set { background1 = value; }
        }
    }
}
