using NUnit.Framework;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain.Tests
{
    public class MoveBackwardTests
    {
        private NavigationMap map = new(100, 100);

        [Test]
        public void Rover_Pointing_North_Should_Move_Forward_To_North()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(50, 50), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(50, 49), CardinalPoint.North));
        }

        [Test]
        public void Rover_Pointing_South_Should_Move_Forward_To_South()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(50, 50), CardinalPoint.South);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(50, 51), CardinalPoint.South));
        }

        [Test]
        public void Rover_Pointing_West_Should_Move_Forward_To_West()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(50, 50), CardinalPoint.West);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(51, 50), CardinalPoint.West));
        }

        [Test]
        public void Rover_Pointing_East_Should_Move_Forward_To_East()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(50, 50), CardinalPoint.East);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(49, 50), CardinalPoint.East));
        }

        [Test]
        public void Rover_On_North_Pole_On_Move_Forward_Should_Move_To_Other_Edge()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(99, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(new Position(new Coordinate(99, 99), CardinalPoint.North), rover.CurrentPosition);
        }

        [Test]
        public void Rover_On_South_Pole_On_Move_Forward_Should_Move_To_Other_Edge()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(99, 99), CardinalPoint.South);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(new Position(new Coordinate(99, 0), CardinalPoint.South), rover.CurrentPosition);
        }

        [Test]
        public void Rover_On_West_Boundarie_On_Move_Forward_Should_Move_To_Other_Edge()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(99, 99), CardinalPoint.West);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(new Position(new Coordinate(0, 99), CardinalPoint.West), rover.CurrentPosition);
        }

        [Test]
        public void Rover_On_East_Boundarie_On_Move_Forward_Should_Move_To_Other_Edge()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 99), CardinalPoint.East);
            var rover = new Rover(map, landingPoint);

            // Act
            rover.MoveBackward();

            // Assert
            Assert.AreEqual(new Position(new Coordinate(99, 99), CardinalPoint.East), rover.CurrentPosition);
        }
    }
}