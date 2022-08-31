using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("> ");
                string inputString = Console.ReadLine();
                Commands command = ToCommand(inputString.Trim().ToUpper());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;
                        

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                        outputString = "You moved NORTH";
                        break;

                    case Commands.SOUTH:
                        outputString = "You moved SOUTH";
                        break;

                    case Commands.EAST:
                        outputString = "You moved EAST";
                        break;

                    case Commands.WEST:
                        outputString = "You moved WEST";
                        break;
          
                    default:
                        outputString = "Unknown command.";
                        break;
                }
                Console.WriteLine(outputString);
            }
            
        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }
    }
}
