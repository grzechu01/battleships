namespace BattleshipsAppTests
{
    using BattleshipsApp.Domain;
    using System.Collections.Generic;
    using Xunit;
    using Shouldly;
    using BattleshipsApp.Logic;

    public class ShipTests
    {
        [Fact]
        public void IsSunk_WhenAllItemsWereHit_ShouldBeSunk()
        {
            var ship = new Ship(new List<Position>
                {
                    new Position(0, 0, true),
                    new Position(0, 1, true),
                    new Position(0, 2, true)
                }
            );

            var actual = ship.IsSunk();

            actual.ShouldBeTrue();
        }

        [Fact]
        public void IsSunk_WhenNotAllItemsWereHit_ShouldNotBeSunk()
        {
            var ship = new Ship(new List<Position>
                {
                    new Position(0, 0, true),
                    new Position(0, 1, false),
                    new Position(0, 2, true)
                }
            );

            var actual = ship.IsSunk();

            actual.ShouldBeFalse();
        }
    }
}
