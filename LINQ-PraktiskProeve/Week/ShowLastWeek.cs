using System;
using System.Globalization;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Week;

public static class ShowLastWeek
{
    public static void ShowLastWeekWeather(Root weatherData)
    {
        var days = weatherData.Daily?.Time;
        var maxTemps = weatherData.Daily?.Temperature2mMax;
        var minTemps = weatherData.Daily?.Temperature2mMin;
        var rainSum = weatherData.Daily?.RainSum;
        var windMax = weatherData.Daily?.WindSpeed10mMax;

        if (days != null && maxTemps != null && minTemps != null && rainSum != null && windMax != null)
        {
            Console.WriteLine("Vejret den seneste uge:");
            for (int i = 0; i < 7; i++)
            {
                var date = DateTime.Parse(days[i]);
                var dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Dag: {dayOfWeek} | Dato: {DateTime.Parse(days[i]).ToShortDateString()}");
                Console.WriteLine($"Max Temp: {maxTemps[i]}°C | Min Temp: {minTemps[i]}°C");
                Console.WriteLine($"Regn: {rainSum[i]} mm | Max vind: {windMax[i]} m/s");
            }
        }
    }
}