using System;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain
{
    public sealed class Rover : IRover
    {

        /// <summary>
        /// Initialize 
        /// </summary>
        /// <param name="planetMap"></param>
        /// <param name="landingPosition"></param>
        public Rover(NavigationMap planetMap, Position landingPosition)
        {
            this.PlanetMap = planetMap ?? throw new ArgumentNullException(nameof(planetMap));
            this.CurrentPosition = landingPosition;
        }

        public NavigationMap PlanetMap { get; private set; }
        public Position CurrentPosition { get; private set; }

        public void RotateLeft()
        {
            var totalAvailablePoints = Enum.GetValues<CardinalPoint>().Length - 1;
            var nextCardinalPoint = (int)CurrentPosition.Orientation - 1 >= 0 ? (int)CurrentPosition.Orientation - 1 : totalAvailablePoints;
            CurrentPosition = new Position(CurrentPosition.Coordinate, (CardinalPoint)nextCardinalPoint);
        }

        public void RotateRight()
        {
            var totalAvailablePoints = Enum.GetValues<CardinalPoint>().Length - 1;
            var nextCardinalPoint = (int)CurrentPosition.Orientation + 1 > totalAvailablePoints ? 0 : (int)CurrentPosition.Orientation + 1;
            CurrentPosition = new Position(CurrentPosition.Coordinate, (CardinalPoint)nextCardinalPoint);
        }

        public void MoveForward()
        {
            var nextPosition = CurrentPosition.Orientation switch
            {
                CardinalPoint.North => MoveToNorth(),
                CardinalPoint.South => MoveToSouth(),
                CardinalPoint.West => MoveToWest(),
                CardinalPoint.East => MoveToEast(),
                _ => throw new NotImplementedException("Unrecognized Cardinal Point")
            };
            if (PlanetMap.CheckColision(nextPosition.Coordinate))
            {
                throw new CollisionDetectionException(nextPosition.Coordinate);
            }
            CurrentPosition = nextPosition;
        }

        public void MoveBackward()
        {
            var nextPosition = CurrentPosition.Orientation switch
            {
                CardinalPoint.North => MoveToSouth(),
                CardinalPoint.South => MoveToNorth(),
                CardinalPoint.West => MoveToEast(),
                CardinalPoint.East => MoveToWest(),
                _ => throw new NotImplementedException("Unrecognized Cardinal Point")
            };
            if (PlanetMap.CheckColision(nextPosition.Coordinate))
            {
                throw new CollisionDetectionException(nextPosition.Coordinate);
            }
            CurrentPosition = nextPosition;
        }

        private Position MoveToNorth()
        {
            var currentCoordinates = CurrentPosition.Coordinate;
            var coordinates = new Coordinate(currentCoordinates.X, currentCoordinates.Y + 1 > PlanetMap.Height - 1 ? 0 : currentCoordinates.Y + 1);
            return new Position(coordinates, CurrentPosition.Orientation);
        }

        private Position MoveToSouth()
        {
            var currentCoordinates = CurrentPosition.Coordinate;
            var coordinates = new Coordinate(currentCoordinates.X, currentCoordinates.Y - 1 < 0 ? PlanetMap.Height - 1 : currentCoordinates.Y - 1);
            return new Position(coordinates, CurrentPosition.Orientation);
        }


        private Position MoveToEast()
        {
            var currentCoordinates = CurrentPosition.Coordinate;
            var coordinates = new Coordinate(currentCoordinates.X + 1 > PlanetMap.Width - 1 ? 0 : currentCoordinates.X + 1, currentCoordinates.Y);
            return new Position(coordinates, CurrentPosition.Orientation);
        }

        private Position MoveToWest()
        {
            var currentCoordinates = CurrentPosition.Coordinate;
            var coordinates = new Coordinate(currentCoordinates.X - 1 < 0 ? PlanetMap.Width - 1 : currentCoordinates.X - 1, currentCoordinates.Y);
            return new Position(coordinates, CurrentPosition.Orientation);
        }
    }
}