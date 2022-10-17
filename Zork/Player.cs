using System;

namespace Zork
{
    public class Player
    {
        public Room CurrentRoom
        {
            get
            {
                return null;
            }
        }

        public Player(World world, string startingLocation)
        {
            _world = world;

            //for (int row = 0; row < _world.Rooms.GetLength(0); row++)
            //{
            //    for (int column = 0; column < _world.Rooms.GetLength(1); column++)
            //    {
            //        Room room = _world.Rooms[row, column];
            //        if (string.Compare(room.Name, startingLocation, ignoreCase: true) == 0)
            //        {
            //            _location = (row, column);
            //            return;
            //        }
            //    }
            //}
            //throw new Exception($"Invalid starting location: {startingLocation}");
        }

        private World _world;
    }
}
