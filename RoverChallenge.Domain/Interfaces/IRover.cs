namespace RoverChallenge.Domain
{
    public interface IRover
    {
        void MoveBackward();
        void MoveForward();
        void RotateLeft();
        void RotateRight();
    }
}