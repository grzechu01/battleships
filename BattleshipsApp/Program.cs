namespace BattleshipsApp
{
    using BattleshipsApp.Logic;

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new ApplicationRunner();
            runner.RunGame();
        }
    }
}
