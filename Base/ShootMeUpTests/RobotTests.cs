using ShootMeUp;

namespace ShootMeUpTests;

[TestClass]
public class RobotTests
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
    public void RobotFires()
    {
        //Arrange : On créer un robot et un environnement avec un joueur
        new BattleMap(new Joueur(BattleMap.WIDTH / 2 + 1, BattleMap.HEIGHT / 2 + 1));
        Robot monRobot = new Robot(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2, 1);   
        BattleMap.allObstacle = new List<Obstacle>(); //Créer une liste vide pour pas qu'il y ait d'obstacle

        //Act : On laisse passer le temps qu'il faut à un robot pour tirer
        while (BattleMap.allFaisceaux.Count < 1)
            monRobot.Update(0);
        

        //Assert: On regarde si une attaque de type faisceaux à été ajouté.
        Assert.AreEqual(1, BattleMap.allFaisceaux.Count);
    }
    
    [TestMethod]
    public void RobotMoves()
    {
        //Arrange : On créer un robot et un environnement avec un joueur
        new BattleMap(new Joueur(0, 0));
        Robot monRobot = new Robot(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2, 1);
        BattleMap.allObstacle = new List<Obstacle>(); //Créer une liste vide pour pas qu'il y ait d'obstacle

        //Act : On fait passer un update pour qu'il trouve une nouvelle destination et qu'il avance vers elle
        monRobot.Update(0);

        //Assert: On regarde si le manager à bouger (sa position doit avoir changer)
        Assert.IsFalse(BattleMap.WIDTH / 2 == monRobot.X || BattleMap.HEIGHT / 2 == monRobot.Y);
    }
}
