using System;
using System.Globalization;
using System.Linq;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class Show24HoursDetail
{
    public static void ShowNext24HoursDetailed(Root weatherData)
    {
        if (weatherData.Hourly.Time.Any()
            || weatherData.Hourly.Temperature2M.Any()
            || weatherData.Hourly.WindSpeed10M.Any())
        {
            int currentHour = DateTime.Now.Hour + 14;
            List<string>? times = weatherData.Hourly.Time;
            List<object>? temperatures = weatherData.Hourly.Temperature2M;
            List<object>? windSpeeds = weatherData.Hourly.WindSpeed10M;
            
            var weatherList =
                times.Zip(temperatures, (t, temp) => new { Time = t, Temp = temp })
                    .Zip(windSpeeds, (entry, wind) => new { entry.Time, entry.Temp, Wind = wind })
                    .Skip(currentHour)
                    .Take(25)
                    .OrderByDescending(x => x.Time)
                    .ToList();

            Console.WriteLine("Time-for-time vejr de næste 24 timer (sorteret faldende):");
            foreach (var entry in weatherList)
            {
                Console.WriteLine(
                    $"Tid: {DateTime.Parse(entry.Time).ToShortDateString()} {DateTime.Parse(entry.Time).TimeOfDay} " +
                    $"| Temp: {entry.Temp}°C " +
                    $"| Vind: {entry.Wind} m/s");
            }
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