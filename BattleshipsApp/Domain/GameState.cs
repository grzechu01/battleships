namespace BattleshipsApp.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class GameState
    {
        public List<Ship> Ships { get; set; }

        public List<Position> MissedShots { get; set; }

        public bool IsGameOver() => Ships.All(s => s.IsSunk());
    }
}
