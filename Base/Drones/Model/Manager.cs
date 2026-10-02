using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp
{
    public class Manager
    {
        private float _x;                                      // Position en X depuis la gauche de l'espace aérien
        private float _y;                                      // Position en Y depuis le haut de l'espace aérien
        private int _destX = 100;
        private int _destY = 100;
        private float _originX;
        private float _originY;
        private const int MANAGER_SPEED = 10;                    // Vitesse du Managaer
        private const int MANAGER_WIDTH = 45;
        private const int MANAGER_HEIGHT = 80;
        private double _distance;
        private float _completionIndex = 1;
        
        public Manager(int x, int y)
        {
            _x = x;
            _y = y;
            _originX = x;
            _originY = y;
        }

        public void Update(int interval)
        {
            if (_completionIndex >= 1)
            {
                _originX = _x;
                _originY = _y;
                _destX = randomValueHelper.Alea.Next(MANAGER_WIDTH, BattleMap.WIDTH - MANAGER_WIDTH);
                _destY = randomValueHelper.Alea.Next(MANAGER_HEIGHT, BattleMap.HEIGHT - MANAGER_HEIGHT);
                _distance = mathHelper.distance(_originX, _originY, _destX, _destY);
                _completionIndex = 0;
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
            drawingSpace.Graphics.DrawImage(Resources.Manager, _x - (MANAGER_WIDTH/2), _y - (MANAGER_HEIGHT/2), MANAGER_WIDTH, MANAGER_HEIGHT);
        }
    }
}
