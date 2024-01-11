using C3_Windows_App.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;


namespace C3_Windows_App.Data
{
    internal class FootballGameData
    {
        private string apiUrl;

        private Dictionary<int, int> matchIndexToIdMapping;
        private JsonSerializer serializer;
        private JsonReader jsonReader;
        private HttpResponseMessage response;
        private List<FootballGame> matches;

        public FootballGameData()
        {
            apiUrl = "https://fifa.amo.rocks/api/matches.php?key={D295237}";
            matchIndexToIdMapping = new Dictionary<int, int>();
            serializer = new JsonSerializer();
            ConsumeApiAsync();
        }

        private void ConsumeApiAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a stream
                    using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                    using (StreamReader reader = new StreamReader(stream))
                    using (JsonReader jsonReader = new JsonTextReader(reader))
                    {
                        // Use JSON.NET to deserialize the stream
                        var serializer = new JsonSerializer();
                        FootballGame[] between = serializer.Deserialize<FootballGame[]>(jsonReader);
                        matches = new List<FootballGame>();
                        foreach(FootballGame match in between)
                        {
                            matches.Add(new FootballGame(match.Id, match.Team1_Id, match.Team1_Name, match.Team2_Id, match.Team2_Name));
                        }
                        
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

                
            }
        }
        public List<FootballGame> GetMatchList()
        {
            return matches;
        }
    }
}