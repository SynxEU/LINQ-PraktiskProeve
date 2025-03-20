using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using LINQ_PraktiskProeve.Models;
using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.JSON;

public class Get
{
    public static async Task<List<Root>> GetWeatherDataAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            List<Root> weatherData = JsonConvert.DeserializeObject<List<Root>>(content);
            Save.SaveJsonToFile(content);

            return weatherData;
        }
    }
}