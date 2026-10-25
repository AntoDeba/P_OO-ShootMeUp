using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp
{
    public class Manager
    {
        private float _x;                                      // Position en X depuis la gauche de l'espace aérien
        private float _y;                                      // Position en Y depuis le haut de l'espace aérien

        private float _expectedX;                               //X que l'on pense avoir (utilisé pour verifier les collisions)
        private float _expectedY;                               //Y que l'on pense avoir (utilisé pour verifier les collisions)
        private int _destX;
        private int _destY;
        private float _originX;
        private float _originY;
        private const int MANAGER_SPEED = 10;                    // Vitesse du Managaer
        public const int MANAGER_WIDTH = 45;
        public const int MANAGER_HEIGHT = 80;
        private double _distance;
        private float _completionIndex = 1;
        private int _health;
        private int _loot;                                  //Argent qu'un manager fait tomber
        private int _initalHealt;
        private int _fireFrame;                             //Toute les 40 frames on tire
        private int _level;
        private const int MANAGER_LOOT_AT_START = 5000;     //Argent qu'un manager fait tomber au début (évolue avec les niveaux)
        private const int MANAGER_HEALTH_START = 13;        //PV d'un manager au début (évolue avec les niveaux) 
        private int _projectileDamage;                      //Dégats effectuer par les sacs (évolue avec les niveaux)
        public const int FIRE_RATE = 40;                   //À quelle fréquence le manager tire (dixième de sec)


        public int Health { get => _health; set => _health = value; }
        public float X { get => _x;  }
        public float Y { get => _y;  }
        public int Loot { get => _loot;}

        public Manager(int x, int y, int managerLevel)
        {
            _x = x;
            _y = y;
            _originX = x;
            _originY = y;
            _level = managerLevel;

            if(_level == 1)
            {
                _health = MANAGER_HEALTH_START;
                _loot = MANAGER_LOOT_AT_START;
                _projectileDamage = 10;
            }
            if (_level == 2)
            {
                _health = MANAGER_HEALTH_START * 2;
                _loot = MANAGER_LOOT_AT_START * 4;
                _projectileDamage = 15;
            }
            if (_level == 3)
            {
                _health = MANAGER_HEALTH_START * 4;
                _loot = MANAGER_LOOT_AT_START * 16;
                _projectileDamage = 20;
            }
            
            _initalHealt = _health;

            _fireFrame = RandomValueHelper.alea.Next(FIRE_RATE);       //Ajout d'aléatoire pour pas que tout les manager tire en même temps.
        }

        public void Update(int interval)
        {
            _fireFrame++;

            if (_completionIndex >= 1)
            {
                _originX = _x;
                _originY = _y;
                _destX = RandomValueHelper.alea.Next(MANAGER_WIDTH, BattleMap.WIDTH - MANAGER_WIDTH);
                _destY = RandomValueHelper.alea.Next(MANAGER_HEIGHT, BattleMap.HEIGHT - MANAGER_HEIGHT);
                _distance = MathHelper.Distance(_originX, _originY, _destX, _destY);
                _completionIndex = 0;
            }
            else
            {
                _completionIndex += MANAGER_SPEED / Convert.ToSingle(_distance);
                _expectedX = _originX + ((_destX - _originX) * _completionIndex);
                _expectedY = _originY + ((_destY - _originY) * _completionIndex);

                for (int i = BattleMap.allObstacle.Count - 1; i >= 0; i--)
                {
                    if (MathHelper.AreTouching(_expectedX, BattleMap.allObstacle[i].X, _expectedY, BattleMap.allObstacle[i].Y, Obstacle.OBSTACLE_WIDTH, MANAGER_WIDTH, Obstacle.OBSTACLE_HEIGHT, MANAGER_HEIGHT))
                    {
                        _completionIndex = 1; //Forcera le manager a trouvé un nouveau chemin.
                    }
                }
                if(_completionIndex != 1)
                {
                    _x = _expectedX;
                    _y = _expectedY;
                }
            }

            if(_fireFrame % FIRE_RATE == 0)
            {
                BattleMap.allMoneyBags.Add(new MoneyBag(BattleMap.Player.X, BattleMap.Player.Y, (int)Math.Round(_x) , (int)Math.Round(_y), _projectileDamage)); //Toutes les 4 secondes on tire un burger en direction du joueur
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.Manager, _x - (MANAGER_WIDTH/2), _y - (MANAGER_HEIGHT/2), MANAGER_WIDTH, MANAGER_HEIGHT);
            drawingSpace.Graphics.DrawString($"{_health}/{_initalHealt}", TextHelpers.drawFont, new SolidBrush(Color.Red), _x - (MANAGER_WIDTH / 2), _y - (MANAGER_HEIGHT / 2) - 10);
        }
    }
}
