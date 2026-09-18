using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace ShootMeUp
{
    public class projectileBurger
    {
        private int _x;                                           // Position en X depuis la gauche de l'espace aérien
        private int _y;                                           // Position en Y depuis le haut de l'espace aérien
        private int _destX;
        private int _destY;
        private int _originX;
        private int _originY;
        private const int SPEED = 20;                           // Vitesse du projectile
        private const int BURGER_HEIGHT = 50;                   // hauteur du projectile
        private const int BURGER_WIDTH = 50;                    // largeur du projectile
        private double _completionIndex = 0;
        private double _distance;

        public projectileBurger(int destX, int destY, int originX, int originY)
        {
            this._destX = destX;
            this._destY = destY;
            this._originX = originX;
            this._originY = originY;

            this._distance = mathHelper.distance(_originX, _originY, _destX, _destY);
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
