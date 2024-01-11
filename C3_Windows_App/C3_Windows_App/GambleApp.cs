using C3_Windows_App.Data;
using C3_Windows_App.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C3_Windows_App
{
    internal class GambleApp
    {
        WindowsAppDataContext WindowsAppDataContext;
        FootballGameData FootballGameData;
        List<FootballGame> matches;
        User currentUser;
        private string State;

        public GambleApp()
        {
            WindowsAppDataContext = new WindowsAppDataContext();
            FootballGameData = new FootballGameData();
        }

        internal void run()
        {
            State = "login";
            string userInput = "";
            
            while (userInput.ToLower() != "x")
            {
                switch (State)
                {
                    case "login":
                        userInput = ShowLoginMenu();
                        LoginUserInput(userInput);
                        break;
                    case "gamble":
                        userInput = ShowGambleMenu();
                        GambleInput(userInput);
                        break;
                }

                
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
                    Register();
                    break;
                default:
                    Console.WriteLine("Incorrect choice...");
                    // Invalid input
                    break;
            }
            Helpers.Pause();
        }
        private void GambleInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    ShowAllMatches();
                    break;
                case "2":
                    CheckBalance();
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
            Console.WriteLine("2. check uw balans");

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

                    // Check if the user is an Admin
                    if (user.Rank == "Admin")
                    {
                        Console.WriteLine($"{user.Name} u bent ingelogd als Admin. Welkom!");
                    }
                    else
                    {
                        Console.WriteLine($"{user.Name} u bent ingelogd");
                    }

                    if (user.Rank == "Admin")
                    {
                        ShowAdminMenu();
                    }
                    else
                    {
                       State = "gamble";
                    }
                    return;
                }
            }
           
            Console.WriteLine("dit account bestaat niet");
        }

        private async void ShowAdminMenu()
        {
            string adminInput = "";
            while (adminInput.ToLower() != "x")
            {
                Console.Clear();
                Console.WriteLine("1. uitloggen");
                Console.WriteLine("2. Uitbetalen"); 
                Console.WriteLine("3. Laad resultaten");
                Console.WriteLine("4. Laad wedstrijden");

                Console.WriteLine("X. Exit"); // sluit de applicatie af

                adminInput = Helpers.Ask("Make your choice and press <ENTER>.");

                switch (adminInput)
                {
                    case "1":
                        State = "login";
                        Console.WriteLine("U bent uitgelogd");
                        return;
                        
                    case "2":
                       
                        break;
                    case "3":
                        
                        break;
                    case "4":
                        
                        await FetchDataFromApi();
                        break;
                    default:
                        Console.WriteLine("Incorrect choice...");
                        // Invalid input
                        break;
                }

                Helpers.Pause();
            }
        }


        private async Task FetchDataFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Replace "YOUR_API_ENDPOINT" with the actual API endpoint you want to query
                    string apiEndpoint = "https://fifa.amo.rocks/api/results.php?key=ja17";

                    HttpResponseMessage response = await client.GetAsync(apiEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Data from API:");

                        // Parse JSON string and format it
                        var formattedJson = JToken.Parse(apiData).ToString(Formatting.Indented);

                        Console.WriteLine(formattedJson);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch data from API. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data from API: {ex.Message}");
                }
            }
        }

        private void Register()
        {
            int balance = 50;

            string rank = "Gambler";

            Console.Clear();
            Console.WriteLine("Registreer een nieuw account");
            string name = Helpers.Ask("Geef je naam op");
            string email = Helpers.Ask("Geef je email op");
            string password = Helpers.Ask("Geef je wachtwoord op");
            

            User newUser = new User(name, email, password, balance, rank);
            WindowsAppDataContext.Users.Add(newUser);
            WindowsAppDataContext.SaveChanges();
            Console.WriteLine("Registration successful! Press <ENTER> to continue.");
        }
        private void ShowAllMatches()
        {
            Console.Clear();

            Console.WriteLine("================ All MATCHES ================\n");
            if (FootballGameData.GetMatchList().Count > 0)
            {
                matches = FootballGameData.GetMatchList();
                foreach(FootballGame Match in matches)
                {
                    Console.WriteLine($"{Match.Id} | {Match.Team1_Name} vs {Match.Team2_Name}");
                }
            }
            else
            {
                Console.WriteLine("there are no matches in matchList");
            }
            string Bool = Helpers.Ask("Do you want to gamble on a match? (y/n)");
            if (Bool == "y" || Bool == "Y")
            {
                int match_id = Helpers.AskForInt("Which game would you like to gamble on?");
                Gamble(match_id);
            }
            else if (Bool == "n" || Bool == "N")
            {
                return;
            }
            else
            {
                Console.WriteLine("please enter a valid value");
                return;
            }
            
        }


        private void Gamble(int match_id)
        {
            foreach(FootballGame Match in matches)
            {
                if(Match.Id == match_id)
                {
                    Console.WriteLine($"{Match.Team1_Id} | {Match.Team1_Name}");
                    Console.WriteLine($"{Match.Team2_Id} | {Match.Team2_Name}");
                    int teamId = Helpers.AskForInt($"on which team would u like to gamble to win (type it's Id)");
                    int amountSpent = Helpers.AskForInt("how much would u like to gamble on this team?");
                    string Bool = Helpers.Ask("are u sure? (y/n)");
                    if (Bool == "y" || Bool == "Y") 
                    {
                        foreach(User user in WindowsAppDataContext.Users)
                        {
                            if(user.Id == currentUser.Id)
                            {
                                user.Balance -= amountSpent;
                                
                            }
                        }
                        WindowsAppDataContext.SaveChanges();
                        Console.WriteLine("Gamble succesfully came through");
                    }
                    else if (Bool == "n" || Bool == "N")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("please enter a valid value");
                        return;
                    }
                }
            }
            
        }
        private void CheckBalance()
        {
            Console.Clear();
            Console.WriteLine($"Uw balans: {currentUser.Balance}");
        }
    }
}
