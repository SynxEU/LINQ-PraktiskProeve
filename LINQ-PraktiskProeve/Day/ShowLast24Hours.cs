using System;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class ShowLast24Hours
{
    public static void ShowLast24HoursWeather(Root weatherData)
    {
        List<string>? times = weatherData.Hourly?.Time;
        List<object>? temperatures = weatherData.Hourly?.Temperature2m;
        List<object> windSpeeds = weatherData.Hourly?.WindSpeed10m;

        if (times != null && temperatures != null && windSpeeds != null)
        {
            Console.WriteLine("Vejret de seneste 24 timer:");
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(
                    $"Tid: {DateTime.Parse(times[i]).ToShortDateString()} {DateTime.Parse(times[i]).TimeOfDay} " +
                    $"| Temp: {temperatures[i]}°C " +
                    $"| Vind: {windSpeeds[i]} m/s");
            }
        }
    }
}