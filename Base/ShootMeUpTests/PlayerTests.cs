using ShootMeUp;

namespace ShootMeUpTests;

[TestClass]
public class PlayerTests
{
    [TestInitialize]
    public void Setup()
    {
        BattleMap.allObstacle = new List<Obstacle>();
        BattleMap.allFaisceaux = new List<Faisceau>();
        BattleMap.allRobots = new List<Robot>();
        BattleMap.allManagers = new List<Manager>();
        BattleMap.allBurgers = new List<ProjectileBurger>();
        BattleMap.allMoneyBags = new List<MoneyBag>();
    }

    [TestMethod]
    public void PlayerBurgerAttack()
    {
        //Arrange : On créer un joueur et un clic gauche à coté du joueur.
        Joueur monJoueur = new Joueur(0,0);
        System.Windows.Forms.MouseEventArgs clicGauche = new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Left,1,10,10,0);

        //Act : On simule un clic gauche
        monJoueur.MouseInput(clicGauche);

        //Assert : On regarde si un burger à été créer
        Assert.AreEqual(1, BattleMap.allBurgers.Count);
    }
    
    [TestMethod]
    public void PlayerMeleeAttack()
    {
        //Arrange : On créer un joueur et un clic droit à coté du joueur.
        Joueur monJoueur = new Joueur(0, 0);
        System.Windows.Forms.MouseEventArgs clicDroit = new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, 10, 10, 0);

        //Act
        monJoueur.Update(0);                //On laisse passer une frame pour que le cooldown de départ passe
        monJoueur.MouseInput(clicDroit);    //On simule le clic
        monJoueur.Update(10);               //On relaisse passer une frame pour que le joueur actualise son mode mélée

        //Assert : On regarde si le joueur est en mode melee
        Assert.IsTrue(monJoueur.ModeMelee);
    }

    [TestMethod]
    public void PlayerGoUp()
    {
        //Arrange : On créer un joueur et on enleve les potentiel obstacles
        Joueur monJoueur = new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2);

        //Act, on simule avoir appuyer sur 'W'
        monJoueur.KeysPressed.Add('W');
        monJoueur.Update(0); 


        //Assert : On regarde si le joueur à monter correctement
        Assert.AreEqual(BattleMap.HEIGHT / 2 - Joueur.SPEED,monJoueur.Y);
    }
    
    [TestMethod]
    public void PlayerGoDown()
    {
        //Arrange : On créer un joueur et on enleve les potentiel obstacles
        Joueur monJoueur = new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2);

        //Act, on simule avoir appuyer sur 'S'
        monJoueur.KeysPressed.Add('S');
        monJoueur.Update(0);


        //Assert : On regarde si le joueur à descendu correctement
        Assert.AreEqual(BattleMap.HEIGHT / 2 + Joueur.SPEED, monJoueur.Y);
    }

    [TestMethod]
    public void PlayerGoLeft()
    {
        //Arrange : On créer un joueur et on enleve les potentiel obstacles
        Joueur monJoueur = new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2);

        //Act, on simule avoir appuyer sur 'A'
        monJoueur.KeysPressed.Add('A');
        monJoueur.Update(0);


        //Assert : On regarde si le joueur va correctement à gauche
        Assert.AreEqual(BattleMap.WIDTH / 2 - Joueur.SPEED, monJoueur.X);
    }

    [TestMethod]
    public void PlayerGoRight()
    {
        //Arrange : On créer un joueur et on enleve les potentiel obstacles
        Joueur monJoueur = new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2);

        //Act, on simule avoir appuyer sur 'D'
        monJoueur.KeysPressed.Add('D');
        monJoueur.Update(0);


        //Assert : On regarde si le joueur va correctement à droite
        Assert.AreEqual(BattleMap.WIDTH / 2 + Joueur.SPEED, monJoueur.X);
    }

    [TestMethod]
    public void PlayerCollisionWithObstacle()
    {
        //Arrange : On créer un joueur et un obstacle
        Joueur monJoueur = new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2);
        BattleMap.allObstacle.Add(new Obstacle(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2));

        //Act, on simule avoir appuyer sur 'W' pour avancer vers l'obstacle
        monJoueur.KeysPressed.Add('W');
        monJoueur.Update(0);


        //Assert : On regarde si le joueur à bougé ou non.
        Assert.AreEqual(BattleMap.HEIGHT / 2, monJoueur.Y);
    }

    [TestMethod]
    public void PlayerGoOutOfBounds()
    {
        //Arrange : On créer un joueur au bord de la map
        Joueur monJoueur = new Joueur(BattleMap.WIDTH / 2,0);

        //Act, on simule avoir appuyer sur 'W' pour avancer vers l'obstacle
        monJoueur.KeysPressed.Add('W');
        monJoueur.Update(0);


        //Assert : On regarde si le joueur à bougé ou non.
        Assert.AreEqual(0, monJoueur.Y);
    }
}
