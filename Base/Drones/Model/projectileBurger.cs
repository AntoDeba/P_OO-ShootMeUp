using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootMeUp.Properties;

namespace ShootMeUp
{
    public class projectileBurger
    {
        private int _x;                                           // Position en X depuis la gauche de l'espace aérien
        private int _y;                                           // Position en Y depuis le haut de l'espace aérien
        private const int SPEED = 20;                           // Vitesse du projectile
        private const int BURGER_HEIGHT = 50;                   // hauteur du projectile
        private const int BURGER_WIDTH = 50;                    // largeur du projectile

        public projectileBurger(int y, int x)
        {
            this._x = x;
            this._y = y;
        }

        public void Update(int interval)
        {

        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.burger, _x, _y, BURGER_WIDTH, BURGER_HEIGHT);
        }
    }
}
