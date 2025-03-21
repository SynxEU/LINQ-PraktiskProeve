using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class ShowNext24Hours
{
    /// <summary>
    /// Denne metode viser en opsummering af vejret for de næste 24 timer, herunder højeste og laveste temperatur 
    /// samt den gennemsnitlige vindhastighed. Den beregner disse værdier baseret på modtagne vejrdata og 
    /// præsenterer dem for brugeren. Hvis dataene ikke er tilgængelige, vises en fejlmeddelelse.
    /// </summary>
    public static void ShowNext24HoursSummary(Root weatherData)
    {
        // Tjek om nødvendige data er tilgængelige
        if (weatherData.Hourly.Temperature2M.Any()
            || weatherData.Hourly.WindSpeed10M.Any()
            || weatherData.Hourly.Time.Any())
        {
            // Hent den aktuelle time og dataene for temperatur og vindhastighed
            int currentHour = DateTime.Now.Hour + 14;
            List<object>? temperatures = weatherData.Hourly.Temperature2M;
            List<object>? windSpeeds = weatherData.Hourly.WindSpeed10M;

            List<double> next24Temps = new List<double>(); // Liste til at opbevare temperaturer
            List<double> next24Winds = new List<double>(); // Liste til at opbevare vindhastigheder

            // Fyld temperaturlisten for de næste 24 timer
            if (temperatures.Any())
                foreach (object temperature in temperatures
                             .Skip(currentHour)
                             .Take(25)
                             .ToList())
                    if (double.TryParse(temperature?.ToString(), out double tempValue))
                        next24Temps.Add(tempValue);

            // Fyld vindhastighedslisten for de næste 24 timer
            if (windSpeeds.Any())
                foreach (object wind in windSpeeds
                             .Skip(currentHour)
                             .Take(25)
                             .ToList())
                    if (double.TryParse(wind?.ToString(), out double windValue))
                        next24Winds.Add(windValue);

            // Beregn højeste og laveste temperatur samt gennemsnitlig vindhastighed
            double maxTemp = next24Temps.Max();
            double minTemp = next24Temps.Min();
            double avgWindSpeed = next24Winds.Average();

            // Vis de beregnede værdier
            Console.WriteLine("Vejr for de næste 24 timer:");
            Console.WriteLine($"Højeste Temp: {maxTemp}°C");
            Console.WriteLine($"Laveste Temp: {minTemp}°C");
            Console.WriteLine($"Gennemsnitlig Vindhastighed: {avgWindSpeed:F2} m/s");
        }
        else
        {
            // Hvis data ikke er tilgængelige, vis en fejlmeddelelse
            DateTime date = DateTime.Now;
            string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
            Console.WriteLine($"{dayOfWeek} den {DateTime.Now.ToShortDateString()} klokken {DateTime.Now.TimeOfDay}:" +
                              $"\nKunne ikke hente dataen." +
                              $"\nKlik på en tast, for at komme tilbage");
            Console.ReadKey();
        }
    }
}