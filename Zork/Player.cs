using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork
{
    public class Player
    {
        public World World { get; }

        public List<Item> Inventory { get; }

        [JsonIgnore]
        public Room Location { get; private set; }

        //[JsonIgnore]             
        //public Room Location
        //{
        //    get => _location
        //    set => _location = value;
        //}

        [JsonIgnore]
        public string LocationName //0 references
        {
            get
            {
                return Location?.Name;
            }
            set
            {
                Location = World?.RoomsByName.GetValueOrDefault(value);
            }
        }
        public Player(World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;

            //if(World.RoomsByName.TryGetValue(startingLocation, out _location) == false) 
            //{
            //    throw new Exception($"Invalid starting location: {startingLocation}.");
            //}

            Inventory = new List<Item>();
        }

        public bool Move(Directions direction)
        {
            bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                Location = destination;
            }
            return isValidMove;
        }
    }
}