using System;
using System.Globalization;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Week;

public static class ShowLastWeek
{
    public static void ShowLastWeekWeather(Root weatherData)
    {
        List<string>? days = weatherData.Daily?.Time;
        List<double>? maxTemps = weatherData.Daily?.Temperature2mMax;
        List<double>? minTemps = weatherData.Daily?.Temperature2mMin;
        List<double>? rainSum = weatherData.Daily?.RainSum;
        List<double> windMax = weatherData.Daily?.WindSpeed10mMax;

        if (days != null && maxTemps != null && minTemps != null && rainSum != null && windMax != null)
        {
            Console.WriteLine("Vejret den seneste uge:");
            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Parse(days[i]);
                string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Dag: {dayOfWeek} | Dato: {DateTime.Parse(days[i]).ToShortDateString()}");
                Console.WriteLine($"Max Temp: {maxTemps[i]}°C | Min Temp: {minTemps[i]}°C");
                Console.WriteLine($"Regn: {rainSum[i]} mm | Max vind: {windMax[i]} m/s");
            }
        }
    }
}