namespace BattleshipsApp.Logic
{
    using BattleshipsApp.Domain;

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
    }
}
