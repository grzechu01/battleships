namespace BattleshipsApp.Logic
{
    using BattleshipsApp.Domain;
    using System;
    using System.Collections.Generic;

    public interface IGameInitializer
    {
        GameState InitializeGame(int boardWidth, int boardHeight, List<(int shipSize, int numberOfShips)> shipsToAdd);
    }

    public class GameInitializer : IGameInitializer
    {
        private readonly Random _rand;

        public GameInitializer()
        {
            _rand = new Random();
        }

        public GameState InitializeGame(int boardWidth, int boardHeight, List<(int shipSize, int numberOfShips)> shipsToAdd) => new GameState
        {
            Ships = GenerateShips(boardWidth, boardHeight, shipsToAdd),
            MissedShots = new List<Position>()
        };

        private List<Ship> GenerateShips(int boardWidth, int boardHeight, List<(int shipSize, int numberOfShips)> shipsToAdd)
        {
            var result = new List<Ship>();
            var board = new bool[boardWidth, boardHeight];
            foreach (var shipToAdd in shipsToAdd)
            {
                for (int i = 0; i < shipToAdd.numberOfShips; i++)
                {
                    var addShipResult = AddShip(board, shipToAdd.shipSize);
                    board = addShipResult.board;
                    result.Add(addShipResult.ship);
                }
            }

            return result;
        }

        private (bool[,] board, Ship ship) AddShip(bool[,] board, int shipToAddSize)
        {
            var shipIsNotAdded = true;
            var attempts = 0;

            while (shipIsNotAdded && attempts < 100)
            {
                var startingPoint = GetRandomLocation(board.GetUpperBound(0), board.GetUpperBound(1));
                var direction = IsGoingRight();

                if (ShipFits(startingPoint, direction, shipToAddSize, board))
                {
                    return CreateShip(startingPoint, direction, shipToAddSize, board);
                }
                else
                {
                    attempts++;
                }
            }

            return (board, null);
        }

        private (bool[,], Ship) CreateShip((int X, int Y) startingPoint, bool goingRight, int shipToAddSize, bool[,] board)
        {
            var positions = new List<Position>();

            var x = startingPoint.X;
            var y = startingPoint.Y;

            var currentSize = 0;

            while (currentSize < shipToAddSize)
            {
                board[x, y] = true;
                positions.Add(new Position(x, y));
                currentSize++;

                if (goingRight)
                    x++;
                else
                    y++;
            }

            return (board, new Ship(positions));
        }

        private (int X, int Y) GetRandomLocation(int maxX, int maxY) =>
            (_rand.Next(maxX), _rand.Next(maxY));

        private bool IsGoingRight() => _rand.NextDouble() < 0.5;

        private bool ShipFits((int X, int Y) startingPoint, bool isGoingRight, int size, bool[,] board)
        {
            var x = startingPoint.X;
            var y = startingPoint.Y;

            var currentSize = 0;

            while (currentSize < size && x <= board.GetUpperBound(0) && y <= board.GetUpperBound(1))
            {
                if (!board[x, y])
                {
                    currentSize++;
                    if (isGoingRight)
                        x++;
                    else
                        y++;
                }
                else
                {
                    return false;
                }
            }

            return currentSize == size;
        }
    }
}