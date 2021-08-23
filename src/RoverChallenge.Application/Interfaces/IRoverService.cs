namespace RoverChallenge.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RoverChallenge.Application.Dtos;

    public interface IRoverService
    {
        public Task<MotionReport> Process(Guid roverId, ICollection<MotionCommand> commands);
    }
}