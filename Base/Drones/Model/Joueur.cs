using ShootMeUp.Helpers;
using ShootMeUp.Properties;

namespace ShootMeUp
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Joueur
    {
        private int x;                                           // Position en X depuis la gauche de l'espace aérien
        private int y;                                           // Position en Y depuis le haut de l'espace aérien
        private const int SPEED = 20;                           // Vitesse du joueur
        private const int PLAYER_HEIGHT = 50;
        private const int PLAYER_WIDTH = 50;
        private List<char> keysPressed = new List<char>();  //Liste des touches pressé
        public List<char> KeysPressed { get => keysPressed; set => keysPressed = value; }
        
        
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

            if (keysPressed.Contains('W') == true && y > 0)
                y -= SPEED;
            if (keysPressed.Contains('S') == true && y < BattleMap.HEIGHT-PLAYER_HEIGHT)
                y += SPEED;
            if (keysPressed.Contains('A') == true && x > 0)
                x -= SPEED;
            if (keysPressed.Contains('D') == true && x < BattleMap.WIDTH-PLAYER_WIDTH)
                x += SPEED;
        }

        public void FireBurger(int mouseX, int mouseY)
        {
            projectileBurger monBurger = new projectileBurger(mouseX,mouseY,x,y);
            BattleMap.allBurgers.Add(monBurger);
        }

        /// //////////////////////////////////////////////////////////////////////////////
        //  
        //  Ce qui suit appartient à la vue, pas au modèle.
        //  Il aurait été préférable de séparer la déclaration de la classe Drone en deux,
        //  Nous regroupons tout ici pour simplifier
        //  
        /// //////////////////////////////////////////////////////////////////////////////

        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.drone, x, y, PLAYER_WIDTH, PLAYER_HEIGHT);

            //drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, x + 5, y - 25);
        }

        // De manière textuelle
        
        /*
        public override string ToString()
        {
            return $"{name} ({((int)((double)charge / 1000 * 100)).ToString()}%)";
        }
        */

    }
}
