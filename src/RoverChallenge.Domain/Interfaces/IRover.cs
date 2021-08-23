using System;
using RoverChallenge.Domain.ValueTypes;

namespace RoverChallenge.Domain
{
    public interface IRover
    {
        public Guid Id { get; }
        public NavigationMap PlanetMap { get; }
        public Position CurrentPosition { get; }

        void MoveBackward();
        void MoveForward();
        void RotateLeft();
        void RotateRight();
    }
}