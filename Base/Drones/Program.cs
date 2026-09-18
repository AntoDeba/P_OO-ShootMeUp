namespace ShootMeUp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            // Démarrage
            Application.Run(new BattleMap(new Joueur(BattleMap.WIDTH / 2, BattleMap.HEIGHT / 2)));
        }
    }
}