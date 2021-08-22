using NUnit.Framework;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain.Tests
{
    public class DemoTest
    {
        private NavigationMap map = new(100, 100);


        [Test]
        public void Testing()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveForward();
            rover.MoveForward();
            rover.RotateRight();
            rover.MoveForward();
            rover.MoveForward();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(2, 2), CardinalPoint.East));
        }

        [Test]
        public void Testing2()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveForward();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(0, 1), CardinalPoint.North));
        }
    }
}