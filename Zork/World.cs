using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Zork
{
    public class World
    {
        public Room[] Rooms { get; }

        public World(Room[] rooms)
        {
            Rooms = rooms;
            RoomsByName = new Dictionary<string, Room>(StringComparer.OrdinalIgnoreCase);
            foreach
                {

                }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext streamingContext)
        {
            Room.UpdateNeighborNames(this);
        }
    }
}
