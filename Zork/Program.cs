using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = Console.ReadLine();
            Commands command = ToCommand(inputString.Trim().ToUpper());
            Console.WriteLine(command);
        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }
    }

}
//Commands command;
//
//if (commandString == "QUIT")
//{
//    Console.WriteLine("Thank you for playing.");
//    command = Commands.QUIT;
//}
//
//else if (commandString == "LOOK")
//{
//    Console.WriteLine("This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.");
//    command = Commands.LOOK;
//}
//
//else if (commandString == "NORTH")
//{
//    command = Commands.NORTH;
//}
//
//else if (commandString == "SOUTH")
//{
//    command = Commands.SOUTH;
//}
//
//else if (commandString == "EAST")
//{
//    command = Commands.EAST;
//}
//
//else if (commandString == "WEST")
//{
//    command = Commands.WEST;
//}
//
//else
//{
//    Console.WriteLine($"Unknown command: {inputString}");
//    command = Commands.UNKNOWN;
//}
