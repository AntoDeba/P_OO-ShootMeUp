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
        private float _x;                                      // Position en X depuis la gauche de l'espace aérien
        private float _y;                                      // Position en Y depuis le haut de l'espace aérien
        private int _destX;
        private int _destY;
        private int _originX;
        private int _originY;                       
        private const int BURGER_HEIGHT = 50;                   // hauteur du projectile
        private const int BURGER_WIDTH = 50;                    // largeur du projectile
        private const int BURGER_SPEED = 30;                    // Vitesse du projectile
        private double _distance;
        private float _completionIndex = 0;

        public float X { get => _x;}
        public float Y { get => _y; }

        public projectileBurger(int destX, int destY, int originX, int originY)
        {
            this._destX = destX;
            this._destY = destY;
            this._originX = originX;
            this._originY = originY;
            this._x = originX;
            this._y = originY;

            this._distance = mathHelper.distance(_originX, _originY, _destX, _destY);
        }

        public void Update(int interval)
        {
            _completionIndex += BURGER_SPEED / Convert.ToSingle(_distance);

            _x = _originX + ((_destX - _originX) * _completionIndex);
            _y = _originY + ((_destY - _originY) * _completionIndex);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.burger, _x, _y, BURGER_WIDTH, BURGER_HEIGHT);
        }
    }
}
