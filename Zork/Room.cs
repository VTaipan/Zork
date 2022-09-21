using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public class Room : object
    {
        public string Name { get; }
        public string Description { get; set; }

        public bool HasBeenVisited;

        public Room(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
