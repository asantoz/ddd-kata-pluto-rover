namespace RoverChallenge.Application.Dtos
{
    /// <summary>
    /// Position coordinates
    /// </summary>
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Position X
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Position Y
        /// </summary>
        public int Y { get; }
    }
}
