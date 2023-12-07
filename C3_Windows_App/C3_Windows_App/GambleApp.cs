using C3_Windows_App.Data;
using C3_Windows_App.Model;
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
        User currentUser;

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
                LoginUserInput(userInput);
            }
        }
        private void LoginUserInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    Login();
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
        private string ShowGambleMenu()
        {
            Console.Clear();
            Console.WriteLine("1. zie de opkomende wedstrijden");
            Console.WriteLine("2. ");

            Console.WriteLine("X. Exit");

            string userInput = Helpers.Ask("Make your choice and press <ENTER>.");
            return userInput;
        }
        private void Login()
        {
            Console.Clear();
            Console.WriteLine("uw email:");
            string emailInput = Console.ReadLine();
            Console.WriteLine("uw wachtwoord:");
            string passInput = Console.ReadLine();

            foreach(User user in WindowsAppDataContext.Users )
            {
                if (user.Email == emailInput && user.Password == passInput)
                {
                    currentUser = user;
                    Console.WriteLine($"{user.Name} u bent ingelogd");
                    
                    return;
                }
            }
            Console.WriteLine("dit account bestaat niet");
        }
    }
}
