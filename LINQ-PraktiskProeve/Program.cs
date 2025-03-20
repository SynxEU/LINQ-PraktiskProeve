using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using LINQ_PraktiskProeve.Day;
using LINQ_PraktiskProeve.JSON;
using LINQ_PraktiskProeve.Models;
using LINQ_PraktiskProeve.Week;

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
            
            try
            {
                List<Root> root = Get.GetWeatherDataAsync(url);

                foreach (Root roots in root)
                    ProcessWeatherData(roots);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fejl ved hentning af data: {e.Message}");
            }
        }

        static void ProcessWeatherData(Root weatherData)
        {
            bool running = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Vælg en mulighed:");
                Console.WriteLine("1. Vejr lige nu");
                Console.WriteLine("2. Vejret de seneste 24 timer");
                Console.WriteLine("3. Vejret for de næste 24 timer");
                Console.WriteLine("4. Time-for-time vejr de næste 24 timer");
                Console.WriteLine("5. Vejret den seneste uge");
                Console.WriteLine("6. Vejret den næste uge");
                Console.WriteLine("7. Afslut");
                Console.Write("Indtast valg (1-7): ");
                ConsoleKeyInfo choice = Console.ReadKey();

                Console.Clear();

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
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nTryk på en tast for at fortsætte...");
                    ConsoleKeyInfo keyPress = Console.ReadKey();
                    running = keyPress.Key != ConsoleKey.Escape && keyPress.Key != ConsoleKey.D7 &&
                              keyPress.Key != ConsoleKey.NumPad7;
                    Console.Clear();
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
                    Console.WriteLine("Fejl: Ingen aktuelle vejrdata tilgængelige.");
                    return;
                }

                Console.WriteLine("Vejrudsigten lige nu:");
                Console.WriteLine($"Temperatur: {current.Temperature2m}°C");
                Console.WriteLine($"Regn i dag: {current.Rain} mm");
                Console.WriteLine($"Er det dag? {(current.IsDay == 1 ? "Ja" : "Nej")}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Fejl ved visning af data.");
            }
        }
    }
}

