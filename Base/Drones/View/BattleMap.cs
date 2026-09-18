using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ShootMeUp
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class BattleMap : Form
    {
        public static readonly int WIDTH = 1200;        // Dimensions of the airspace
        public static readonly int HEIGHT = 600;
        private char currentlyPressedKey;

        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private Joueur _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public BattleMap(Joueur player)
        {
            InitializeComponent();
            ClientSize = new Size(WIDTH, HEIGHT);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.Clear(Color.AliceBlue);

            _player.Render(airspace);

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }
        private void BattleMap_KeyUp(object sender, KeyEventArgs e)
        {
            currentlyPressedKey = Convert.ToChar(e.KeyValue);

            if (_player.KeysPressed.Contains(currentlyPressedKey)) //si la liste contien la touche
            {
                _player.KeysPressed.Remove(currentlyPressedKey); //Retire la touche à la liste
            }
        }
        private void BattleMap_KeyDown(object sender, KeyEventArgs e)
        {
            currentlyPressedKey = Convert.ToChar(e.KeyValue);

            if (!_player.KeysPressed.Contains(currentlyPressedKey)) //si la liste ne contien pas déja la touche
            {
                _player.KeysPressed.Add(currentlyPressedKey); //Ajoute la touche à la liste
            }
        }
    }
}