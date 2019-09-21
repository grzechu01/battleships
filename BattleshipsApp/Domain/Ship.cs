namespace BattleshipsApp.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class Ship
    {
        public Ship(List<Position> positions)
        {
            Positions = positions;
        }

        public List<Position> Positions { get; set; }

        public bool IsSunk() =>
            Positions.All(x => x.IsShot);
    }
}