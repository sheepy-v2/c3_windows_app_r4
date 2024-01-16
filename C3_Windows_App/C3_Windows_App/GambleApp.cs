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
using System.Globalization;
using C3_Windows_App.Migrations;
using C3_Windows_App.Model.Gamble_seperate;
using C3_Windows_App.Model.screens;

namespace C3_Windows_App
{
    internal class GambleApp
    {
        GambleApp gambleApp;
        WindowsAppDataContext WindowsAppDataContext;
        FootballGameData FootballGameData;
        ResultsData ResultsData;
        

        Login_Screen loginScreen;
        Admin_Screen adminScreen;
        Gamble_Screen gambleScreen;
        private string State;
        private User currentUser;

        private string userInput;
        public GambleApp()
        {
            gambleApp = this;
            WindowsAppDataContext = new WindowsAppDataContext();
            FootballGameData = new FootballGameData();
            ResultsData = new ResultsData();
        }

        private bool istrue;
        private string error;
        internal void run()
        {
            State = "login";
            userInput = "";
            
            while (userInput.ToLower() != "x")
            { 
                switch (State)
                {
                    case "login":
                        loginScreen = new Login_Screen(gambleApp);
                        break;
                    case "gamble":
                        gambleScreen = new Gamble_Screen(gambleApp);
                        break;
                    case "admin":
                        adminScreen = new Admin_Screen(gambleApp);
                        break;
                }
                currentUser = loginScreen.GetCurrentUser();
                
            }
            Console.WriteLine("...Program Ended...");
        }
        
        

        public WindowsAppDataContext GetDataContext()
        {
            return WindowsAppDataContext;
        }
        public FootballGameData GetMatchData()
        {
            return FootballGameData;
        }
        public ResultsData GetResultsData()
        {
            return ResultsData;
        }
        public void SetState(string newstate)
        {
            State = newstate;
        }
        public User GetUser()
        {
            return currentUser;
        }
        public string GetInput()
        {
            return userInput;
        }
        public void SetInput(string input)
        {
            userInput = input;
        }
    }
}
