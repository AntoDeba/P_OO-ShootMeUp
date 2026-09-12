using Drones.Helpers;
using Drones.Properties;

namespace Drones
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Joueur
    {
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_x;                           // Déplacement horizontal
        public int speed_y;

        // Constructeur
        public Joueur(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
            x += speed_x;   
            y += speed_y;

            speed_x = 0;
            speed_y = 0;
        }

        
        public void GoUp()
        {

            if (y >= 10)
                speed_y += -10;
        }
        public void GoDown()
        {
            if (y <= AirSpace.HEIGHT-50)
                speed_y += 10;
        }
        public void GoRight()
        {
            if (x <= AirSpace.WIDTH-50)
                speed_x += 10;
        }
        public void GoLeft()
        {
            if (x >= 10)
                speed_x += -10;
        }
        public void NotHorizontal()
        {
            speed_x = 0;
        }
        public void NotVertical()
        {
            speed_y = 0;
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
            drawingSpace.Graphics.DrawImage(Resources.drone, x, y, 50, 50);
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
