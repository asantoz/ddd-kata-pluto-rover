using System;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain
{
    /// <summary>
    /// Rover aggregate instance class
    /// </summary>
    public sealed class Rover : IRover
    {

        /// <summary>
        /// Initialize 
        /// </summary>
        /// <param name="planetMap"></param>
        /// <param name="landingPosition"></param>
        public Rover(NavigationMap planetMap, Position landingPosition)
        {
            this.Id = new Guid();
            this.PlanetMap = planetMap ?? throw new ArgumentNullException(nameof(planetMap));
            this.CurrentPosition = landingPosition;
        }

        /// <summary>
        /// Rover identification
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Rover Navigation Map
        /// </summary>
        public NavigationMap PlanetMap { get; private set; }

        /// <summary>
        /// Current Position
        /// </summary>
        public Position CurrentPosition { get; private set; }


        /// <summary>
        /// Execute Rotate left operation
        /// </summary>
        public void RotateLeft()
        {
            var totalAvailablePoints = Enum.GetValues<CardinalPoint>().Length - 1;
            var nextCardinalPoint = (int)CurrentPosition.Orientation - 1 >= 0 ? (int)CurrentPosition.Orientation - 1 : totalAvailablePoints;
            CurrentPosition = new Position(CurrentPosition.Coordinates, (CardinalPoint)nextCardinalPoint);
        }

        /// <summary>
        /// Execute rotate right operation
        /// </summary>
        public void RotateRight()
        {
            var totalAvailablePoints = Enum.GetValues<CardinalPoint>().Length - 1;
            var nextCardinalPoint = (int)CurrentPosition.Orientation + 1 > totalAvailablePoints ? 0 : (int)CurrentPosition.Orientation + 1;
            CurrentPosition = new Position(CurrentPosition.Coordinates, (CardinalPoint)nextCardinalPoint);
        }

        /// <summary>
        /// Execute move forward operation
        /// </summary>
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
            if (PlanetMap.CheckColision(nextPosition.Coordinates))
            {
                throw new CollisionDetectionException(nextPosition.Coordinates);
            }
            CurrentPosition = nextPosition;
        }

        /// <summary>
        /// Execute move backward operation
        /// </summary>
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
            if (PlanetMap.CheckColision(nextPosition.Coordinates))
            {
                throw new CollisionDetectionException(nextPosition.Coordinates);
            }
            CurrentPosition = nextPosition;
        }

        /// <summary>
        /// Calculate position to North
        /// </summary>
        /// <returns>Next position</returns>
        private Position MoveToNorth()
        {
            var currentCoordinates = CurrentPosition.Coordinates;
            var coordinates = new Coordinate(currentCoordinates.X, currentCoordinates.Y + 1 > PlanetMap.Height - 1 ? 0 : currentCoordinates.Y + 1);
            return new Position(coordinates, CurrentPosition.Orientation);
        }

        /// <summary>
        /// Calculate position to South
        /// </summary>
        /// <returns>Next position</returns>
        private Position MoveToSouth()
        {
            var currentCoordinates = CurrentPosition.Coordinates;
            var coordinates = new Coordinate(currentCoordinates.X, currentCoordinates.Y - 1 < 0 ? PlanetMap.Height - 1 : currentCoordinates.Y - 1);
            return new Position(coordinates, CurrentPosition.Orientation);
        }

        /// <summary>
        /// Calculate position to East
        /// </summary>
        /// <returns>Next position</returns>
        private Position MoveToEast()
        {
            var currentCoordinates = CurrentPosition.Coordinates;
            var coordinates = new Coordinate(currentCoordinates.X + 1 > PlanetMap.Width - 1 ? 0 : currentCoordinates.X + 1, currentCoordinates.Y);
            return new Position(coordinates, CurrentPosition.Orientation);
        }

        /// <summary>
        /// Calculate position to West
        /// </summary>
        /// <returns></returns>
        private Position MoveToWest()
        {
            var currentCoordinates = CurrentPosition.Coordinates;
            var coordinates = new Coordinate(currentCoordinates.X - 1 < 0 ? PlanetMap.Width - 1 : currentCoordinates.X - 1, currentCoordinates.Y);
            return new Position(coordinates, CurrentPosition.Orientation);
        }
    }
}