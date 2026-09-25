using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp
{
    public class Manager
    {
        private float _x;                                      // Position en X depuis la gauche de l'espace aérien
        private float _y;                                      // Position en Y depuis le haut de l'espace aérien
        private int _destX = 10;
        private int _destY = 10;
        private int _originX;
        private int _originY;
        private const int MANAGER_SPEED = 0;                    // Vitesse du projectile
        private const int MANAGER_WIDTH = 30;
        private const int MANAGER_HEIGHT = 30;
        private double _distance;
        private float _completionIndex = 0;
        
        public Manager(int x, int y)
        {
            _x = x;
            _y = y;
            _originX = x;
            _originY = y;
        }

        public void Update(int interval)
        {
            if(_completionIndex >= 1)
            {

            }
            else 
            {
                _completionIndex += MANAGER_SPEED / Convert.ToSingle(_distance);


                _x = _originX + ((_destX - _originX) * _completionIndex);
                _y = _originY + ((_destY - _originY) * _completionIndex);
            }

        }

        public void Render(BufferedGraphics drawingSpace)
        {
            //drawingSpace.Graphics.DrawImage(Resources.Manager, _x, _y, MANAGER_WIDTH, MANAGER_HEIGHT);
        }
    }
}
