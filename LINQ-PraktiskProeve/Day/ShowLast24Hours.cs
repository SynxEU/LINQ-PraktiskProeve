using System;
using System.Globalization;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class ShowLast24Hours
{
    public static void ShowLast24HoursWeather(Root weatherData)
    {
        if (weatherData.Hourly.Temperature2M.Any() 
            || weatherData.Hourly.Time.Any() 
            || weatherData.Hourly.WindSpeed10M.Any())
        {
            List<string>? times = weatherData.Hourly?.Time ?? new List<string>();
            List<object>? temperatures = weatherData.Hourly?.Temperature2M ?? new List<object>();
            List<object>? windSpeeds = weatherData.Hourly?.WindSpeed10M ?? new List<object>();
            Console.WriteLine("Vejret de seneste 24 timer:");
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(
                    $"Tid: {DateTime.Parse(times[i]).ToShortDateString()} {DateTime.Parse(times[i]).TimeOfDay} " +
                    $"| Temp: {temperatures[i]}°C " +
                    $"| Vind: {windSpeeds[i]} m/s");
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