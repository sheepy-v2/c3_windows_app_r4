using C3_Windows_App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App.Data
{
    internal class ResultsData
    {
       
            private string apiUrl;

            private Dictionary<int, int> resultIndexToIdMapping;
            private JsonSerializer serializer;
            private JsonReader jsonReader;
            private HttpResponseMessage response;
            private List<Result> results;

            public ResultsData()
            {
                apiUrl = "https://fifa.amo.rocks/api/results.php?key={D295237}";
                resultIndexToIdMapping = new Dictionary<int, int>();
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
                            Result[] between = serializer.Deserialize<Result[]>(jsonReader);
                            results = new List<Result>();
                            foreach (Result result in between)
                            {
                            int? winnerId = result.Winner_Id ?? null;
                            results.Add(new Result(
                                    result.Id,
                                    result.Team1_Id,
                                    result.Team1_Name,
                                    result.Team1_Score,
                                    result.Team2_Id,
                                    result.Team2_Name,
                                    result.Team2_Score,
                                    winnerId
                                )) ;
                            }

                    }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }


                }
            }
            public List<Result> GetResultsList()
            {
                return results;
            }
        }
}
