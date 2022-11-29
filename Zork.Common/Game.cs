using System;
using System.Linq;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        [JsonIgnore]
        public Player Player { get; }

        public Enemy Enemy { get; }

        [JsonIgnore]
        public IInputService Input { get; private set; }

        [JsonIgnore]
        public IOutputService Output { get; private set; }

        [JsonIgnore]
        public bool IsRunning { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IInputService input, IOutputService output)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
            Output = output ?? throw new ArgumentNullException(nameof(output));

            IsRunning = true;
            Input.InputReceived += OnInputReceived;
            Output.WriteLine("Welcome to Zork!");
            Look();
            Output.WriteLine($"\n{Player.CurrentRoom}");
        }

        public void OnInputReceived(object sender, string inputString)
        {
            char separator = ' ';
            string[] commandTokens = inputString.Split(separator);

            string verb;
            string subject = null;
            string preposition = null;
            string weapon = null;
            if (commandTokens.Length == 0)
            {
                return;
            }
            else if (commandTokens.Length == 1)
            {
                verb = commandTokens[0];
            }
            else if (commandTokens.Length == 2)
            {
                verb = commandTokens[0];
                subject = commandTokens[1];
            }
            else if (commandTokens.Length == 3)
            {
                return;
            }
            else
            {
                verb = commandTokens[0];
                subject = commandTokens[1];
                preposition = commandTokens[2];
                weapon = commandTokens[3];
            }

            Room previousRoom = Player.CurrentRoom;
            Commands command = ToCommand(verb);
            switch (command)
            {
                case Commands.Quit:
                    IsRunning = false;
                    Output.WriteLine("Thank you for playing!");
                    break;

                case Commands.Look:
                    Look();
                    break;

                case Commands.North:
                case Commands.South:
                case Commands.East:
                case Commands.West:
                    Directions direction = (Directions)command;
                    Output.WriteLine(Player.Move(direction) ? $"You moved {direction}." : "The way is shut!");
                    break;

                case Commands.Take:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Take(subject);
                    }
                    break;

                case Commands.Drop:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Drop(subject);
                    }
                    break;

                case Commands.Inventory:
                    if (Player.Inventory.Count() == 0)
                    {
                        Console.WriteLine("You are empty handed.");
                    }
                    else
                    {
                        Console.WriteLine("You are carrying:");
                        foreach (Item item in Player.Inventory)
                        {
                            Output.WriteLine(item.InventoryDescription);
                        }
                    }
                    break;

                case Commands.Moves:
                    Output.WriteLine(Player.Moves);
                    break;

                case Commands.Reward:
                    Player.Score++;
                    break;

                case Commands.Score:
                    Output.WriteLine(Player.Score);
                    break;

                case Commands.Attack:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else if (string.IsNullOrEmpty(preposition))
                    {
                        Output.WriteLine("This command requires a preposition.");
                    }
                    else if (string.IsNullOrEmpty(weapon))
                    {
                        Output.WriteLine("This command requires a weapon.");
                    }
                    else
                    {
                        Attack(subject, weapon);
                    }
                    break;

                default:
                    Output.WriteLine("Unknown command.");
                    break;
            }

            if (ReferenceEquals(previousRoom, Player.CurrentRoom) == false)
            {
                Look();
            }

            Output.WriteLine($"\n{Player.CurrentRoom}");

            if (command != Commands.Unknown && command != Commands.Reward)
            {
                Player.Moves++;
            }
        }
        
        private void Attack(string enemyName, string weaponName)
        {
            Enemy enemyToAttack = Player.CurrentRoom.Enemies.FirstOrDefault(enemy => string.Compare(enemy.Name, enemyName, ignoreCase: true) == 0);
            Item weaponUsed = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, weaponName, ignoreCase: true) == 0);
            if (enemyToAttack.EnemyHP <= 0)
            {
                Output.WriteLine("Its already dead, you monster.");
            }
            else if (enemyToAttack.EnemyHP > 0)
            {
                if (enemyToAttack == null || weaponUsed == null)
                {
                    Output.WriteLine("Invalid command.");
                }
                else if (weaponUsed.Type != "Weapon")
                {
                    Output.WriteLine("This would do nothing.");
                }
                else
                {
                    enemyToAttack.EnemyHP = enemyToAttack.EnemyHP - weaponUsed.Damage;
                    if (enemyToAttack.EnemyHP <= 0)
                    {
                        enemyToAttack.LookDescription = $"The {enemyToAttack} is dead.";
                        Output.WriteLine($"You killed the {enemyToAttack} with the {weaponName}.");
                        enemyToAttack.EnemyState = "Dead";
                    }
                    else
                    {
                        Output.WriteLine($"You hit the {enemyName}! \nYou have done {weaponUsed.Damage} damage to the {enemyName}!");
                    }
                }
            }
        }

        private void Look()
        {
            Output.WriteLine(Player.CurrentRoom.Description);
            foreach (Item item in Player.CurrentRoom.Inventory)
            {
                Output.WriteLine(item.LookDescription);
            }
            foreach (Enemy enemy in Player.CurrentRoom.Enemies)
            {
                if (enemy.EnemyState == "Alive")
                {
                    Output.WriteLine(enemy.LookDescription);
                }
                else if (enemy.EnemyState == "Dead")
                {
                    enemy.LookDescription = $"The {enemy.Name} is laying there, motionless.";
                    Output.WriteLine(enemy.LookDescription);
                }
            }
        }

        private void Take(string itemName)
        {
            Item itemToTake = Player.CurrentRoom.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToTake == null)
            {
                Output.WriteLine("You can't see any such thing.");                
            }
            else
            {
                Player.AddItemToInventory(itemToTake);
                Player.CurrentRoom.RemoveItemFromInventory(itemToTake);
                Console.WriteLine("Taken.");
            }
        }

        private void Drop(string itemName)
        {
            Item itemToDrop = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToDrop == null)
            {
                Console.WriteLine("You can't see any such thing.");                
            }
            else
            {
                Player.CurrentRoom.AddItemToInventory(itemToDrop);
                Player.RemoveItemFromInventory(itemToDrop);
                Console.WriteLine("Dropped.");
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
}