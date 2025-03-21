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

        static void ProcessWeatherData(Root weatherData)
        {
            bool running = true;
            string city = weatherData switch
            {
                { Latitude: >= 55.2 and <= 55.8, Longitude: >= 12.2 and <= 12.9 } => "København",
                { Latitude: >= 51.0 and <= 51.6, Longitude: >= -0.3 and <= 0.3 } => "London",
                _ => null
            };
            do
            {
                Clear();
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
                ConsoleKeyInfo choice = ReadKey();

                Clear();

                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        ShowCurrentWeather(weatherData);
                        break;
                    case ConsoleKey.D2:
                        ShowLast24Hours.ShowLast24HoursWeather(weatherData);
                        break;
                    case ConsoleKey.D3:
                        ShowNext24Hours.ShowNext24HoursSummary(weatherData);
                        break;
                    case ConsoleKey.D4:
                        Show24HoursDetail.ShowNext24HoursDetailed(weatherData);
                        break;
                    case ConsoleKey.D5:
                        ShowLastWeek.ShowLastWeekWeather(weatherData);
                        break;
                    case ConsoleKey.D6:
                        ShowNextWeek.ShowNextWeekWeather(weatherData);
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.Escape:
                        Environment.Exit(1);
                        running = false;
                        break;
                    default:
                        WriteLine("Ugyldigt valg, prøv igen.");
                        break;
                }

                if (running)
                {
                    WriteLine("\nTryk på en tast for at fortsætte...");
                    ConsoleKeyInfo keyPress = ReadKey();
                    running = keyPress.Key != ConsoleKey.Escape && keyPress.Key != ConsoleKey.D7 &&
                              keyPress.Key != ConsoleKey.NumPad7;
                    Clear();
                }
                else
                {
                    WriteLine("\nVi ses næste gang :)");
                }

            } while (running);
        }

        static void ShowCurrentWeather(Root weatherData)
        {
            try
            {
                Current current = weatherData.Current;
                if (current == null)
                {
                    WriteLine("Fejl: Ingen aktuelle vejrdata tilgængelige.");
                    return;
                }

                WriteLine("Vejrudsigten lige nu:");
                WriteLine($"Temperatur: {current.Temperature2M}°C");
                WriteLine($"Regn i dag: {current.Rain} mm");
                WriteLine($"Er det dag? {(current.IsDay == 1 ? "Ja" : "Nej")}");
            }
            catch (Exception e)
            {
                WriteLine($"Fejl ved visning af data: {e.Message}");
            }
        }
    }
}