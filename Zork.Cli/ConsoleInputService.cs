﻿using System;
using System.Collections.Generic;
using System.Text;
using Zork.Common;

namespace Zork.Cli
{
    internal interface ConsoleInputService : IInputService
    {
        public event EventHandler<string> InputReceived;

        public void ProcessInput()
        {
            string inputString = Console.ReadLine()
        }
    }
}
