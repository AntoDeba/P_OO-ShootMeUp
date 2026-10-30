using ShootMeUp;

namespace ShootMeUpTests;

[TestClass]
public class ProjectileTests
{
    [TestMethod]
    public void MoneyBagDeplacementTest()
    {
        //Arrange : On créer un prjectile MoneyBag 
        MoneyBag projectile = new MoneyBag(0,0,100, 100, 0);

        //Act : On fait passer un update
        projectile.Update(0);

        //Assert: On regarde si le projectile à bouger
        Assert.IsFalse(projectile.X == 100 || projectile.Y == 100);
    }
    [TestMethod]
    public void FaisceauDeplacementTest()
    {
        //Arrange : On créer un prjectile Faisceau allant vers le nord
        Faisceau projectile = new Faisceau(100, 100, 'N');

        //Act : On fait passer un update
        projectile.Update(0);

        //Assert: On regarde si le projectile à bouger
        Assert.IsTrue(projectile.Y != 100);
    }
    [TestMethod]
    public void BurgerDeplacementTest()
    {
        //Arrange : On créer un projectile ProjectileBurger
        ProjectileBurger projectile = new ProjectileBurger(0, 0, 100, 100);

        //Act : On fait passer un update
        projectile.Update(0);

        //Assert: On regarde si le projectile à bouger
        Assert.IsFalse(projectile.X == 100 || projectile.Y == 100);
    }
}
