using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using LINQ_PraktiskProeve.Day;
using LINQ_PraktiskProeve.JSON;
using LINQ_PraktiskProeve.Models;
using LINQ_PraktiskProeve.Week;
using static System.Console;

namespace LINQ_PraktiskProeve
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "RiderProjects",
                "LINQ-PraktiskProeve"
            );
            string envFilePath = Path.Combine(folderPath, "config.env");
            Env.Load(envFilePath);
            string? url = Environment.GetEnvironmentVariable("API_URL");

            WriteLine("Vælg en by:");
            WriteLine("1. København");
            WriteLine("2. London");
            Write("Indtast valg (1-2): ");

            ConsoleKeyInfo locationChoice = ReadKey();
            string selectedLocation = string.Empty;

            if (locationChoice.Key == ConsoleKey.D1 || locationChoice.Key == ConsoleKey.NumPad1)
            {
                selectedLocation = "Copenhagen";
            }
            else if (locationChoice.Key == ConsoleKey.D2 || locationChoice.Key == ConsoleKey.NumPad2)
            {
                selectedLocation = "London";
            }
            else
            {
                WriteLine("Ugyldigt valg.");
                return;
            }

            try
            {
                List<Root> root = Get.GetWeatherDataAsync(url);
                
                Root selectedWeatherData = selectedLocation 
                    switch
                {
                    "Copenhagen" => root.Find(r => r.Latitude == 55.6785 && r.Longitude == 12.570435),
                    "London" => root.Find(r => r.Latitude == 51.5 && r.Longitude == -0.120000124),
                    _ => null
                };

                if (selectedWeatherData != null)
                {
                    ProcessWeatherData(selectedWeatherData);
                }
                else
                {
                    WriteLine("Der blev ikke fundet vejrdata for den valgte by.");
                }
            }
            catch (Exception e)
            {
                WriteLine($"Fejl ved hentning af data: {e.Message}");
            }
        }

        /// <summary>
        /// Denne metode viser vejrinformationer for en by baseret på geografiske koordinater (bredde- og længdegrad).
        /// Brugeren får en menu med forskellige muligheder for at se vejret for nuværende, de sidste 24 timer, de næste 24 timer, timeløst vejr for de næste 24 timer, vejret for den seneste uge, og vejret for den næste uge.
        /// Brugeren kan afslutte programmet ved at vælge "Afslut" eller trykke på Escape-tasten.
        /// </summary>
        static void ProcessWeatherData(Root weatherData)
        {
            bool running = true;

            // Bestem byen baseret på latitude og longitude
            string city = weatherData switch
            {
                { Latitude: >= 55.2 and <= 55.8, Longitude: >= 12.2 and <= 12.9 } => "København", // København
                { Latitude: >= 51.0 and <= 51.6, Longitude: >= -0.3 and <= 0.3 } => "London", // London
                _ => null // Standardværdi, hvis ingen by matcher
            };

            do
            {
                // Ryd skærmen og opdater
                Clear();
                WriteLine("\x1b[3J");
                Clear();

                // Vis menuen med vejrmuligheder
                WriteLine($"Vejrdata for {city}");
                WriteLine("Vælg en mulighed:");
                WriteLine("1. Vejr lige nu");
                WriteLine("2. Vejret de seneste 24 timer");
                WriteLine("3. Vejret for de næste 24 timer");
                WriteLine("4. Time-for-time vejr de næste 24 timer");
                WriteLine("5. Vejret den seneste uge");
                WriteLine("6. Vejret den næste uge");
                WriteLine("7. Afslut");
                Write("Indtast valg (1-7): ");

                // Fang brugerens valg
                ConsoleKeyInfo choice = ReadKey();

                // Ryd skærmen efter input
                Clear();
                WriteLine("\x1b[3J");
                Clear();

                // Behandl brugerens valg baseret på input
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        ShowCurrentWeather(weatherData); // Vis nuværende vejr
                        break;
                    case ConsoleKey.D2:
                        ShowLast24Hours.ShowLast24HoursWeather(weatherData); // Vis vejr for de sidste 24 timer
                        break;
                    case ConsoleKey.D3:
                        ShowNext24Hours.ShowNext24HoursSummary(weatherData); // Vis opsummering for de næste 24 timer
                        break;
                    case ConsoleKey.D4:
                        ShowNext24HoursDetail.ShowNext24HoursDetailed(weatherData); // Vis detaljeret time-for-time vejr
                        break;
                    case ConsoleKey.D5:
                        ShowLastWeek.ShowLastWeekWeather(weatherData); // Vis vejr for den seneste uge
                        break;
                    case ConsoleKey.D6:
                        ShowNextWeek.ShowNextWeekWeather(weatherData); // Vis vejr for den næste uge
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.Escape:
                        running = false; // Afslut programmet
                        Environment.Exit(1);
                        break;
                    default:
                        WriteLine("Ugyldigt valg, prøv igen."); // Fejlmeddelelse ved ugyldigt valg
                        break;
                }

                // Kontroller om løkken skal fortsætte
                if (running)
                {
                    WriteLine("\nTryk på en tast for at fortsætte...");
                    ConsoleKeyInfo keyPress = ReadKey();
                    running = keyPress.Key != ConsoleKey.Escape && keyPress.Key != ConsoleKey.D7 &&
                              keyPress.Key != ConsoleKey.NumPad7;
                    Clear();
                }

            } while (running); // Fortsæt løkken, hvis 'running' er sandt
        }

        /// <summary>
        /// Denne metode viser de aktuelle vejrinformationer for en given by baseret på de modtagne vejrdata.
        /// Hvis de aktuelle vejrinformationer ikke er tilgængelige, vises en fejlmeddelelse. 
        /// Metoden håndterer også eventuelle fejl, der opstår under visningen af vejret.
        /// </summary>
        static void ShowCurrentWeather(Root weatherData)
        {
            try
            {
                // Hent de aktuelle vejrinformationer
                Current current = weatherData.Current;
        
                // Hvis der ikke er nogen aktuelle vejrinformationer
                if (current == null)
                {
                    WriteLine("Fejl: Ingen aktuelle vejrdata tilgængelige.");
                    return;
                }

                // Vis aktuelle vejrinformationer
                WriteLine("Vejrudsigten lige nu:");
                WriteLine($"Temperatur: {current.Temperature2M}°C"); // Temperatur
                WriteLine($"Regn i dag: {current.Rain} mm"); // Regn
                WriteLine($"Er det dag? {(current.IsDay == 1 ? "Ja" : "Nej")}"); // Dag/nat status
            }
            catch (Exception e)
            {
                // Håndter fejl, der opstår under visning af data
                WriteLine($"Fejl ved visning af data: {e.Message}");
            }
        }
    }
}