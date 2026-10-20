using ShootMeUp;

namespace ShootMeUpTests;

[TestClass]
public class PlayerTests
{
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
}
