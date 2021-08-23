using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain.Tests
{
    public class RoverTests
    {
        [Test]
        public void Rover_With_Null_Navigation_Map_Should_Throw_ArgumentNullException()
        {
            // Arrange
            var landingPosition = new Position(new Coordinate(0, 0), CardinalPoint.North);


            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Rover(null, landingPosition));

            // Assert
            Assert.IsNotNull(exception);
        }
    }
}