namespace RoverChallenge.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IRoverStateRepository
    {
        Task<IRover> GetById(Guid roverId);
        Task Save(IRover roverState);
    }
}
