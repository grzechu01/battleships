namespace BattleshipsApp.Logic
{
    using BattleshipsApp.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public static class ShipExtensions
    {
        public static ShipType GetShipType(this Ship ship)
        {
            switch (ship.Positions.Count)
            {
                case 5:
                    return ShipType.Battleship;
                case 4:
                    return ShipType.Destroyer;
                default:
                    throw new System.Exception($"Unknown ship, size: {ship.Positions.Count}");
            }
        }

        public static bool ContainsPosition(this List<Position> existingPositions, (int X, int Y) position) =>
            existingPositions.Any(p => p.X == position.X && p.Y == position.Y);
    }
}
