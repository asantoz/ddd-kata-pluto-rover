using System;

namespace RoverChallenge.Domain.ValueTypes
{
    /// <summary>
    /// Map position point structure
    /// </summary>
    public struct Position : IEquatable<Position>
    {
        /// <summary>
        /// Initialize Position
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="orientation"></param>
        public Position(Coordinate coordinates, CardinalPoint orientation)
        {

            this.Coordinates = coordinates;
            this.Orientation = Enum.IsDefined(orientation) ? orientation : throw new ArgumentException("Invalid Cardinal Point");
        }

        /// <summary>
        /// Current Coordinates
        /// </summary>
        public Coordinate Coordinates { get; private set; }

        /// <summary>
        /// Cardinal orientation
        /// </summary>
        public CardinalPoint Orientation { get; private set; }

        /// <summary>
        /// Compares two positions
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Position other)
        {
            if (this.Coordinates.X != other.Coordinates.X) return false;
            if (this.Coordinates.Y != other.Coordinates.Y) return false;
            if (this.Orientation != other.Orientation) return false;
            return true;
        }
    }
}
