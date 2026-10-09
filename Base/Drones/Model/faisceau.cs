using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp.Model
{
    public class Faisceau
    {
        private float _x;                                   // Position en X depuis la gauche de la carte
        private float _y;                                   // Position en Y depuis le haut de la carte
        public const int BEAM_HEIGHT = 20;                  // hauteur du projectile
        public const int BEAM_WIDTH = 145;                  // largeur du projectile
        private const int BEAM_SPEED = 20;                   // Vitesse du projectile
        public const int BEAM_DAMAGE = 25;                  
        private char _direction;                            //les directions cardinal (N,E,S,O) pour les quatre direction 

        public float X { get => _x; }
        public float Y { get => _y; }
        public char Direction { get => _direction; set => _direction = value; }

        public Faisceau(float originX, float originY, char direction)
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

            Graphics g = drawingSpace.Graphics;
            GraphicsState state = g.Save(); //Sauvegarde la position et la rotation de l'écran

            // Déplacer le centre de rotation sur le faisceau
            g.TranslateTransform(_x , _y );

            if (_direction == 'S')
            {
                g.RotateTransform(90);
            }
            if (_direction == 'N')
            {
                g.RotateTransform(270);
            }
            if (_direction == 'E')
            {
                g.RotateTransform(0);
            }
            if (_direction == 'O')
            {
                g.RotateTransform(180);
            }

            // Dessine le faisceau
            g.DrawImage(Resources.faisceau,0 - (BEAM_WIDTH / 2), 0 - (BEAM_HEIGHT / 2), BEAM_WIDTH, BEAM_HEIGHT);

            g.Restore(state);

        }
    }
}
