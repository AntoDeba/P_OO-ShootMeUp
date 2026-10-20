using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp
{
    public class Robot
    {
        private float _x;                                       // Position en X depuis la gauche de l'espace aérien
        private float _y;                                       // Position en Y depuis le haut de l'espace aérien
        private float _expectedX;                               //X que l'on pense avoir (utilisé pour verifier les collisions)
        private float _expectedY;                               //Y que l'on pense avoir (utilisé pour verifier les collisions)
        private int _destX = 100;
        private int _destY = 100;
        private float _originX;
        private float _originY;
        private const int ROBOT_SPEED = 30;                     // Vitesse du Robot
        public const int ROBOT_WIDTH = 45;
        public const int ROBOT_HEIGHT = 80;
        private double _distance;
        private float _completionIndex = 0;
        private int _health;
        private int _initalHealt;
        private int _timeSinceArrivalAtDest;                    //Temps depuis lequel on est arriver à la position
        private int _level;
        private const int ROBOT_HEALTH_START = 21;              //PV d'un robot au début (évolue avec les niveaux) 
        private int _horizontalOrVertical;                      //Sera égal à 1 si le robot suis le joueur sur l axe y et 0 si c'est sur l'axe x.
        private const int TIME_FOR_AIMING = 30;

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
            _timeSinceArrivalAtDest = 0;

            _horizontalOrVertical = RandomValueHelper.alea.Next(0, 2);
            if (_horizontalOrVertical == 0)
            {
                _destY = RandomValueHelper.alea.Next(ROBOT_HEIGHT, BattleMap.HEIGHT - ROBOT_HEIGHT);
                _destX = BattleMap.Player.X;
            }
            else
            {
                _destX = RandomValueHelper.alea.Next(ROBOT_WIDTH, BattleMap.WIDTH - ROBOT_WIDTH);
                _destY = BattleMap.Player.Y;
            }
            _distance = MathHelper.Distance(_originX, _originY, _destX, _destY);
        }

        public void Update(int interval)
        {

            if (_completionIndex >= 1)
            {
                if(_timeSinceArrivalAtDest >= TIME_FOR_AIMING)
                {
                    _originX = _x;
                    _originY = _y;
                    if (_horizontalOrVertical == 0)
                    {
                        _destX = BattleMap.Player.X;
                        if (BattleMap.Player.Y > _y)
                            BattleMap.allFaisceaux.Add(new Faisceau(_x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), 'S'));
                        else
                            BattleMap.allFaisceaux.Add(new Faisceau(_x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), 'N'));
                    }
                    else
                    {
                        _destY = BattleMap.Player.Y;
                        if (BattleMap.Player.X > _x)
                            BattleMap.allFaisceaux.Add(new Faisceau(_x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), 'E'));
                        else
                            BattleMap.allFaisceaux.Add(new Faisceau(_x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), 'O'));
                    }

                    _distance = MathHelper.Distance(_originX, _originY, _destX, _destY);
                    _completionIndex = 0;
                    _timeSinceArrivalAtDest = 0;
                }
                else
                {
                    _timeSinceArrivalAtDest += 1;
                    
                }

            }
            else
            {
                _completionIndex += ROBOT_SPEED / Convert.ToSingle(_distance);
                
                _expectedX = _originX + ((_destX - _originX) * _completionIndex);
                _expectedY = _originY + ((_destY - _originY) * _completionIndex);

                for (int i = BattleMap.allObstacle.Count - 1; i >= 0; i--)
                {
                    if (MathHelper.AreTouching(_expectedX, BattleMap.allObstacle[i].X, _expectedY, BattleMap.allObstacle[i].Y, Obstacle.OBSTACLE_WIDTH, ROBOT_WIDTH, Obstacle.OBSTACLE_HEIGHT, ROBOT_HEIGHT))
                    {
                        //Si il se touchent, on trouve une nouvelle desination
                        if (_horizontalOrVertical == 0)
                        {
                            _destY = RandomValueHelper.alea.Next(ROBOT_HEIGHT, BattleMap.HEIGHT - ROBOT_HEIGHT);
                            _destX = BattleMap.Player.X;
                        }
                        else
                        {
                            _destX = RandomValueHelper.alea.Next(ROBOT_WIDTH, BattleMap.WIDTH - ROBOT_WIDTH);
                            _destY = BattleMap.Player.Y;
                        }

                        _originX = _x;
                        _originY = _y;
                        _distance = MathHelper.Distance(_originX, _originY, _destX, _destY);
                        _completionIndex = 0;
                        _timeSinceArrivalAtDest = 0;
                    }
                }
                if (_completionIndex != 0)
                {
                    _x = _expectedX;
                    _y = _expectedY;
                }
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.robot, _x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), ROBOT_WIDTH, ROBOT_HEIGHT);
            drawingSpace.Graphics.DrawString($"{_health}/{_initalHealt}", TextHelpers.drawFont, new SolidBrush(Color.Red), _x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2) - 10);
            if(_timeSinceArrivalAtDest % 2 == 0 && _timeSinceArrivalAtDest != 0) //fait clignoter une ellipse avant de tirer le rayons
            {
                drawingSpace.Graphics.FillEllipse(new SolidBrush(Color.SkyBlue), _x - (ROBOT_WIDTH / 2), _y - (ROBOT_HEIGHT / 2), ROBOT_WIDTH, ROBOT_WIDTH);
            }
        }
    }
}
