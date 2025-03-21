using System;
using System.Globalization;
using LINQ_PraktiskProeve.Models;
using Spectre.Console;

namespace LINQ_PraktiskProeve.Week;

public static class ShowLastWeek
{
    public static void ShowLastWeekWeather(Root weatherData)
    {
        if (weatherData.Daily.Time.Any() 
            || weatherData.Daily.Temperature2MMax.Any() 
            || weatherData.Daily.Temperature2MMin.Any() 
            || weatherData.Daily.RainSum.Any() 
            || weatherData.Daily.WindSpeed10MMax.Any())
        {
            List<string> days = weatherData.Daily.Time;
            List<double> maxTemps = weatherData.Daily.Temperature2MMax;
            List<double> minTemps = weatherData.Daily.Temperature2MMin;
            List<double> rainSum = weatherData.Daily.RainSum;
            List<double> windMax = weatherData.Daily.WindSpeed10MMax;
            Table table = new Table();
    
            table.AddColumn("Dag");
            table.AddColumn("Max Temp (°C)");
            table.AddColumn("Min Temp (°C)");
            table.AddColumn("Regn (mm)");
            table.AddColumn("Max Vind (m/s)");
            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Parse(days[i]);
                string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
                
                string dato = $"{dayOfWeek} {date.ToShortDateString()}";
                
                table.AddRow(dato, maxTemps[i] + "°C", minTemps[i] + "°C", rainSum[i] + " mm", windMax[i] + " m/s");
            }
            AnsiConsole.Write(table);
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