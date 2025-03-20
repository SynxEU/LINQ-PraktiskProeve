using System;
using System.Linq;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class Show24HoursDetail
{
    public static void ShowNext24HoursDetailed(Root weatherData)
    {
        if (weatherData.Hourly != null)
        {
            int currentHour = DateTime.Now.Hour + 14;
            List<string>? times = weatherData.Hourly.Time;
            List<object>? temperatures = weatherData.Hourly.Temperature2m;
            List<object>? windSpeeds = weatherData.Hourly.WindSpeed10m;

            if (times != null && temperatures != null && windSpeeds != null)
            {
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
        }
    }
}