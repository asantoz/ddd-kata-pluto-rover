using NUnit.Framework;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain.Tests
{
    public class RoverRotateLeftTests
    {
        private NavigationMap map = new(100, 100);


        [Test]
        public void Rover_Pointing_North_Should_Turn_West_On_Left_Rotation()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.RotateLeft();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(0, 0), CardinalPoint.West));
        }

        [Test]
        public void Rover_Pointing_West_Should_Turn_South_On_Left_Rotation()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.West);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.RotateLeft();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(0, 0), CardinalPoint.South));
        }

        [Test]
        public void Rover_Pointing_South_Should_Turn_East_On_Left_Rotation()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.South);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.RotateLeft();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(0, 0), CardinalPoint.East));
        }

        [Test]
        public void Rover_Pointing_East_Should_Turn_North_On_Left_Rotation()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.East);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.RotateLeft();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(0, 0), CardinalPoint.North));
        }
    }
}