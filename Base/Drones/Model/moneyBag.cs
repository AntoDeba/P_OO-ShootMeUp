using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp
{
    public class moneyBag
    {
        private float _x;                                      // Position en X depuis la gauche de l'espace aérien
        private float _y;                                      // Position en Y depuis le haut de l'espace aérien
        private int _destX;
        private int _destY;
        private int _originX;
        private int _originY;
        public const int MONEY_BAG_HEIGHT = 50;                   // hauteur du projectile
        public const int MONEY_BAG_WIDTH = 50;                    // largeur du projectile
        private const int MONEY_BAG_SPEED = 30;                   // Vitesse du projectile
        private double _distance;
        private float _completionIndex = 0;
        private int _damage;

        public float X { get => _x; }
        public float Y { get => _y; }
        public int Damage { get => _damage; }

        public moneyBag(int destX, int destY, int originX, int originY, int damage)
        {
            this._destX = destX;
            this._destY = destY;
            this._originX = originX;
            this._originY = originY;
            this._x = originX;
            this._y = originY;
            this._damage = damage;

            this._distance = mathHelper.distance(_originX, _originY, _destX, _destY);
        }

        public void Update(int interval)
        {
            _completionIndex += MONEY_BAG_SPEED / Convert.ToSingle(_distance);

            _x = _originX + ((_destX - _originX) * _completionIndex);
            _y = _originY + ((_destY - _originY) * _completionIndex);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.moneyBag, _x - (MONEY_BAG_WIDTH / 2), _y - (MONEY_BAG_HEIGHT / 2), MONEY_BAG_WIDTH, MONEY_BAG_HEIGHT);
        }
    }
}
