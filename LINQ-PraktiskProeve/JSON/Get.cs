using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using LINQ_PraktiskProeve.Models;
using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.JSON;

public class Get
{
    public static List<Root> GetWeatherDataAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string content = response.Content.ReadAsStringAsync().Result;

            List<Root> weatherData = JsonConvert.DeserializeObject<List<Root>>(content);
            Save.SaveJsonToFile(content);

            return weatherData;
        }
    }
}