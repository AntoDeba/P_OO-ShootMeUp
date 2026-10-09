using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp.Model
{
    public class faisceau
    {
        private float _x;                                   // Position en X depuis la gauche de la carte
        private float _y;                                   // Position en Y depuis le haut de la carte
        public const int BEAM_HEIGHT = 50;                  // hauteur du projectile
        public const int BEAM_WIDTH = 50;                   // largeur du projectile
        private const int BEAM_SPEED = 30;                  // Vitesse du projectile
        public const int BEAM_DAMAGE = 25;                  
        private char _direction;                            //les directions cardinal (N,E,S,O) pour les quatre direction 

        public float X { get => _x; }
        public float Y { get => _y; }


        public faisceau(int originX, int originY, char direction)
        {
            this._x = originX;
            this._y = originY;
            this._direction = direction;
        }

        public void Update(int interval)
        {
            if(_direction == 'S')
            {
                _y += BEAM_SPEED;
            }
            if (_direction == 'N')
            {
                _y -= BEAM_SPEED;
            }
            if (_direction == 'E')
            {
                _x += BEAM_SPEED;
            }
            if (_direction == 'O')
            {
                _x -= BEAM_SPEED;
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            // Déplacer le centre de rotation sur le faisceau
            drawingSpace.Graphics.TranslateTransform(_x, _y);

            if (_direction == 'S')
            {
                drawingSpace.Graphics.RotateTransform(0);
            }
            if (_direction == 'N')
            {
                drawingSpace.Graphics.RotateTransform(90);
            }
            if (_direction == 'E')
            {
                drawingSpace.Graphics.RotateTransform(180);
            }
            if (_direction == 'O')
            {
                drawingSpace.Graphics.RotateTransform(270);
            }

            // Dessiner le joueur
            drawingSpace.Graphics.DrawImage(Resources.moneyBag, _x - (BEAM_WIDTH / 2), _y - (BEAM_HEIGHT / 2), BEAM_WIDTH, BEAM_HEIGHT);

        }
    }
}
