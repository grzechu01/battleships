using System;

namespace BattleshipsApp.Logic
{
    public class ApplicationRunner
    {
        private readonly IGameInitializer _gameInitializer;

        private readonly IShootingProcessor _shottingProcessor;

        public ApplicationRunner()
        {
            _gameInitializer = new GameInitializer();
            _shottingProcessor = new ShootingProcessor();
        }

        public void RunGame()
        {
            var gameState = _gameInitializer.InitializeGame(
                Constants.BoardWidth,
                Constants.BoardHeight,
                Constants.ShipsConfigiration);

            ConsoleDecorator.PrintWelcomeMessage();

            while (!gameState.IsGameOver())
            {
                ConsoleDecorator.PrintBoard(gameState);

                var position = ConsoleDecorator.ReadPosition();

                if (ValidatePosition(position))
                {
                    gameState = _shottingProcessor.Shot(gameState, position, out var shotResult);
                    ConsoleDecorator.PrintShotResult(shotResult);
                }
                else
                {
                    ConsoleDecorator.PrintWrongPositions();
                }
            }

            ConsoleDecorator.GameOver();
        }

        private bool ValidatePosition((int X, int Y) position) =>
            position.X < Constants.BoardWidth && position.Y < Constants.BoardHeight;
    }
}
