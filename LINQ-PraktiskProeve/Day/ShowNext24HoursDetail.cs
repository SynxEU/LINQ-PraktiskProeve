using System;
using System.Globalization;
using System.Linq;
using LINQ_PraktiskProeve.Models;
using Spectre.Console;

namespace LINQ_PraktiskProeve.Day;

public class ShowNext24HoursDetail
{
    /// <summary>
    /// Denne metode viser vejret for de næste 24 timer i detaljer, herunder tidspunkt, temperatur og vindhastighed 
    /// for hver time. Den sorterer og viser disse data i en tabelformat. Hvis dataene ikke er tilgængelige, vises en fejlmeddelelse.
    /// </summary>
    public static void ShowNext24HoursDetailed(Root weatherData)
    {
        // Tjek om nødvendige data er tilgængelige
        if (weatherData.Hourly.Time.Any()
            || weatherData.Hourly.Temperature2M.Any()
            || weatherData.Hourly.WindSpeed10M.Any())
        {
            // Hent den aktuelle time og dataene for tid, temperatur og vindhastighed
            int currentHour = DateTime.Now.Hour + 15; // Juster med tidszoneforskel
            List<string>? times = weatherData.Hourly.Time;
            List<object>? temperatures = weatherData.Hourly.Temperature2M;
            List<object>? windSpeeds = weatherData.Hourly.WindSpeed10M;

            // Opret tabel til at vise data
            Table table = new Table();
            table.AddColumn("Dato");
            table.AddColumn("Temp (°C)");
            table.AddColumn("Max Vind (m/s)");

            // Kombiner dataene for tid, temperatur og vindhastighed
            var weatherList =
                times.Zip(temperatures, (t, temp) => new { Time = t, Temp = temp })
                    .Zip(windSpeeds, (entry, wind) => new { entry.Time, entry.Temp, Wind = wind })
                    .Skip(currentHour)
                    .Take(25)
                    .OrderByDescending(x => x.Time)
                    .ToList();

            // Tilføj dataene til tabellen
            foreach (var entry in weatherList)
            {
                string tid = $"{DateTime.Parse(entry.Time).ToShortDateString()} {DateTime.Parse(entry.Time).TimeOfDay}";
                table.AddRow(tid, entry.Temp.ToString(), entry.Wind.ToString());
            }

            // Opdatér skærmen og vis tabellen
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
            AnsiConsole.Write(table);
        }
        else
        {
            // Hvis data ikke er tilgængelige, vis en fejlmeddelelse
            DateTime date = DateTime.Now;
            string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
            Console.WriteLine($"{dayOfWeek} den {DateTime.Now.ToShortDateString()} klokken {DateTime.Now.TimeOfDay}:" +
                              $"\nKunne ikke hente dataen." +
                              $"\nKlik på en tast, for at komme tilbage");
            Console.ReadKey();
        }
    }
}