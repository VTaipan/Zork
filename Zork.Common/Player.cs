using System;
using System.Collections.Generic;

namespace Zork.Common
{
    public class Player
    {
        public event EventHandler<int> MovesChanged;
        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
        }

        public int Moves
        {
            get
            {
                return _moves;
            }

            set
            {
                if (_moves != value)
                {
                    _moves = value;
                    MovesChanged?.Invoke(this, _moves);
                }
            }
        }

        public List<Item> Inventory { get; }

        void AddToInventory(Item itemToAdd)
        {

        }

        void RemoveFromInventory(Item itemToAdd)
        {

        }

        public Player(World world, string startingLocation)
        {
            _world = world;

            if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
            {
                throw new Exception($"Invalid starting location: {startingLocation}");
            }

            Inventory = new List<Item>();
        }

        public bool Move(Directions direction)
        {
            bool didMove = _currentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
            if (didMove)
            {
                CurrentRoom = neighbor;
            }

            return didMove;
        }

        private World _world;
        private Room _currentRoom;
        private int _moves;
    }
}
