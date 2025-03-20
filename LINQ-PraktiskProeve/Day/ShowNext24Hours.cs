using System;
using System.Collections.Generic;
using System.Linq;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Day;

public class ShowNext24Hours
{
    public static void ShowNext24HoursSummary(Root weatherData)
    {
        if (weatherData.Hourly != null)
        {
            int currentHour = DateTime.Now.Hour + 14;
            var temperatures = weatherData.Hourly.Temperature2m;
            var windSpeeds = weatherData.Hourly.WindSpeed10m;

            if (temperatures != null && windSpeeds != null)
            {
                List<double> next24Temps = new List<double>();
                List<double> next24Winds = new List<double>();

                foreach (var temperature in temperatures.Skip(currentHour).Take(25).ToList())
                    next24Temps.Add(double.Parse(temperature.ToString()));

                foreach (var wind in windSpeeds.Skip(currentHour).Take(25).ToList())
                    next24Winds.Add(double.Parse(wind.ToString()));

                double maxTemp = next24Temps.Max();
                double minTemp = next24Temps.Min();
                double avgWindSpeed = next24Winds.Average();

                Console.WriteLine("Vejr for de næste 24 timer:");
                Console.WriteLine($"Højeste Temp: {maxTemp}°C");
                Console.WriteLine($"Laveste Temp: {minTemp}°C");
                Console.WriteLine($"Gennemsnitlig Vindhastighed: {avgWindSpeed:F2} m/s");
            }
        }
    }
}