using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp.Model
{
    public class Robot
    {
        private float _x;                                       // Position en X depuis la gauche de l'espace aérien
        private float _y;                                       // Position en Y depuis le haut de l'espace aérien
        private int _destX = 100;
        private int _destY = 100;
        private float _originX;
        private float _originY;
        private const int ROBOT_SPEED = 10;                     // Vitesse du Robot
        public const int ROBOT_WIDTH = 45;
        public const int ROBOT_HEIGHT = 80;
        private double _distance;
        private float _completionIndex = 1;
        private int _health;
        private int _initalHealt;
        private int _fireFrame;                                 //Toute les 40 frames on tire
        private int _level;
        private const int ROBOT_HEALTH_START = 21;              //PV d'un robot au début (évolue avec les niveaux) 
        private int _horizontalOrVertical;                      //Sera égal à 1 si le robot suis le joueur sur l axe y et 0 si c'est sur l'axe x.

        public int Health { get => _health; set => _health = value; }
        public float X { get => _x; }
        public float Y { get => _y; }

        public Robot(int x, int y, int robotLevel)
        {
            _x = x;
            _y = y;
            _originX = x;
            _originY = y;
            _level = robotLevel;

            if (_level == 2)
            {
                _health = ROBOT_HEALTH_START;

            }
            if (_level == 3)
            {
                _health = ROBOT_HEALTH_START * 2;

            }

            _initalHealt = _health;

            _horizontalOrVertical = randomValueHelper.Alea.Next(0, 2);
            if (_horizontalOrVertical == 0)
            {
                _destY = randomValueHelper.Alea.Next(ROBOT_HEIGHT, BattleMap.HEIGHT - ROBOT_HEIGHT);
            }
            else
            {
                _destX = randomValueHelper.Alea.Next(ROBOT_WIDTH, BattleMap.WIDTH - ROBOT_WIDTH);
            }
        }

        public void Update(int interval)
        {
            _fireFrame++;

            if (_completionIndex >= 1)
            {
                _originX = _x;
                _originY = _y;
                if(_horizontalOrVertical == 0)
                {
                    _destX = BattleMap.Player.X;
                }
                else
                {
                    _destY = BattleMap.Player.Y; 
                }

                _distance = mathHelper.distance(_originX, _originY, _destX, _destY);
                _completionIndex = 0;
            }
            else
            {
                _completionIndex += ROBOT_SPEED / Convert.ToSingle(_distance);


                _x = _originX + ((_destX - _originX) * _completionIndex);
                _y = _originY + ((_destY - _originY) * _completionIndex);
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.robot, _x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), ROBOT_WIDTH, ROBOT_HEIGHT);
            drawingSpace.Graphics.DrawString($"{_health}/{_initalHealt}", TextHelpers.drawFont, new SolidBrush(Color.Red), _x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2) - 10);
        }
    }
}
