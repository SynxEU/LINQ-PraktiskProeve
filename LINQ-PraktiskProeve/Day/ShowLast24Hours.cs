using System;
using System.Globalization;
using LINQ_PraktiskProeve.Models;
using Spectre.Console;

namespace LINQ_PraktiskProeve.Day;

public class ShowLast24Hours
{
    /// <summary>
    /// Denne metode viser vejrinformationer for de sidste 24 timer, herunder temperatur og vindhastighed, 
    /// baseret på de modtagne vejrdata. Hvis dataene er tilgængelige, præsenteres de i en tabel. Hvis ikke, 
    /// vises en fejlmeddelelse med dato og tid for forsøget på at hente data.
    /// </summary>
    public static void ShowLast24HoursWeather(Root weatherData)
    {
        // Tjek om nødvendige data er tilgængelige
        if (weatherData.Hourly.Temperature2M.Any()
            || weatherData.Hourly.Time.Any()
            || weatherData.Hourly.WindSpeed10M.Any())
        {
            // Hent dataene for tid, temperatur og vindhastighed
            List<string>? times = weatherData.Hourly?.Time ?? new List<string>();
            List<object>? temperatures = weatherData.Hourly?.Temperature2M ?? new List<object>();
            List<object>? windSpeeds = weatherData.Hourly?.WindSpeed10M ?? new List<object>();

            // Opret en tabel til at vise dataene
            Table table = new Table();
            table.AddColumn("Dato"); // Kolonne for dato
            table.AddColumn("Temp (°C)"); // Kolonne for temperatur
            table.AddColumn("Max Vind (m/s)"); // Kolonne for vindhastighed

            // Udfyld tabellen med data for de sidste 24 timer
            for (int i = 0; i < 25; i++)
            {
                string tid = $"{DateTime.Parse(times[i]).ToShortDateString()} {DateTime.Parse(times[i]).TimeOfDay}";
                table.AddRow(tid, temperatures[i].ToString(), windSpeeds[i].ToString());
            }

            // Ryd skærmen og vis tabellen
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