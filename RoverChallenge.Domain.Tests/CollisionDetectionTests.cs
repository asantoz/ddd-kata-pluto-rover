using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain.Tests
{
    public class CollisionDetectionTests
    {
        private NavigationMap map = new(100, 100,new HashSet<Coordinate>(new[] { new Coordinate(0,15) }));

        [Test]
        public void Rover_Pointing_North_Should_Move_Forward_To_North()
        {
            // Arrange
            var landingPoint = new Position(new Coordinate(0, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            // Act
            Assert.Throws<CollisionDetectionException>(() =>Enumerable.Range(0, 15).ToList().ForEach(_ => rover.MoveForward()));

            // Assert
            Assert.AreEqual(rover.CurrentPosition, new Position(new Coordinate(0, 14), CardinalPoint.North));
        }
    }
}