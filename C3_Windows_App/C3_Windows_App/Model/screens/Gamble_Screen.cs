using C3_Windows_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C3_Windows_App.Model.Gamble_seperate
{
    internal class Gamble_Screen
    {
        GambleApp gambleApp;
        List<FootballGame> matches;
        List<Result> results;

        public Gamble_Screen(GambleApp gambleapp)
        {
            gambleApp = gambleapp;
            ShowGambleMenu();
            GambleInput();
        }

        private void GambleInput()
        {
            switch (gambleApp.GetInput())
            {
                case "1":
                    ShowAllMatches();
                    break;
                case "2":
                    CheckBalance();
                    break;
                case "3":
                    checkMatchresults();
                    break;
                default:
                    Console.WriteLine("Incorrect choice...");
                    // Invalid input
                    break;
            }
            Helpers.Pause();
        }

        private void ShowGambleMenu()
        {
            Console.Clear();
            Console.WriteLine("1. zie de opkomende wedstrijden");
            Console.WriteLine("2. check uw balans");
            Console.WriteLine("3. zie resultaten van de wedstrijden");

            Console.WriteLine("X. Exit");

            gambleApp.SetState(Helpers.Ask("Make your choice and press <ENTER>."));
        }





        private void ShowAllMatches()
        {
            Console.Clear();

            Console.WriteLine("================ All MATCHES ================\n");
            if (gambleApp.GetMatchData().GetMatchList().Count > 0)
            {
                matches = gambleApp.GetMatchData().GetMatchList();
                foreach (FootballGame Match in matches)
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
            foreach (FootballGame Match in matches)
            {
                if (Match.Id == match_id)
                {
                    Console.WriteLine($"{Match.Team1_Id} | {Match.Team1_Name}");
                    Console.WriteLine($"{Match.Team2_Id} | {Match.Team2_Name}");
                    int teamId = Helpers.AskForInt($"on which team would u like to gamble to win (type it's Id)");
                    int amountSpent = Helpers.AskForInt("how much would u like to gamble on this team?");
                    string Bool = Helpers.Ask("are u sure? (y/n)");
                    if (Bool == "y" || Bool == "Y")
                    {
                        foreach (User user in gambleApp.GetDataContext().Users)
                        {
                            if (user.Id == gambleApp.GetUser().Id)
                            {
                                user.Balance -= amountSpent;

                            }
                        }
                        gambleApp.GetDataContext().SaveChanges();
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
            Console.WriteLine($"Uw balans: {gambleApp.GetUser().Balance}");
        }

        private void checkMatchresults()
        {

            results = gambleApp.GetResultsData().GetResultsList();
            foreach (Result result in results)
            {
                Console.WriteLine($"{result.Team1_Name} | {result.Team1_Score} : {result.Team2_Score} | {result.Team2_Name}");
            }

        }
    }
}
