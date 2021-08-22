using System;

namespace RoverChallenge.Domain.ValueTypes
{
    public struct Position : IEquatable<Position>
    {
        public Position(Coordinate coordinate, CardinalPoint orientation)
        {

            this.Coordinate = coordinate;
            this.Orientation = Enum.IsDefined(orientation) ? orientation : throw new ArgumentException("Invalid Cardinal Point");
        }

        public Coordinate Coordinate { get; private set; }
        public CardinalPoint Orientation { get; private set; }

        public bool Equals(Position other)
        {
            if (this.Coordinate.X != other.Coordinate.X) return false;
            if (this.Coordinate.Y != other.Coordinate.Y) return false;
            if (this.Orientation != other.Orientation) return false;
            return true;
        }

        public override string ToString()
        {
            return $"X: {Coordinate.X} , Y: {Coordinate.Y}, Orientation: {Orientation}";
        }
    }
}
