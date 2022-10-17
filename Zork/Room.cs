using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zork
{
    public class Room : object
    {
        public string Name { get; }
        public string Description { get; set; }

        public bool HasBeenVisited;

        [JsonIgnore]
        public Dictionary<Directions, Room> NeighborNames { get; set; }

        [JsonProperty]
        private Dictionary<Directions, string> NeighborNames { get; set; }
        public Room(string name, string description, Dictionary<Directions, string> neigborNames)
        {
            Name = name;
            Description = description;
            NeighborNames = neigborNames ?? new Dictionary<Directions, string>();
        }

        public override string ToString()
        {
            return Name;
        }

        public void UpdateNeighborNames(World world)
        {
             //TO DO
        }
    }
}
s