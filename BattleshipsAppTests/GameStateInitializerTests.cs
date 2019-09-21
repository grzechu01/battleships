namespace BattleshipsAppTests
{
    using BattleshipsApp;
    using BattleshipsApp.Domain;
    using BattleshipsApp.Logic;
    using Shouldly;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GameInitializerTests
    {
        private readonly IGameInitializer _gameInitializer;

        public GameInitializerTests()
        {
            _gameInitializer = new GameInitializer();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        public void InitializeGame_WhenProperParametersAreProvided_ShouldInitializeGame(int numberOfBattleships, int numberOfDestroyers)
        {
            var gameState = _gameInitializer.InitializeGame(Constants.BoardWidth, Constants.BoardHeight, new List<(int shipSize, int numberOfShips)>
            {
                (4, numberOfDestroyers),
                (5, numberOfBattleships)
            });

            gameState.Ships.Where(x => x.GetShipType() == ShipType.Battleship).Count()
                .ShouldBe(numberOfBattleships);
            gameState.Ships.Where(x => x.GetShipType() == ShipType.Destroyer).Count()
                .ShouldBe(numberOfDestroyers);

            gameState.IsGameOver().ShouldBeFalse();

            gameState.Ships.All(ship => ship.Positions.All(p => !p.IsShot)).ShouldBeTrue();
        }
    }
}
