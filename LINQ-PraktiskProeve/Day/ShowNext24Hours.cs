using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class ShowNext24Hours
{
    public static void ShowNext24HoursSummary(Root weatherData)
    {
        if (weatherData.Hourly.Temperature2M.Any()
            || weatherData.Hourly.WindSpeed10M.Any()
            || weatherData.Hourly.Time.Any())
        {
            int currentHour = DateTime.Now.Hour + 14;
            List<object>? temperatures = weatherData.Hourly.Temperature2M;
            List<object>? windSpeeds = weatherData.Hourly.WindSpeed10M;

            List<double> next24Temps = new List<double>();
            List<double> next24Winds = new List<double>();

            if (temperatures.Any())
                foreach (object temperature in 
                         temperatures
                             .Skip(currentHour)
                             .Take(25)
                             .ToList())
                    if (double.TryParse(
                            temperature?.ToString(), 
                            out double tempValue))
                        next24Temps.Add(tempValue);
            
            if (windSpeeds.Any())
                foreach (object wind in 
                         windSpeeds
                             .Skip(currentHour)
                             .Take(25)
                             .ToList())
                    if (double.TryParse(
                            wind?.ToString(),
                            out double windValue))
                        next24Winds.Add(windValue);;

            double maxTemp = next24Temps.Max();
            double minTemp = next24Temps.Min();
            double avgWindSpeed = next24Winds.Average();

            Console.WriteLine("Vejr for de næste 24 timer:");
            Console.WriteLine($"Højeste Temp: {maxTemp}°C");
            Console.WriteLine($"Laveste Temp: {minTemp}°C");
            Console.WriteLine($"Gennemsnitlig Vindhastighed: {avgWindSpeed:F2} m/s");
        }
        else
        {
            DateTime date = DateTime.Now;
            string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
            Console.WriteLine($"{dayOfWeek} den {DateTime.Now.ToShortDateString()} klokken {DateTime.Now.TimeOfDay}:" +
                              $"\nKunne ikke hente dataen." +
                              $"\nKlik på en tast, for at komme tilbage");
            Console.ReadKey();
        }
    }
}