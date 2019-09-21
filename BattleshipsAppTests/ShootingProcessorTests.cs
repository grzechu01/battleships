namespace BattleshipsAppTests
{
    using BattleshipsApp;
    using BattleshipsApp.Logic;
    using System.Collections.Generic;
    using Xunit;
    using Shouldly;
    using System;

    public class ShootingProcessorTests
    {
        private readonly IGameInitializer _gameInitializer;

        private readonly IShootingProcessor _shootingProcessor;

        private readonly Random _random;

        public ShootingProcessorTests()
        {
            _gameInitializer = new GameInitializer();
            _shootingProcessor = new ShootingProcessor();
            _random = new Random();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        public void IsGameOver_WhenShipsAreSunk_ShouldEndGame(int numberOfBattleships, int numberOfDestroyers)
        {
            var gameState = _gameInitializer.InitializeGame(Constants.BoardWidth, Constants.BoardHeight, new List<(int shipSize, int numberOfShips)>
            {
                (4, numberOfDestroyers),
                (5, numberOfBattleships)
            });

            foreach (var ship in gameState.Ships)
                foreach (var position in ship.Positions)
                    gameState = _shootingProcessor.Shot(gameState, (position.X, position.Y), out _);

            gameState.IsGameOver().ShouldBeTrue();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        public void IsGameOver_WhenNoShipWasShot_ShouldNotEndGame(int numberOfBattleships, int numberOfDestroyers)
        {
            var gameState = _gameInitializer.InitializeGame(Constants.BoardWidth, Constants.BoardHeight, new List<(int shipSize, int numberOfShips)>
            {
                (4, numberOfDestroyers),
                (5, numberOfBattleships)
            });

            gameState.IsGameOver().ShouldBeFalse();
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(2, 3, 3)]
        [InlineData(4, 7, 10)]
        public void IsGameOver_WhenSomeShipWereShot_ShouldNotEndGame(int numberOfBattleships, int numberOfDestroyers, int numberOfShots)
        {
            var gameState = _gameInitializer.InitializeGame(Constants.BoardWidth, Constants.BoardHeight, new List<(int shipSize, int numberOfShips)>
            {
                (4, numberOfDestroyers),
                (5, numberOfBattleships)
            });

            for (var i = 0; i < numberOfShots; i++)
            {
                var shipToShot = gameState.Ships[_random.Next(numberOfBattleships + numberOfDestroyers - 1)];
                var shotPosition = shipToShot.Positions[_random.Next(shipToShot.Positions.Count - 1)];
                gameState = _shootingProcessor.Shot(gameState, (shotPosition.X, shotPosition.Y), out _);
            }

            gameState.IsGameOver().ShouldBeFalse();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        public void Shot_WhenShipIsHit_ShouldMarkShipAsHit(int numberOfBattleships, int numberOfDestroyers)
        {
            var gameState = _gameInitializer.InitializeGame(Constants.BoardWidth, Constants.BoardHeight, new List<(int shipSize, int numberOfShips)>
            {
                (4, numberOfDestroyers),
                (5, numberOfBattleships)
            });

            var shipToShot = gameState.Ships[_random.Next(gameState.Ships.Count - 1)];
            var shotPosition = shipToShot.Positions[_random.Next(shipToShot.Positions.Count - 1)];
            gameState = _shootingProcessor.Shot(gameState, (shotPosition.X, shotPosition.Y), out _);

            shotPosition.IsShot.ShouldBeTrue();
        }
    }
}
