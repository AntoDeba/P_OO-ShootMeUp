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
        public static readonly int WIDTH = 1200;            // Hauteur de la map
        public static readonly int HEIGHT = 600;            //largeur de la map
        private char currentlyPressedKey;
        private static Joueur _player;

        private bool _removed;                              //Quand on détecte les collisions entre un projectile burger et quelque chose d'autre, on détruit le burger et ceci devient vrai

        private const int NUMBER_OF_OBSTACLES = 5;
        private const int NUMBER_OF_ROBOTS = 4;             //Nombre d'ennemie robot qu'il doit en tout temps avoir sur la carte à partir du niveau 2
        private const int NUMBER_OF_MANAGERS = 7;           //Nombre d'ennemie manager qu'il doit en tout temps avoir sur la carte
        private const int LEVEL_TWO_THRESHHOLD = 100000;      //Argent à partir duquel les ennemis seront de niveau deux
        private const int LEVEL_THREE_THRESHHOLD = 500000;  //Argent à partir duquel les ennemis seront de niveau trois
        private int _currentLevel;


        //Tous les projectiles de type burger
        public static List<ProjectileBurger> allBurgers = new List<ProjectileBurger>();

        public static List<MoneyBag> allMoneyBags = new List<MoneyBag>();

        public static List<Manager> allManagers = new List<Manager>();
        public static List<Robot> allRobots = new List<Robot>();
        public static List<Faisceau> allFaisceaux = new List<Faisceau>();
        public static List<Obstacle> allObstacle = new List<Obstacle>();


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

            //Fait apparaitre les obstacles et les suprime tant que il y a des chevauchement
            while (allObstacle.Count < NUMBER_OF_OBSTACLES)
            {
                allObstacle.Add(new Obstacle(RandomValueHelper.alea.Next(Obstacle.OBSTACLE_WIDTH,BattleMap.WIDTH - Obstacle.OBSTACLE_WIDTH), RandomValueHelper.alea.Next(Obstacle.OBSTACLE_HEIGHT,BattleMap.HEIGHT - Obstacle.OBSTACLE_WIDTH)));
                for (int i = allObstacle.Count - 2; i >= 0; i--)
                {
                    if (MathHelper.AreTouching(allObstacle[allObstacle.Count - 1].X, allObstacle[i].X, allObstacle[allObstacle.Count - 1].Y, allObstacle[i].Y, Obstacle.OBSTACLE_WIDTH + Joueur.PLAYER_WIDTH, Obstacle.OBSTACLE_WIDTH + Joueur.PLAYER_WIDTH, Obstacle.OBSTACLE_HEIGHT + Joueur.PLAYER_HEIGHT, Obstacle.OBSTACLE_HEIGHT + Joueur.PLAYER_HEIGHT))
                    {
                        allObstacle.RemoveAt(allObstacle.Count - 1);
                        break;
                    }
                }
            }

        }   

        // Affichage de la situation actuelle
        private void Render()
        {
            battleMap.Graphics.Clear(Color.AliceBlue);

            foreach (Obstacle obstacle in allObstacle)
                obstacle.Render(battleMap);

            foreach (ProjectileBurger monBurger in allBurgers) //affiches les projectils burgers
                monBurger.Render(battleMap);

            foreach(Manager manager in allManagers)
                manager.Render(battleMap);

            foreach (MoneyBag moneyBag in allMoneyBags)
                moneyBag.Render(battleMap);

            foreach (Robot robot in allRobots)
                robot.Render(battleMap);

            foreach (Faisceau faisceau in allFaisceaux)
                faisceau.Render(battleMap);



            _player.Render(battleMap);

            battleMap.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);

            foreach (ProjectileBurger monBurger in allBurgers) //met à jour les projectils burger
                monBurger.Update(interval);

            foreach (MoneyBag moneyBag in allMoneyBags)
                moneyBag.Update(interval);

            foreach (Manager manager in allManagers)
                manager.Update(interval);
            
            foreach (Robot robot in allRobots)
                robot.Update(interval);

            foreach (Faisceau faisceau in allFaisceaux)
                faisceau.Update(interval);

            foreach (Obstacle obstacle in allObstacle)
                obstacle.Update(interval);

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
                int i = RandomValueHelper.alea.Next(5);
                if (i == 0)
                    allManagers.Add(new Manager(0, RandomValueHelper.alea.Next(BattleMap.HEIGHT), _currentLevel));
                if (i == 1)
                    allManagers.Add(new Manager(BattleMap.WIDTH, RandomValueHelper.alea.Next(BattleMap.HEIGHT), _currentLevel));
                if (i == 3)
                    allManagers.Add(new Manager(RandomValueHelper.alea.Next(BattleMap.WIDTH), 0, _currentLevel));
                if (i == 4)
                    allManagers.Add(new Manager(RandomValueHelper.alea.Next(BattleMap.WIDTH), BattleMap.HEIGHT, _currentLevel));
            }

            while (allRobots.Count < NUMBER_OF_ROBOTS && _currentLevel > 1)
            {
                int i = RandomValueHelper.alea.Next(5);
                if (i == 0)
                    allRobots.Add(new Robot(0, RandomValueHelper.alea.Next(BattleMap.HEIGHT), _currentLevel));
                if (i == 1)
                    allRobots.Add(new Robot(BattleMap.WIDTH, RandomValueHelper.alea.Next(BattleMap.HEIGHT), _currentLevel));
                if (i == 3)
                    allRobots.Add(new Robot(RandomValueHelper.alea.Next(BattleMap.WIDTH), 0, _currentLevel));
                if (i == 4)
                    allRobots.Add(new Robot(RandomValueHelper.alea.Next(BattleMap.WIDTH), BattleMap.HEIGHT, _currentLevel));
            }


            for (int i = allBurgers.Count -1; i >= 0; i--)
            {
                _removed = false; //Deviens vrai quand il y a une collision

                if (allBurgers[i].X > BattleMap.WIDTH || allBurgers[i].Y > BattleMap.HEIGHT || allBurgers[i].X < 0 || allBurgers[i].Y < 0)
                {
                    allBurgers.RemoveAt(i);
                    continue;
                }
                
                //Vérifie si un manager à été touché par un burger
                for (int j = allManagers.Count -1; j >= 0; j--)
                {
                    if (MathHelper.AreTouching(allManagers[j].X, allBurgers[i].X, allManagers[j].Y, allBurgers[i].Y, ProjectileBurger.BURGER_WIDTH, Manager.MANAGER_WIDTH, ProjectileBurger.BURGER_HEIGHT, Manager.MANAGER_HEIGHT))
                    {
                        allBurgers.RemoveAt(i);
                        allManagers[j].Health -= ProjectileBurger.BURGER_DAMAGE;
                        _removed = true;
                        if (allManagers[j].Health <= 0)
                        {
                            _player.Money += allManagers[j].Loot;
                            allManagers.RemoveAt(j);
                        }
                        break;
                    }
                }

                //Si le burger à été detruit, on passe au suivant 
                if(_removed == true)
                    continue;

                //Vérifie si un robot à été touché par un burger
                for (int j = allRobots.Count - 1; j >= 0; j--)
                {
                    if (MathHelper.AreTouching(allRobots[j].X, allBurgers[i].X, allRobots[j].Y, allBurgers[i].Y, ProjectileBurger.BURGER_WIDTH, Robot.ROBOT_WIDTH, ProjectileBurger.BURGER_HEIGHT, Robot.ROBOT_HEIGHT))
                    {
                        allBurgers.RemoveAt(i);
                        allRobots[j].Health -= ProjectileBurger.BURGER_DAMAGE;
                        _removed = true;
                        if (allRobots[j].Health <= 0)
                        {
                            allRobots.RemoveAt(j);
                        }
                        break;
                    }
                }

                if (_removed == true)
                    continue;

                //Regarde si une collision avec un obstacle à eu lieu
                for (int j = allObstacle.Count - 1; j >= 0; j--)
                {
                    if (MathHelper.AreTouching(allBurgers[i].X, allObstacle[j].X, allBurgers[i].Y, allObstacle[j].Y, Obstacle.OBSTACLE_WIDTH, ProjectileBurger.BURGER_WIDTH, Obstacle.OBSTACLE_HEIGHT, ProjectileBurger.BURGER_HEIGHT))
                    {
                        allBurgers.RemoveAt(i);
                        break;
                    }
                }
            }
            
            for (int j = allManagers.Count - 1; j >= 0; j--)
            {
                if (MathHelper.AreTouching(allManagers[j].X, _player.X, allManagers[j].Y, _player.Y, Joueur.PLAYER_WIDTH + Joueur.MELEE_RANGE, Manager.MANAGER_WIDTH, Joueur.PLAYER_HEIGHT + Joueur.MELEE_RANGE, Manager.MANAGER_HEIGHT) && _player.ModeMelee)
                {
                    allManagers[j].Health -= Joueur.MELEE_DAMAGE;
                    if (allManagers[j].Health <= 0)
                    {
                        _player.Money += allManagers[j].Loot;
                        allManagers.RemoveAt(j);
                    }

                }
            }

            for (int j = allRobots.Count - 1; j >= 0; j--)
            {
                if (MathHelper.AreTouching(allRobots[j].X, _player.X, allRobots[j].Y, _player.Y, Joueur.PLAYER_WIDTH + Joueur.MELEE_RANGE, Robot.ROBOT_WIDTH, Joueur.PLAYER_HEIGHT + Joueur.MELEE_RANGE, Robot.ROBOT_HEIGHT) && _player.ModeMelee)
                {
                    allRobots[j].Health -= Joueur.MELEE_DAMAGE;
                    if (allRobots[j].Health <= 0)
                    {
                        allRobots.RemoveAt(j);
                    }

                }
            }

            for (int i = allMoneyBags.Count - 1; i >= 0; i--)
            {
                if (allMoneyBags[i].X > BattleMap.WIDTH || allMoneyBags[i].Y > BattleMap.HEIGHT || allMoneyBags[i].X < 0 || allMoneyBags[i].Y < 0)
                {
                    allMoneyBags.RemoveAt(i);
                    continue;
                }

                //Vérifie si le joueur à été touché par un sac d'argent
                if (MathHelper.AreTouching(_player.X, allMoneyBags[i].X, _player.Y, allMoneyBags[i].Y, Joueur.PLAYER_WIDTH, MoneyBag.MONEY_BAG_WIDTH, Joueur.PLAYER_HEIGHT, MoneyBag.MONEY_BAG_HEIGHT))
                {
                    _player.Health -= allMoneyBags[i].Damage;
                    allMoneyBags.RemoveAt(i);
                    continue;
                }

                //Regarde si une collision avec un obstacle à eu lieu
                for (int j = allObstacle.Count - 1; j >= 0; j--)
                {
                    if (MathHelper.AreTouching(allMoneyBags[i].X, allObstacle[j].X, allMoneyBags[i].Y, allObstacle[j].Y, Obstacle.OBSTACLE_WIDTH, MoneyBag.MONEY_BAG_WIDTH, Obstacle.OBSTACLE_HEIGHT, MoneyBag.MONEY_BAG_WIDTH))
                    {
                        allMoneyBags.RemoveAt(i);
                        break;
                    }
                }
            }

            for (int i = allFaisceaux.Count - 1; i >= 0; i--)
            {
                if (allFaisceaux[i].X > BattleMap.WIDTH || allFaisceaux[i].Y > BattleMap.HEIGHT || allFaisceaux[i].X < 0 || allFaisceaux[i].Y < 0)
                {
                    allFaisceaux.RemoveAt(i);
                    continue;
                }

                //Vérifie si le joueur à été touché par un vaisseau. On doit prendre en compte le fait que la forme du faisceau peut être de deux manière différente en fonction de son orientation
                if ((MathHelper.AreTouching(_player.X, allFaisceaux[i].X, _player.Y, allFaisceaux[i].Y, Joueur.PLAYER_WIDTH, Faisceau.BEAM_WIDTH, Joueur.PLAYER_HEIGHT, Faisceau.BEAM_HEIGHT) && (allFaisceaux[i].Direction == 'O' || allFaisceaux[i].Direction == 'E'))
                    || (MathHelper.AreTouching(_player.X, allFaisceaux[i].X, _player.Y, allFaisceaux[i].Y, Joueur.PLAYER_WIDTH, Faisceau.BEAM_HEIGHT, Joueur.PLAYER_HEIGHT, Faisceau.BEAM_WIDTH) && (allFaisceaux[i].Direction == 'N' || allFaisceaux[i].Direction == 'S')))
                {
                    _player.Health -= Faisceau.BEAM_DAMAGE;
                    allFaisceaux.RemoveAt(i);
                    continue;
                }

                //Regarde si une collision avec un obstacle à eu lieu
                for (int j = allObstacle.Count - 1; j >= 0; j--)
                {
                    if ((MathHelper.AreTouching(allObstacle[j].X, allFaisceaux[i].X, allObstacle[j].Y, allFaisceaux[i].Y, Obstacle.OBSTACLE_WIDTH, Faisceau.BEAM_WIDTH, Obstacle.OBSTACLE_HEIGHT, Faisceau.BEAM_HEIGHT) && (allFaisceaux[i].Direction == 'O' || allFaisceaux[i].Direction == 'E'))
                        || (MathHelper.AreTouching(allObstacle[j].X, allFaisceaux[i].X, allObstacle[j].Y, allFaisceaux[i].Y, Obstacle.OBSTACLE_WIDTH, Faisceau.BEAM_HEIGHT, Obstacle.OBSTACLE_HEIGHT, Faisceau.BEAM_WIDTH) && (allFaisceaux[i].Direction == 'N' || allFaisceaux[i].Direction == 'S')))
                    {
                        allFaisceaux.RemoveAt(i);
                        break;
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
        private void BattleMapKeyUp(object sender, KeyEventArgs e)
        {
            currentlyPressedKey = Convert.ToChar(e.KeyValue);

            if (_player.KeysPressed.Contains(currentlyPressedKey)) //si la liste contien la touche
            {
                _player.KeysPressed.Remove(currentlyPressedKey); //Retire la touche à la liste
            }
        }
        private void BattleMapKeyDown(object sender, KeyEventArgs e)
        {
            
            currentlyPressedKey = Convert.ToChar(e.KeyValue);

            if (!_player.KeysPressed.Contains(currentlyPressedKey)) //si la liste ne contien pas déja la touche
            {
                _player.KeysPressed.Add(currentlyPressedKey); //Ajoute la touche à la liste
            }


        }
        private void BattleMapMouseClick(object sender, MouseEventArgs e)
        {
            _player.MouseInput(e);
        }

    }
}