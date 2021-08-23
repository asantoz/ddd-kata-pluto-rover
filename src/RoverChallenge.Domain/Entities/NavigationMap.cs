namespace RoverChallenge.Domain
{
    using System.Collections.Generic;
    using RoverChallenge.Domain.ValueTypes;

    /// <summary>
    /// Rover navigation map
    /// </summary>
    public class NavigationMap
    {
        private readonly ISet<Coordinate> _obstacles;

        /// <summary>
        /// Initalize navigation map
        /// </summary>
        /// <param name="width">Map Width</param>
        /// <param name="height">Map Height</param>
        public NavigationMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            _obstacles = new HashSet<Coordinate>();
        }

        /// <summary>
        /// Initalize navigation map
        /// </summary>
        /// <param name="width">Map Width</param>
        /// <param name="height">Map Height</param>
        /// <param name="obstacles">Map collision obstacles</param>
        public NavigationMap(int width, int height, IEnumerable<Coordinate> obstacles)
        {
            this.Width = width;
            this.Height = height;
            this._obstacles = new HashSet<Coordinate>(obstacles);
        }

        /// <summary>
        /// Map Width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Map Height
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Check if there is an obstacle on coordinate point
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool CheckColision(Coordinate coordinate)
        {
            return _obstacles.Contains(coordinate);
        }
    }
}