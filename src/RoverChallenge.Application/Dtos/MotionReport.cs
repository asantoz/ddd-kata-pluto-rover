using System.Linq;
using System.Collections.Generic;
namespace RoverChallenge.Application.Dtos
{
    /// <summary>
    /// Rover motion report for motion command set
    /// </summary>
    public class MotionReport
    {
        private MotionReport(IReadOnlyCollection<Coordinates> tracedRoute,
            Coordinates currentPosition,
            Coordinates detectedObstacle,
            bool stoppedByColisionAlert)
        {
            DetectedObstacle = detectedObstacle;
            TracedRoute = tracedRoute;
            CurrentPosition = currentPosition;
            StoppedByColisionAlert = stoppedByColisionAlert;
        }

        public bool StoppedByColisionAlert { get; }
        public Coordinates DetectedObstacle { get; }
        public IReadOnlyCollection<Coordinates> TracedRoute { get; }
        public Coordinates CurrentPosition { get; }


        /// <summary>
        /// Create motion Report
        /// </summary>
        /// <param name="tracedRoute"></param>
        /// <param name="detectedObstacle"></param>
        /// <returns></returns>
        public static MotionReport CreateMotionReport(IReadOnlyCollection<Coordinates> tracedRoute, Coordinates detectedObstacle)
        {
            return new MotionReport(tracedRoute, tracedRoute.LastOrDefault(), detectedObstacle, detectedObstacle != null);
        }
    }
}
