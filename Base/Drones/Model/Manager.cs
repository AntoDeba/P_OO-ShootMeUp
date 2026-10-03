using ShootMeUp.Helpers;
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
        private int _destX = 100;
        private int _destY = 100;
        private float _originX;
        private float _originY;
        private const int MANAGER_SPEED = 10;                    // Vitesse du Managaer
        public const int MANAGER_WIDTH = 45;
        public const int MANAGER_HEIGHT = 80;
        private double _distance;
        private float _completionIndex = 1;
        private int _health;
        private int _loot;                          //Argent qu'un manager fait tomber
        private int _initalHealt;
        private int _fireFrame;                     //Toute les 40 frames on tire
        private int _managerLevel;
        const int MANAGER_LOOT_AT_START = 5000;     //Argent qu'un manager fait tomber au début (évolue avec les niveaux)
        const int MANAGER_HEALTH_START = 13;        //PV d'un manager au début (évolue avec les niveaux) 
        private int _projectileDamage;               //Dégats effectuer par les sacs (évolue avec les niveaux)


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
            _managerLevel = managerLevel;

            if(_managerLevel == 1)
            {
                _health = MANAGER_HEALTH_START;
                _loot = MANAGER_LOOT_AT_START;
                _projectileDamage = 10;
            }
            if (_managerLevel == 2)
            {
                _health = MANAGER_HEALTH_START * 2;
                _loot = MANAGER_LOOT_AT_START * 4;
                _projectileDamage = 15;
            }
            if (_managerLevel == 3)
            {
                _health = MANAGER_HEALTH_START * 4;
                _loot = MANAGER_LOOT_AT_START * 16;
                _projectileDamage = 20;
            }
            
            _initalHealt = _health;

            _fireFrame = randomValueHelper.Alea.Next(40);       //Ajout d'aléatoire pour pas que tout les manager tire en même temps.
        }

        public void Update(int interval)
        {
            _fireFrame++;

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

            if(_fireFrame % 40 == 0)
            {
                BattleMap.allMoneyBags.Add(new moneyBag(BattleMap.Player.X, BattleMap.Player.Y, (int)Math.Round(_x) , (int)Math.Round(_y), _projectileDamage)); //Toutes les 4 secondes on tire un burger en direction du joueur
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.Manager, _x - (MANAGER_WIDTH/2), _y - (MANAGER_HEIGHT/2), MANAGER_WIDTH, MANAGER_HEIGHT);
            drawingSpace.Graphics.DrawString($"{_health}/{_initalHealt}", TextHelpers.drawFont, new SolidBrush(Color.Red), _x - (MANAGER_WIDTH / 2), _y - (MANAGER_HEIGHT / 2) - 10);
        }
    }
}
