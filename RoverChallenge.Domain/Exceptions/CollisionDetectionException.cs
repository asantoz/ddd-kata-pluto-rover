using System;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain
{
    public class CollisionDetectionException : Exception
    {
        public CollisionDetectionException(Coordinate obstaclePoint) : base($"Detected obstacle on X:{obstaclePoint.X} - Y:{obstaclePoint.Y}")
        {
        }
    }
}
