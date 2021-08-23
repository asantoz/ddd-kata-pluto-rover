namespace RoverChallenge.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dawn;
    using RoverChallenge.Application.Dtos;
    using RoverChallenge.Domain.Interfaces;
    using RoverChallenge.Domain;

    /// <summary>
    /// Rover application service
    /// </summary>
    public class RoverService : IRoverService
    {
        private readonly IRoverStateRepository _roverStateRepository;

        public RoverService(IRoverStateRepository roverStateRepository)
        {
            Guard.Argument(roverStateRepository, nameof(roverStateRepository)).NotNull();
            this._roverStateRepository = roverStateRepository;
        }

        /// <summary>
        /// Process motion commands
        /// </summary>
        /// <param name="roverId">Rover id</param>
        /// <param name="commands">List of motion commands to execute</param>
        /// <returns></returns>
        public async Task<MotionReport> Process(Guid roverId, ICollection<MotionCommand> commands)
        {
            Guard.Argument(roverId, nameof(roverId)).NotEqual(Guid.Empty);
            Guard.Argument(commands, nameof(commands)).NotNull().NotEmpty();

            // Get current rover state
            var roverState = await this._roverStateRepository.GetById(roverId) ?? throw new RoverStateNotFoundException(roverId);

            var trace = new List<Coordinates>();
            foreach (var command in commands)
            {
                try
                {
                    switch (command)
                    {
                        case MotionCommand.MoveBackward:
                            roverState.MoveBackward();
                            trace.Add(new Coordinates(roverState.CurrentPosition.Coordinates.X, roverState.CurrentPosition.Coordinates.Y));
                            break;
                        case MotionCommand.MoveForward:
                            roverState.MoveForward();
                            trace.Add(new Coordinates(roverState.CurrentPosition.Coordinates.X, roverState.CurrentPosition.Coordinates.Y));
                            break;
                        case MotionCommand.RotateLeft:
                            roverState.RotateLeft();
                            break;
                        case MotionCommand.RotateRight:
                            roverState.RotateRight();
                            break;
                    };
                }
                catch (CollisionDetectionException ex)
                {
                    return MotionReport.CreateMotionReport(trace, new Coordinates(ex.ObstaclePoint.X, ex.ObstaclePoint.Y));
                }
            }

            // Save latest rover state
            await this._roverStateRepository.Save(roverState);

            return MotionReport.CreateMotionReport(trace, null);
        }
    }
}
