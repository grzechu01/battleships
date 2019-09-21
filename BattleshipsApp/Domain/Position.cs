namespace BattleshipsApp.Domain
{
    public class Position
    {
        public Position(int x, int y, bool isShot = false)
        {
            X = x;
            Y = y;
            IsShot = isShot;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public bool IsShot { get; set; }
    }
}