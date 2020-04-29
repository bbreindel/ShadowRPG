using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GameModels
{
    public class World
    {
        private readonly List<Location> _locations = new List<Location>();
        //$"C:\\Users\\Bill\\Projects\\FIRSTRPG\\FIRSTRPG\\GameEngine\\Images\\Locations\\{imageName}"
        internal void AddLocation(Location location)
        {
            _locations.Add(location);
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in _locations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }

            return null;
        }
    }
}
