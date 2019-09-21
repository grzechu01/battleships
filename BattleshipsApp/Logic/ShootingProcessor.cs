namespace BattleshipsApp.Logic
{
    using BattleshipsApp.Domain;
    using System.Linq;

    public interface IShootingProcessor
    {
        GameState Shot(GameState state, (int X, int Y) position, out ShotResult shotResult);
    }

    public class ShootingProcessor : IShootingProcessor
    {
        public GameState Shot(GameState state, (int X, int Y) position, out ShotResult shotResult)
        {
            shotResult = ShotResult.Missed;

            if (FindShip(state, position, out var ship))
            {
                SetShipHit(position, ship);

                shotResult = ship.IsSunk()
                    ? ShotResult.Sunk
                    : ShotResult.Hit;
            }
            else if (!state.MissedShots.ContainsPosition(position) && position.X < Constants.BoardWidth && position.Y < Constants.BoardHeight)
                state.MissedShots.Add(new Position(position.X, position.Y, true));

            return state;
        }

        private void SetShipHit((int X, int Y) position, Ship ship)
        {
            var shipPosition = ship.Positions.FirstOrDefault(p => p.X == position.X && p.Y == position.Y);
            if (shipPosition != null)
                shipPosition.IsShot = true;
        }

        private bool FindShip(GameState state, (int X, int Y) position, out Ship ship) =>
            (ship = GetShip(state, position)) != null;

        private Ship GetShip(GameState state, (int X, int Y) position) =>
            state.Ships
                .Where(s => s.Positions.Any(p => p.X == position.X && p.Y == position.Y))
                .FirstOrDefault();
    }
}
