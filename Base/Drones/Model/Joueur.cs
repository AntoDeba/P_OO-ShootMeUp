using Microsoft.VisualBasic.Devices;
using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;

namespace ShootMeUp
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Joueur
    {
        private int _x;                                              // Position en X depuis la gauche de l'espace aérien
        private int _y;                                              // Position en Y depuis le haut de l'espace aérien
        private const int SPEED = 20;                               // Vitesse du joueur
        public const int PLAYER_HEIGHT = 50;
        public const int PLAYER_WIDTH = 50;
        private List<char> keysPressed = new List<char>();          //Liste des touches pressé
        private int _framesSinceLastShot = 0;                       //Nombres de frame depuis la dernieres balle tirée
        private int _framesSinceLastMeleeAtack = 0;                 //Nombre de frane depuis la derniere attaque de mélée
        private const int SHOOTING_RELAOD_TIME = 10;                //Temps de rechargement
        private const int MELEE_RELAOD_TIME = 2;                    //Temps de rechargement
        private bool _modeMelee = false;                            //Vrai quand 
        public const int MELEE_DAMAGE = 3;
        public static readonly int MELEE_RANGE = (int)Math.Round(PLAYER_HEIGHT * 1.5);
        private int _pv = 100;


        public List<char> KeysPressed { get => keysPressed; set => keysPressed = value; }
        public bool ModeMelee { get => _modeMelee; }
        public int Y { get => _y; }
        public int X { get => _x; }

        // Constructeur
        public Joueur(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        // Cette méthode calcule le nouvel état dans lequel le joueur se trouve après que
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval) 
        {
            _framesSinceLastMeleeAtack++;
            _framesSinceLastShot++;

            //Modifife la position du joueur en fonction de la touche sur laquelle il appuie en ne dépassant pas les limites.
            //la position change en fonction de la vitesse
            if (keysPressed.Contains('W') == true && _y > 0)
                _y -= SPEED;
            if (keysPressed.Contains('S') == true && _y < BattleMap.HEIGHT-PLAYER_HEIGHT)
                _y += SPEED;
            if (keysPressed.Contains('A') == true && _x > 0)
                _x -= SPEED;
            if (keysPressed.Contains('D') == true && _x < BattleMap.WIDTH-PLAYER_WIDTH)
                _x += SPEED;

            if(_framesSinceLastMeleeAtack == 1)
            {
                _modeMelee = true;
            }
            else
            {
                _modeMelee = false;
            }
        }

        public void mouseInput(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_framesSinceLastShot >= SHOOTING_RELAOD_TIME && _pv > 0)
                {
                    projectileBurger monBurger = new projectileBurger(e.X, e.Y, _x, _y);
                    BattleMap.allBurgers.Add(monBurger);
                    _framesSinceLastShot = 0;
                }

            }


            if (e.Button == MouseButtons.Right)
            {
                if (_framesSinceLastMeleeAtack > MELEE_RELAOD_TIME && _pv > 0)
                {
                    _framesSinceLastMeleeAtack = 0;
                }
            }
                
                
            

        }

        /// //////////////////////////////////////////////////////////////////////////////
        //  
        //  Ce qui suit appartient à la vue, pas au modèle.
        //  Il aurait été préférable de séparer la déclaration de la classe Drone en deux,
        //  Nous regroupons tout ici pour simplifier
        //  
        /// //////////////////////////////////////////////////////////////////////////////


        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            
            if(_pv > 0)
            {
                drawingSpace.Graphics.DrawImage(Resources.drone, _x - (PLAYER_WIDTH / 2), _y - (PLAYER_HEIGHT / 2), PLAYER_WIDTH, PLAYER_HEIGHT);
                if (_modeMelee == true)
                {
                    drawingSpace.Graphics.FillEllipse(new SolidBrush(Color.Purple), _x - (PLAYER_WIDTH / 2), _y - (PLAYER_HEIGHT / 2), PLAYER_WIDTH, PLAYER_HEIGHT);
                }
                drawingSpace.Graphics.DrawString($"{_pv}/100", new Font("Arial", 20), new SolidBrush(Color.Red), 0, BattleMap.HEIGHT - 40);
            }
            else
            {
                drawingSpace.Graphics.DrawString($"Vous avez perdu !", new Font("Arial", 40), new SolidBrush(Color.Red), (BattleMap.WIDTH / 2)-150, BattleMap.HEIGHT/2-30);
                drawingSpace.Graphics.DrawString($"Vous avez été licencié de la corporation Wacdonald's™ et n'obtiendrez aucune compentation de salaire", new Font("Arial", 15), new SolidBrush(Color.Red) , 150, (BattleMap.HEIGHT / 2)+ 30);
            }
        }
        

    }
}
