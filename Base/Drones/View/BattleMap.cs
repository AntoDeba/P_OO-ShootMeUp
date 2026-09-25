using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
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
        private Joueur _player;

        //Tous les projectiles de type burger
        public static List<projectileBurger> allBurgers = new List<projectileBurger>();

        Manager myman = new Manager(40, 40);

        BufferedGraphicsContext currentContext;
        BufferedGraphics battleMap;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public BattleMap(Joueur player)
        {
            InitializeComponent();
            ClientSize = new Size(WIDTH, HEIGHT);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            battleMap = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            battleMap.Graphics.Clear(Color.AliceBlue);

            _player.Render(battleMap);

            foreach (projectileBurger monBurger in allBurgers) //affiches les projectils burgers
                monBurger.Render(battleMap);

            myman.Render(battleMap);

            battleMap.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);
            
            foreach (projectileBurger monBurger in allBurgers) //met à jour les projectils
                monBurger.Update(interval);

            for(int i = allBurgers.Count -1; i >= 0; i--)
            {
                if (allBurgers[i].X > BattleMap.WIDTH || allBurgers[i].Y > BattleMap.HEIGHT || allBurgers[i].X < 0 || allBurgers[i].Y < 0)
                {
                    allBurgers.RemoveAt(i);
                }
            }

            myman.Update(interval);
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
        private void mouseClick(object sender, MouseEventArgs e)
        {
            _player.mouseInput(e);
        }

    }
}