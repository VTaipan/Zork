using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Game
    {
        public World World { get; set; }

        public Player Player { get; set; }

        public void Run()
        {
            InitializeRoomDescriptions();
            Room previousRoom = Player.CurrentRoom;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(Player.CurrentRoom);
                if (previousRoom != Player.CurrentRoom && Player.CurrentRoom.HasBeenVisited == false)
                {
                    Console.WriteLine(Player.CurrentRoom.Description);
                    previousRoom = Player.CurrentRoom;
                    Player.CurrentRoom.HasBeenVisited = true;
                }
                Console.Write("> ");


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
                            Console.WriteLine(Player.CurrentRoom.Description);
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

                    Console.WriteLine(Player.CurrentRoom);
                }
            }
        }
        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }
    }
}
//    private static bool Move(Commands command)
//    {
//        bool didMove = false;

//        switch (command)
//        {
//            case Commands.NORTH when _location.Row < _rooms.GetLength(0) - 1:
//                _location.Row++;
//                didMove = true;
//                break;

//            case Commands.SOUTH when _location.Row > 0:
//                _location.Row--;
//                didMove = true;
//                break;

//            case Commands.EAST when _location.Column < _rooms.GetLength(1) - 1:
//                _location.Column++;
//                didMove = true;
//                break;

//            case Commands.WEST when _location.Column > 0:
//                _location.Column--;
//                didMove = true;
//                break;
//        }
//        return didMove;