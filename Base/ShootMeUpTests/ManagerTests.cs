using ShootMeUp;

namespace ShootMeUpTests;

[TestClass]
public class ManagerTests
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
    public void ManagerFires()
    {
        //Arrange : On créer un manager et un environnement avec un joueur
        Manager monManager = new Manager(0, 0, 1);
        new BattleMap(new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2));
        BattleMap.allObstacle = new List<Obstacle>(); //Créer une liste vide pour pas qu'il y ait d'obstacle

        //Act : On laisse passer le temps qu'il faut à un manager pour tirer
        for (int i = 0; i < Manager.FIRE_RATE; i++)
        {
            monManager.Update(i);
        }

        //Assert: On regarde si une attaque de type sac d'argent à été ajouté.
        Assert.AreEqual(1,BattleMap.allMoneyBags.Count);
    }

    [TestMethod]
    public void ManagerMoves()
    {
        //Arrange : On créer un manager et on enleve les potentiels obstacles
        Manager monManager = new Manager(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2, 1);

        //Act : On fait passer un update pour qu'il trouve une nouvelle destination puis une autre pour qu'il avance vers elle
        monManager.Update(0);
        monManager.Update(0);

        //Assert: On regarde si le manager à bouger (sa position doit avoir changer)
        Assert.IsFalse(BattleMap.WIDTH / 2 == monManager.X || BattleMap.HEIGHT / 2 == monManager.Y);
    }
}
