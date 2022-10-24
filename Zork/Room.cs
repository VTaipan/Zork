using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Zork
{
    public class Room : IEquatable<Room>
    {
        [JsonProperty(Order = 1)]
        public string Name { get; private set; }

        [JsonProperty(Order = 2)]
        public string Description { get; private set; }

        [JsonProperty(PropertyName = "Neighbors", Order = 3)]
        private Dictionary<Directions, string> NeighborNames { get; set; }

        [JsonIgnore]
        public IReadOnlyDictionary<Directions, Room> Neighbors { get; private set; }

        public List<Item> Inventory { get; }

        private string[] InventoryNames { get; set; }

        public Room(List<Item> inventory, string name, string description, Dictionary<Directions, string> neighbors, string[] inventoryNames)
        {
            Name = name;
            Description = description;
            NeighborNames = neighbors ?? new Dictionary<Directions, string>();
            Inventory = inventory ?? new List<Item>();
            InventoryNames = inventoryNames ?? new string[0];
        }

        public static bool operator ==(Room lhs, Room rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            if (lhs is null || rhs is null)
            {
                return false;
            }
            return lhs.Name == rhs.Name;
        }

        public static bool operator !=(Room lhs, Room rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => obj is Room room ? this == room : false;

        public bool Equals(Room other) => this == other;

        public override string ToString() => Name;


        public void UpdateNeighbors(World world)
        {
            Neighbors = new Dictionary<Directions, Room>();
            foreach (var neighborName in NeighborNames)
            {
                Neighbors.Add(neighborName.Key, world.RoomsByName[neighborName.Value]);
            }

            NeigborNames = null;
        }

        public void UpdateInventory(World world)
        {
            Inventory = new List<Item>();
            foreach (var inventoryName in InventoryNames)
            {
                Inventory.Add(world.ItemsByName[inventoryName]);
            }
        }

        public override int GetHashCode() => Name.GetHashCode();

        public void UpdateNeighbors(World world) => Neighbors =
            (from entry in NeighborNames
             let room = world.RoomsByName.GetValueOrDefault(entry.Value)
             where room != null
             select (Direction: entry.Key, Room: room)).ToDictionary(pair => pair.Direction, pair => pair.Room);
    }
}