using ShootMeUp;

namespace ShootMeUpTests
{
    [TestClass]
    public sealed class HelpersTests
    {
        [TestMethod]
        public void TestDistance()
        {
            //Arrange : On créer deux points avec deux coordonné XY chacun
            double x1 = 0;
            double y1 = 0;
            double x2 = 3;
            double y2 = 4;
            double result;


            //Act : on calcule la distance grâce au helper
            result = MathHelper.Distance(x1, y1, x2, y2);

            //Assert : La distance devrait être égal à 5 
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void TestAreTouching()
        {
            //Arrange : On créer deux carrée dont les dimensions les font se superposer
            float x1 = 0;
            float y1 = 0;
            float x2 = 15;
            float y2 = 15;
            int width1 = 15;
            int width2 = 20;
            int height1 = 15;
            int height2 = 20;
            bool result;

            //Act : On appelle la fonction areTouching
            result = MathHelper.AreTouching(x1, x2, y1, y2, width1, width2, height1, height2);

            //Assert: On regarde si la fonction détecte qu'ils se touchent
            Assert.IsTrue(result);
        }
    }
}
