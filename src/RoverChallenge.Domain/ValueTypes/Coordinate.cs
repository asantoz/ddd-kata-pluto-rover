namespace RoverChallenge.Domain.ValueTypes
{
    /// <summary>
    /// Map coordinate structure
    /// </summary>
    public struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Position X
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Position Y
        /// </summary>
        public int Y { get; private set; }
    }
}
