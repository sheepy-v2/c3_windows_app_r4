using C3_Windows_App.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C3_Windows_App.Model.screens
{
    internal class Admin_Screen
    {
        GambleApp gambleApp;
        List<User> users;
        List<Bet> bets;
        private string apiType = "";
        public Admin_Screen(GambleApp gambleapp)
        {
            gambleApp = gambleapp;
            bets = gambleApp.GetDataContext().Bets.ToList();
            users = gambleApp.GetDataContext().Users.ToList();
            ShowAdminMenu();
            AdminInput();
        }
        private void ShowAdminMenu()
        {
            
            
                Console.Clear();
                Console.WriteLine("1. uitloggen");
                Console.WriteLine("2. Uitbetalen");
                Console.WriteLine("3. Laad resultaten");
                Console.WriteLine("4. Laad wedstrijden");

                Console.WriteLine("X. Exit"); // sluit de applicatie af

                
                gambleApp.SetInput(Helpers.Ask("Make your choice and press <ENTER>."));
                

                
            
        }
        private void AdminInput()
        {
            switch (gambleApp.GetInput())
            {
                case "1":
                    Logout();
                    break;

                case "2":
                    CheckBets();
                    break;
                case "3":
                    apiType = "results";
                    FetchDataFromApi();
                    break;
                case "4":
                    apiType = "matches";
                    FetchDataFromApi();
                    break;
                default:
                    Console.WriteLine("Incorrect choice...");
                    // Invalid input
                    break;
            }
            Helpers.Pause();
        }
        private void CheckBets()
        {
            foreach(Bet bet in bets)
            {
                if (bet.Payed == false)
                {
                    foreach (FootballGame match in gambleApp.GetMatchData().GetMatchList())
                    {
                        if (match.Id == bet.MatchId)
                        {
                            foreach (Result result in gambleApp.GetResultsData().GetResultsList())
                            {
                                if (match.Team1_Id == result.Team1_Id && match.Team2_Id == result.Team2_Id)
                                {
                                    if (bet.TeamId == result.Winner_Id || result.Winner_Id == null)
                                    {

                                        PayOut(bet, result);
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
            
        }
        private void PayOut(Bet bet, Result result)
        {
            
            Debug.WriteLine("got to payout");
            if(result.Winner_Id == bet.TeamId)
            {
                foreach (User user in users)
                {
                    if(user.Id == bet.UserId)
                    {
                        user.Balance += bet.Amount * 2;
                        bet.Payed = true;
                    }
                }
                
            }
            else
            {
                foreach (User user in gambleApp.GetDataContext().Users)
                {
                    if (user.Id == bet.UserId)
                    {
                        user.Balance += bet.Amount;
                    }
                }
            }
            gambleApp.GetDataContext().SaveChanges();
            
        }
        private async Task FetchDataFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Replace "YOUR_API_ENDPOINT" with the actual API endpoint you want to query
                    string apiEndpoint = $"https://fifa.amo.rocks/api/{apiType}.php?key=D295237";

                    HttpResponseMessage response = await client.GetAsync(apiEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Data from API:");

                        // Parse JSON string and format it
                        var formattedJson = JToken.Parse(apiData).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);

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
        private void Logout()
        {
            gambleApp.SetState("login");
        }
    }
}
