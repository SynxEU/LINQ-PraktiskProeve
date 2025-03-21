using System.Globalization;
using LINQ_PraktiskProeve.Models;

namespace LINQ_PraktiskProeve.Week;

public class ShowNextWeek
{
    public static void ShowNextWeekWeather(Root weatherData)
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
            Console.WriteLine("Vejret næste uge:");
            for (int i = 7; i < 14; i++)
            {
                DateTime date = DateTime.Parse(days[i]);
                string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Dag: {dayOfWeek} | Dato: {date.ToShortDateString()}");
                Console.WriteLine($"Max Temp: {maxTemps[i]}°C | Min Temp: {minTemps[i]}°C");
                Console.WriteLine($"Regn: {rainSum[i]} mm | Max vind: {windMax[i]} m/s");
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