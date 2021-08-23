namespace RoverChallenge.Application
{
    using System;

    public class RoverStateNotFoundException : Exception
    {
        public RoverStateNotFoundException(Guid roverId) : base($"Unable to find rover state id {roverId}")
        {
            RoverId = roverId;
        }

        public Guid RoverId { get; }
    }
}
