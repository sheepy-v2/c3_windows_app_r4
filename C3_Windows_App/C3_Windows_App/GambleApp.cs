using C3_Windows_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App
{
    internal class GambleApp
    {
        WindowsAppDataContext WindowsAppDataContext;

        public GambleApp()
        {
            WindowsAppDataContext = new WindowsAppDataContext();
        }

        internal void run()
        {
            string userInput = "";

            while (userInput.ToLower() != "x")
            {
                userInput = ShowLoginMenu();

            }
        }
        private void LoginUserInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    break;
                case "2":
                    break;
                default:
                    Console.WriteLine("Incorrect choice...");
                    // Invalid input
                    break;
            }
            Helpers.Pause();
        }
        private string ShowLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("1. login");
            Console.WriteLine("2. registreer een nieuw account");

            Console.WriteLine("X. Exit");

            string userInput = Helpers.Ask("Make your choice and press <ENTER>.");
            return userInput;
        }
    }
}
