using C3_Windows_App.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C3_Windows_App.Model.screens
{
    internal class Admin_Screen
    {
        GambleApp gambleApp;
        public Admin_Screen(GambleApp gambleapp)
        {
            gambleApp = gambleapp;
            ShowAdminMenu();

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
                        gambleApp.SetState("login");
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
    }
}
