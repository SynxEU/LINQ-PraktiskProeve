using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using LINQ_PraktiskProeve.Models;
using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.JSON;

public class Get
{
    /// <summary>
    /// Henter vejrudata fra en given URL og deserialiserer det til en liste af 'Root' objekter.
    /// Dataene gemmes også som en JSON-fil.
    /// </summary>
    /// <param name="url">URL’en, hvor vejrudata kan hentes fra.</param>
    /// <returns>En liste af 'Root' objekter, som indeholder vejrudata.</returns>
    public static List<Root> GetWeatherDataAsync(string url)
    {
        // Opretter en HttpClient for at hente data fra den angivne URL
        using (HttpClient client = new HttpClient())
        {
            // Sender en synkron GET-forespørgsel til URL’en og venter på svaret
            HttpResponseMessage response = client.GetAsync(url).Result;
        
            // Sikrer, at svaret var en succes (statuskode 2xx)
            response.EnsureSuccessStatusCode();
        
            // Læser indholdet af svaret som en streng
            string content = response.Content.ReadAsStringAsync().Result;

            // Deserialiserer JSON-indholdet til en liste af 'Root' objekter
            List<Root> weatherData = JsonConvert.DeserializeObject<List<Root>>(content) ?? new List<Root>();
        
            // Gemmer den hentede JSON-streng til en fil (bruges til senere brug eller debugging)
            Save.SaveJsonToFile(content);

            // Returnerer listen af vejrdata
            return weatherData;
        }
    }

}