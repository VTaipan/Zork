﻿using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static string CurrentRoom => _rooms[_location.Row, _location.Column];

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        break;


                    case Commands.LOOK:
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;


                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }

        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static readonly List<Commands> Directions = new List<Commands>();

        private static readonly string[,] _rooms =
            {
                { "Rocky Trail", "South of House", "Canyon View"},
                { "Forest", "West of House", "Behind House"},
                { "Dense Woods", "North of House", "Clearing"}
            };

        private static (int Row, int Column) _location = (1, 1);
        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when _location.Row < _rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.EAST when _location.Column < _rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                case Commands.WEST when _location.Column > 0:
                    _location.Column--;
                    didMove = true;
                    break;
            }
            return didMove;
        }
    }
}