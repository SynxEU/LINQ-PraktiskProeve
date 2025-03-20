using LINQ_PraktiskProeve.Day;
using LINQ_PraktiskProeve.JSON;
using LINQ_PraktiskProeve.Models;
using LINQ_PraktiskProeve.Week;

namespace LINQ_PraktiskProeve
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var url =
                "https://api.open-meteo.com/v1/forecast?latitude=55.6759,51.5085&longitude=12.5655,-0.1257&daily=rain_sum,temperature_2m_max,temperature_2m_min,wind_speed_10m_max&hourly=temperature_2m,wind_speed_10m&current=temperature_2m,rain,is_day,wind_speed_10m&timezone=Europe%2FBerlin&past_days=7&past_hours=24";

            try
            {
                List<Root> root = await Get.GetWeatherDataAsync(url);

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
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Vælg en mulighed:");
                Console.WriteLine("1. Vejr lige nu");
                Console.WriteLine("2. Vejret de seneste 24 timer");
                Console.WriteLine("3. Vejret den seneste uge");
                Console.WriteLine("4. Vejret for de næste 24 timer");
                Console.WriteLine("5. Time-for-time vejr de næste 24 timer (sorteret)");
                Console.WriteLine("6. Afslut");
                Console.Write("Indtast valg (1-6): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCurrentWeather(weatherData);
                        break;
                    case "2":
                        ShowLast24Hours.ShowLast24HoursWeather(weatherData);
                        break;
                    case "3":
                        ShowLastWeek.ShowLastWeekWeather(weatherData);
                        break;
                    case "4":
                        ShowNext24Hours.ShowNext24HoursSummary(weatherData);
                        break;
                    case "5":
                        Show24HoursDetail.ShowNext24HoursDetailed(weatherData);
                        break;
                    case "6":
                        running = false;
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nTryk på en tast for at fortsætte...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowCurrentWeather(Root weatherData)
        {
            try
            {
                var current = weatherData.Current;
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

