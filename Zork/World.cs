using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Zork
{
    public class World
    {
        public HashSet<Room> Rooms { get; set; }

        public List<Item> Items { get; }

        public World(List<Item> items)
        {
            Items = items;
            ItemsByName = new Dictionary<string, Item>(StringComparer.OrdinalIgnoreCase);
            foreach(Item item in Items)
            {
                ItemsByName.Add(item.Name, item);
            }
        }

        [JsonIgnore]
        public IReadOnlyDictionary<string, Room> RoomsByName => _RoomsByName;

        [JsonIgnore]
        public Dictionary<string, Item> ItemsByName { get; }

        public Player SpawnPlayer() => new Player(this, StartingLocation);

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _RoomsByName = Rooms.ToDictionary(room => room.Name, room => room);
            foreach (Room room in Rooms)
            {
                room.UpdateNeighbors(this);
                room.UpdateInventory(this);
            }
        }

        [JsonProperty]
        private string StartingLocation { get; set; }
        private Dictionary<string, Room> _RoomsByName;
    }
}