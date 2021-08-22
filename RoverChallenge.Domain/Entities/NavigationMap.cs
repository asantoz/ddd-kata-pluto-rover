namespace RoverChallenge.Domain
{
    using System.Collections.Generic;
    using RoverChallenge.Domain.ValueTypes;

    public class NavigationMap
    {
        private readonly ISet<Coordinate> _obstacles;

        public NavigationMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            _obstacles = new HashSet<Coordinate>();
        }

        public NavigationMap(int width, int height, IEnumerable<Coordinate> obstacles)
        {
            this.Width = width;
            this.Height = height;
            this._obstacles = new HashSet<Coordinate>(obstacles);
        }

        public int Width { get; private set; }
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