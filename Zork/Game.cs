using System;

namespace Zork
{
    public class Game
    {
        public World World { get; }
    }

    public Player Player { get; }
    public Game (World world, string startingLocation)
    {
        World = world;
        Player = new Player(World, startingLocation);
    }

    private Commands ToCommand(string commandString)
    {
        return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }

    private bool Move(Commands command)
    {
        bool didMove = false;

        switch (command)
        {
            case Commands.NORTH when _location.Row < _world.Rooms.GetLength(0) - 1:
                _location.Row++;
                didMove = true;
                break;

            case Commands.SOUTH when _location.Row > 0:
                _location.Row--;
                didMove = true;
                break;

            case Commands.EAST when _location.Column < _world.Rooms.GetLength(1) - 1:
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

    public Run()
    {
        Room previousRoom = CurrentRoom;
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine(CurrentRoom);
            if (previousRoom != CurrentRoom && CurrentRoom.HasBeenVisited == false)
            {
                Console.WriteLine(CurrentRoom.Description);
                previousRoom = CurrentRoom;
                CurrentRoom.HasBeenVisited = true;
            }
            Console.Write("> ");
        }
    }


        Commands command = Commands.UNKNOWN;
        while (command != Commands.QUIT)
        {
            command = ToCommand(Console.ReadLine().Trim());

            switch (command)
            {
                case Commands.QUIT:
                    isRunning = false;
                    Console.WriteLine("Thank you for playing!");
                    break;

                case Commands.LOOK:
                    Console.WriteLine(CurrentRoom.Description);
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

            Console.WriteLine(CurrentRoom);
        }
    }
}
