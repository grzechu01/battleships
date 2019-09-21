namespace BattleshipsApp.Logic
{
    using BattleshipsApp.Domain;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class ConsoleDecorator
    {
        public static (int X, int Y) ReadPosition()
        {
            Console.WriteLine(Strings.ENTER_POSITION);

            var text = string.Empty;
            while (!Regex.Match(text = Console.ReadLine(), "^[A-Za-z]{1}[0-9]{1}$").Success)
            {
                Console.WriteLine(Strings.WRONG_POSITION_FORMAT);
            }

            Console.Clear();

            return (X: int.Parse(text.Substring(1)), Y: text[0] % 32 - 1);
        }

        public static void PrintWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine(Strings.WELCOME_MESSAGE);
            Console.WriteLine();
        }

        public static void PrintShotResult(ShotResult shotResult)
        {
            switch (shotResult)
            {
                case ShotResult.Hit:
                    Console.WriteLine(Strings.YOU_HIT);
                    break;
                case ShotResult.Missed:
                    Console.WriteLine(Strings.YOU_MISSED);
                    break;
                case ShotResult.Sunk:
                    Console.WriteLine(Strings.SHIP_DESTROYED);
                    break;
            }

            Console.WriteLine();
        }

        public static void PrintWrongPositions()
        {
            Console.WriteLine(string.Format(Strings.WRONG_COORDINATES, Constants.BoardWidth, Constants.BoardHeight));
            Console.WriteLine();
        }

        public static void GameOver() =>
            Console.WriteLine(Strings.GAME_OVER);

        public static void PrintBoard(GameState state)
        {
            var board = new bool?[Constants.BoardWidth, Constants.BoardHeight];
            foreach (var ship in state.Ships)
            {
                foreach (var position in ship.Positions.Where(x => x.IsShot))
                {
                    board[position.X, position.Y] = true;
                }
            }

            foreach (var missedShot in state.MissedShots)
            {
                board[missedShot.X, missedShot.Y] = false;
            }

            Console.Write("   ");

            for (int x = 0; x < Constants.BoardWidth; x++)
            {
                Console.Write(string.Format(Strings.COLUMN_LABEL, (char)(x + 65)));
            }

            Console.WriteLine();

            for (int x = 0; x < Constants.BoardWidth; x++)
            {
                Console.Write(string.Format(Strings.ROW_LABEL, x));

                for (int y = 0; y < Constants.BoardHeight; y++)
                    Console.Write(GetPointText(board[x, y]));

                Console.Write(Strings.LINE_ENDING);

                Console.WriteLine();

            }

            Console.WriteLine(Strings.BOARD_FOOTER);
            Console.WriteLine();
        }

        public static string GetPointText(bool? point)
        {
            switch (point)
            {
                case true: return Strings.HIT_SQUARE;
                case false: return Strings.MISSED_SQUARE;
                default: return Strings.EMPTY_SQUARE;
            }
        }
    }
}
