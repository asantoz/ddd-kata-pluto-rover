namespace RoverChallenge.Application.Tests
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using RoverChallenge.Application.Dtos;
    using RoverChallenge.Domain;
    using RoverChallenge.Domain.Interfaces;
    using RoverChallenge.Domain.ValueTypes;
    using System.Linq;

    public class RoverServiceTests
    {

        [Test]
        public void Rover_Service_Null_State_Repository_Should_Throw_ArgumentNullException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new RoverService(null));

            // Assert
            Assert.AreEqual("roverStateRepository", exception.ParamName);
        }

        [Test]
        public void Rover_Process_With_Invalid_Guid_Should_Throw_ArgumentException()
        {
            // Arrange
            var repository = new Mock<IRoverStateRepository>();
            var service = new RoverService(repository.Object);

            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => service.Process(Guid.Empty, null));

            // Assert
            Assert.AreEqual("roverId", exception.ParamName);
        }

        [Test]
        public void Rover_Process_With_Null_Commands_Should_Throw_ArgumentException()
        {
            // Arrange
            var repository = new Mock<IRoverStateRepository>();
            var service = new RoverService(repository.Object);

            // Act
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => service.Process(Guid.NewGuid(), null));

            // Assert
            Assert.AreEqual("commands", exception.ParamName);
        }

        [Test]
        public void Rover_Process_With_Empty_Commands_Should_Throw_ArgumentException()
        {
            // Arrange
            var repository = new Mock<IRoverStateRepository>();
            var service = new RoverService(repository.Object);

            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => service.Process(Guid.NewGuid(), Array.Empty<MotionCommand>()));

            // Assert
            Assert.AreEqual("commands", exception.ParamName);
        }

        [Test]
        public void Rover_Process_With_No_Rover_State_Should_Throw_RoverStateNotFoundException()
        {
            // Arrange
            var repository = new Mock<IRoverStateRepository>();
            var roverId = Guid.NewGuid();
            repository.Setup(x => x.GetById(roverId)).ReturnsAsync(() => null);
            var service = new RoverService(repository.Object);

            // Act
            var exception = Assert.ThrowsAsync<RoverStateNotFoundException>(() => service.Process(roverId, new[] { MotionCommand.MoveForward }));

            // Assert
            Assert.AreEqual(roverId, exception.RoverId);
        }

        [Test]
        public async Task Rover_Process_With_No_Collisions_Should_Return_Motion_ReportAsync()
        {
            // Arrange
            var roverId = Guid.NewGuid();
            var repository = new Mock<IRoverStateRepository>();
            var rover = new Mock<IRover>();

            repository.Setup(x => x.GetById(roverId)).ReturnsAsync(() => rover.Object);
            var service = new RoverService(repository.Object);

            // Act
            var report = await service.Process(roverId, new[] { MotionCommand.MoveForward, MotionCommand.MoveBackward, MotionCommand.RotateLeft });

            // Assert
            Assert.NotNull(report);
            Assert.AreEqual(2, report.TracedRoute.Count);
            Assert.AreEqual(false, report.StoppedByColisionAlert);
            rover.Verify(x => x.MoveForward(), Times.Once);
            rover.Verify(x => x.MoveBackward(), Times.Once);
        }

        [Test]
        public async Task Rover_Process_Simple_Route_Should_Return_Last_Expected_Position()
        {
            // Arrange
            var roverId = Guid.NewGuid();
            var map = new NavigationMap(100, 100);
            var landingPoint = new Position(new Domain.ValueTypes.Coordinate(0, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            var repository = new Mock<IRoverStateRepository>();
            repository.Setup(x => x.GetById(roverId)).ReturnsAsync(() => rover);
            var service = new RoverService(repository.Object);

            // Act
            var report = await service.Process(roverId, new[] {
                MotionCommand.MoveForward,
                MotionCommand.MoveForward,
                MotionCommand.RotateRight,
                MotionCommand.MoveForward,
                MotionCommand.MoveForward,
            });

            Assert.NotNull(report);
            Assert.AreEqual(rover.CurrentPosition, new Position(new Domain.ValueTypes.Coordinate(2, 2), CardinalPoint.East));
        }

        [Test]
        public async Task Rover_Process_Route_With_Collision_Should_Return_Last_Expected_Position_And_Collision_Point()
        {
            // Arrange
            var roverId = Guid.NewGuid();
            var collisionPoints = new[] { new Domain.ValueTypes.Coordinate(1, 2) };
            var map = new NavigationMap(100, 100, collisionPoints);
            var landingPoint = new Position(new Domain.ValueTypes.Coordinate(0, 0), CardinalPoint.North);
            var rover = new Rover(map, landingPoint);

            var repository = new Mock<IRoverStateRepository>();
            repository.Setup(x => x.GetById(roverId)).ReturnsAsync(() => rover);
            var service = new RoverService(repository.Object);

            // Act
            var report = await service.Process(roverId, new[] {
                MotionCommand.MoveForward,
                MotionCommand.MoveForward,
                MotionCommand.RotateRight,
                MotionCommand.MoveForward,
                MotionCommand.MoveForward,
            });

            // Assert
            Assert.NotNull(report);

            // Assert detected obstacle
            Assert.NotNull(report.DetectedObstacle);
            Assert.AreEqual(true, report.StoppedByColisionAlert);
            Assert.AreEqual(collisionPoints.First().X, report.DetectedObstacle.X);
            Assert.AreEqual(collisionPoints.First().Y, report.DetectedObstacle.Y);

            // Assert latest position
            Assert.AreEqual(0,report.CurrentPosition.X);
            Assert.AreEqual(2, report.CurrentPosition.Y);
        }
    }
}