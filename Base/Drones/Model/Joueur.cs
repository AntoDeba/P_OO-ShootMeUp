using Microsoft.VisualBasic.Devices;
using ShootMeUp.Helpers;
using ShootMeUp.Properties;

namespace ShootMeUp
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Joueur
    {
        private int x;                                              // Position en X depuis la gauche de l'espace aérien
        private int y;                                              // Position en Y depuis le haut de l'espace aérien
        private const int SPEED = 20;                               // Vitesse du joueur
        private const int PLAYER_HEIGHT = 50;
        private const int PLAYER_WIDTH = 50;
        private List<char> keysPressed = new List<char>();          //Liste des touches pressé
        private int _framesSinceLastShot = 0;                       //Nombres de frame depuis la dernieres balle tirée
        private int _framesSinceLastMeleeAtack = 0;                 //Nombre de frane depuis la derniere attaque de mélée
        private const int SHOOTING_RELAOD_TIME = 10;                //Temps de rechargement
        private const int MELEE_RELAOD_TIME = 2;                    //Temps de rechargement
        private bool _modeMelee = false;                            //Vrai quand 


        public List<char> KeysPressed { get => keysPressed; set => keysPressed = value; }
        public bool ModeMelee { get => _modeMelee; }

        // Constructeur
        public Joueur(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Cette méthode calcule le nouvel état dans lequel le joueur se trouve après que
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval) 
        {
            _framesSinceLastMeleeAtack++;
            _framesSinceLastShot++;

            //Modifife la position du joueur en fonction de la touche sur laquelle il appuie en ne dépassant pas les limites.
            //la position change en fonction de la vitesse
            if (keysPressed.Contains('W') == true && y > 0)
                y -= SPEED;
            if (keysPressed.Contains('S') == true && y < BattleMap.HEIGHT-PLAYER_HEIGHT)
                y += SPEED;
            if (keysPressed.Contains('A') == true && x > 0)
                x -= SPEED;
            if (keysPressed.Contains('D') == true && x < BattleMap.WIDTH-PLAYER_WIDTH)
                x += SPEED;

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
                if (_framesSinceLastShot >= SHOOTING_RELAOD_TIME)
                {
                    projectileBurger monBurger = new projectileBurger(e.X, e.Y, x, y);
                    BattleMap.allBurgers.Add(monBurger);
                    _framesSinceLastShot = 0;
                }

            }


            if (e.Button == MouseButtons.Right)
            {
                if (_framesSinceLastMeleeAtack > MELEE_RELAOD_TIME)
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
            drawingSpace.Graphics.DrawImage(Resources.drone, x, y, PLAYER_WIDTH, PLAYER_HEIGHT);
            if(_modeMelee == true)
            {
                drawingSpace.Graphics.FillEllipse(new SolidBrush(Color.Purple), x , y , PLAYER_WIDTH, PLAYER_HEIGHT);
            }

            //drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, 5, 25);

        }
        

    }
}
