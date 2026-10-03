using ShootMeUp.Helpers;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ShootMeUp
{
    // La classe BattleMap représente le territoire au dessus duquel le joueur peut jouer
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class BattleMap : Form
    {
        public static readonly int WIDTH = 1200;            // Dimensions of the airspace
        public static readonly int HEIGHT = 600;
        private char currentlyPressedKey;
        private static Joueur _player;
        
        private const int NUMBER_OF_MANAGERS = 7;           //Nombre d'ennemie manager qu'il doit en tout temps avoir sur la carte
        private const int LEVEL_TWO_THRESHHOLD = 100000;    //Argent à partir duquel les ennemis seront de niveau deux
        private const int LEVEL_THREE_THRESHHOLD = 500000;  //Argent à partir duquel les ennemis seront de niveau trois
        private int _currentLevel;


        //Tous les projectiles de type burger
        public static List<projectileBurger> allBurgers = new List<projectileBurger>();

        public static List<moneyBag> allMoneyBags = new List<moneyBag>();

        public static List<Manager> allManagers = new List<Manager>();


        BufferedGraphicsContext currentContext;
        BufferedGraphics battleMap;

        public static Joueur Player { get => _player;}

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
            _player = player;


            

        }   

        // Affichage de la situation actuelle
        private void Render()
        {
            battleMap.Graphics.Clear(Color.AliceBlue);

            foreach (projectileBurger monBurger in allBurgers) //affiches les projectils burgers
                monBurger.Render(battleMap);

            foreach(Manager manager in allManagers)
                manager.Render(battleMap);

            foreach (moneyBag moneyBag in allMoneyBags)
                moneyBag.Render(battleMap);

            _player.Render(battleMap);

            battleMap.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);

            foreach (projectileBurger monBurger in allBurgers) //met à jour les projectils burger
                monBurger.Update(interval);

            foreach (moneyBag moneyBag in allMoneyBags)
                moneyBag.Update(interval);

            foreach (Manager manager in allManagers)
                manager.Update(interval);

            if (_player.Money < LEVEL_TWO_THRESHHOLD)
            {
                _currentLevel = 1;
            }
            else if(_player.Money >=  LEVEL_TWO_THRESHHOLD && _player.Money < LEVEL_THREE_THRESHHOLD)
            {
                _currentLevel = 2;
            }
            else
            {
                _currentLevel = 3;
            }

            while (allManagers.Count < NUMBER_OF_MANAGERS)
            {
                int i = randomValueHelper.Alea.Next(5);
                if (i == 0)
                    allManagers.Add(new Manager(0, randomValueHelper.Alea.Next(BattleMap.HEIGHT), _currentLevel));
                if (i == 1)
                    allManagers.Add(new Manager(BattleMap.WIDTH, randomValueHelper.Alea.Next(BattleMap.HEIGHT), _currentLevel));
                if (i == 3)
                    allManagers.Add(new Manager(randomValueHelper.Alea.Next(BattleMap.WIDTH), 0, _currentLevel));
                if (i == 4)
                    allManagers.Add(new Manager(randomValueHelper.Alea.Next(BattleMap.WIDTH), BattleMap.HEIGHT, _currentLevel));
            }
            

            for (int i = allBurgers.Count -1; i >= 0; i--)
            {
                if (allBurgers[i].X > BattleMap.WIDTH || allBurgers[i].Y > BattleMap.HEIGHT || allBurgers[i].X < 0 || allBurgers[i].Y < 0)
                {
                    allBurgers.RemoveAt(i);
                    continue;
                }
                
                //Vérifie si un manager à été touché par un burger
                for (int j = allManagers.Count -1; j >= 0; j--)
                {
                    if (mathHelper.areTouching(allManagers[j].X, allBurgers[i].X, allManagers[j].Y, allBurgers[i].Y, projectileBurger.BURGER_WIDTH, Manager.MANAGER_WIDTH, projectileBurger.BURGER_HEIGHT, Manager.MANAGER_HEIGHT))
                    {
                        allBurgers.RemoveAt(i);
                        allManagers[j].Health -= projectileBurger.BURGER_DAMAGE;
                        if (allManagers[j].Health <= 0)
                        {
                            _player.Money += allManagers[j].Loot;
                            allManagers.RemoveAt(j);
                        }
                        break;
                    }
                }
            }
            
            for (int j = allManagers.Count - 1; j >= 0; j--)
            {
                if (mathHelper.areTouching(allManagers[j].X, _player.X, allManagers[j].Y, _player.Y, Joueur.PLAYER_WIDTH + Joueur.MELEE_RANGE, Manager.MANAGER_WIDTH, Joueur.PLAYER_HEIGHT + Joueur.MELEE_RANGE, Manager.MANAGER_HEIGHT) && _player.ModeMelee)
                {
                    allManagers[j].Health -= Joueur.MELEE_DAMAGE;
                    if (allManagers[j].Health <= 0)
                    {
                        _player.Money += allManagers[j].Loot;
                        allManagers.RemoveAt(j);
                    }

                }
            }


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