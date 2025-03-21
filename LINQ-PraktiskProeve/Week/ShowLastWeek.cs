using System;
using System.Globalization;
using LINQ_PraktiskProeve.Models;
using Spectre.Console;

namespace LINQ_PraktiskProeve.Week;

public static class ShowLastWeek
{
    /// <summary>
    /// Vist data for vejret de sidste 7 dage, inklusiv temperaturer, regn og vind.
    /// </summary>
    public static void ShowLastWeekWeather(Root weatherData)
    {
        // Kontrollerer om der er tilgængelige data for de daglige vejrmålinger
        if (weatherData.Daily.Time.Any()
            || weatherData.Daily.Temperature2MMax.Any()
            || weatherData.Daily.Temperature2MMin.Any()
            || weatherData.Daily.RainSum.Any()
            || weatherData.Daily.WindSpeed10MMax.Any())
        {
            // Henter listen over dagene og de øvrige målinger
            List<string> days = weatherData.Daily.Time;
            List<double> maxTemps = weatherData.Daily.Temperature2MMax;
            List<double> minTemps = weatherData.Daily.Temperature2MMin;
            List<double> rainSum = weatherData.Daily.RainSum;
            List<double> windMax = weatherData.Daily.WindSpeed10MMax;

            // Opretter en tabel til at vise de daglige vejrmålinger
            Table table = new Table();
            table.AddColumn("Dag");
            table.AddColumn("Max Temp (°C)");
            table.AddColumn("Min Temp (°C)");
            table.AddColumn("Regn (mm)");
            table.AddColumn("Max Vind (m/s)");

            // Loop igennem de første 7 dage og tilføj data til tabellen
            for (int i = 0; i < 7; i++)
            {
                // Konverterer datoen til ugenavn og format
                DateTime date = DateTime.Parse(days[i]);
                string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));

                // Formatere datoen som "Ugedag Dato"
                string dato = $"{dayOfWeek} {date.ToShortDateString()}";

                // Tilføjer rækken til tabellen
                table.AddRow(dato, maxTemps[i] + "°C", minTemps[i] + "°C", rainSum[i] + " mm", windMax[i] + " m/s");
            }

            // Viser tabellen med vejrinformation
            AnsiConsole.Write(table);
        }
        else
        {
            // Hvis data ikke kunne hentes, vises en fejlmeddelelse
            DateTime date = DateTime.Now;
            string dayOfWeek = date.ToString("dddd", new CultureInfo("da-DK"));
            Console.WriteLine($"{dayOfWeek} den {DateTime.Now.ToShortDateString()} klokken {DateTime.Now.TimeOfDay}:" +
                              $"\nKunne ikke hente dataen." +
                              $"\nKlik på en tast, for at komme tilbage");
            Console.ReadKey();
        }
    }
}