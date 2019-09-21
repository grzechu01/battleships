namespace BattleshipsApp
{
    using System.Collections.Generic;

    public static class Constants
    {
        public const int BoardWidth = 10;

        public const int BoardHeight = 10;

        public static readonly List<(int shipSize, int numberOfShips)> ShipsConfigiration =
            new List<(int shipSize, int numberOfShips)>
                {
                    (5, 1),
                    (4, 2)
                };
    }
}
