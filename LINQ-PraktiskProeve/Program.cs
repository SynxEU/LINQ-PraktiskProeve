using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LINQ_PraktiskProeve.Models;
using Newtonsoft.Json;

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
                List<Root> root = await GetWeatherDataAsync(url);

                foreach (Root roots in root)
                    ProcessWeatherData(roots);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fejl ved hentning af data: {e.Message}");
            }
        }

        public static async Task<List<Root>> GetWeatherDataAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                List<Root> weatherData = JsonConvert.DeserializeObject<List<Root>>(content);
                SaveJsonToFile(content);

                return weatherData;
            }
        }




        static void SaveJsonToFile(string json)
        {
            string folderPath = @"C:\Users\jonas\Desktop\Skole\H3\LINQ-PraktiskProeve\LINQ-PraktiskProeve";
            string filePath = Path.Combine(folderPath, "weather_data.json");

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.WriteAllText(filePath, json.ToString());
                Console.WriteLine($"JSON gemt i: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved gemning af JSON: {ex.Message}");
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
                        ShowLast24HoursWeather(weatherData);
                        break;
                    case "3":
                        ShowLastWeekWeather(weatherData);
                        break;
                    case "4":
                        ShowNext24HoursSummary(weatherData);
                        break;
                    case "5":
                        ShowNext24HoursDetailed(weatherData);
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

        static void ShowLast24HoursWeather(Root weatherData)
        {
            var times = weatherData.Hourly?.Time;
            var temperatures = weatherData.Hourly?.Temperature2m;
            var windSpeeds = weatherData.Hourly?.WindSpeed10m;

            if (times != null && temperatures != null && windSpeeds != null)
            {
                Console.WriteLine("\nVejret de seneste 24 timer:");
                for (int i = 0; i < 25; i++)
                {
                    Console.WriteLine(
                        $"Tid: {DateTime.Parse(times[i]).ToShortDateString()} {DateTime.Parse(times[i]).TimeOfDay} " +
                        $"| Temp: {temperatures[i]}°C " +
                        $"| Vind: {windSpeeds[i]} m/s");
                }
            }
        }

        static void ShowLastWeekWeather(Root weatherData)
        {
            var days = weatherData.Daily?.Time;
            var maxTemps = weatherData.Daily?.Temperature2mMax;
            var minTemps = weatherData.Daily?.Temperature2mMin;
            var rainSum = weatherData.Daily?.RainSum;
            var windMax = weatherData.Daily?.WindSpeed10mMax;

            if (days != null && maxTemps != null && minTemps != null && rainSum != null && windMax != null)
            {
                Console.WriteLine("\nVejret den seneste uge:");
                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine($"Dag: {DateTime.Parse(days[i]).DayOfWeek} | Dato: {DateTime.Parse(days[i]).ToShortDateString()}");
                    Console.WriteLine($"Max Temp: {maxTemps[i]}°C | Min Temp: {minTemps[i]}°C");
                    Console.WriteLine($"Regn: {rainSum[i]} mm | Max vind: {windMax[i]} m/s");
                    Console.WriteLine("--------------------------------------------------");
                }
            }
        }

        static void ShowNext24HoursSummary(Root weatherData)
        {
            if (weatherData.Hourly != null)
            {
                int currentHour = DateTime.Now.Hour;
                var temperatures = weatherData.Hourly.Temperature2m;
                var windSpeeds = weatherData.Hourly.WindSpeed10m;

                if (temperatures != null && windSpeeds != null)
                {
                    List<double> next24Temps = new List<double>();
                    List<double> next24Winds = new List<double>();

                    foreach (var temperature in temperatures.Skip(currentHour).Take(25).ToList())
                        next24Temps.Add(double.Parse(temperature.ToString()));

                    foreach (var wind in windSpeeds.Skip(currentHour).Take(25).ToList())
                        next24Winds.Add(double.Parse(wind.ToString()));

                    double maxTemp = next24Temps.Max();
                    double minTemp = next24Temps.Min();
                    double avgWindSpeed = next24Winds.Average();

                    Console.WriteLine("\nVejr for de næste 24 timer:");
                    Console.WriteLine($"Højeste Temp: {maxTemp}°C");
                    Console.WriteLine($"Laveste Temp: {minTemp}°C");
                    Console.WriteLine($"Gennemsnitlig Vindhastighed: {avgWindSpeed:F2} m/s");
                }
            }
        }

        static void ShowNext24HoursDetailed(Root weatherData)
        {
            if (weatherData.Hourly != null)
            {
                int currentHour = DateTime.Now.Hour + 14;
                var times = weatherData.Hourly.Time;
                var temperatures = weatherData.Hourly.Temperature2m;
                var windSpeeds = weatherData.Hourly.WindSpeed10m;

                if (times != null && temperatures != null && windSpeeds != null)
                {
                    var weatherList =
                        times.Zip(temperatures, (t, temp) => new { Time = t, Temp = temp })
                            .Zip(windSpeeds, (entry, wind) => new { entry.Time, entry.Temp, Wind = wind })
                            .Skip(currentHour)
                            .Take(25)
                            .OrderByDescending(x => x.Time)
                            .ToList();

                    Console.WriteLine("\nTime-for-time vejr de næste 24 timer (sorteret faldende):");
                    foreach (var entry in weatherList)
                    {
                        Console.WriteLine(
                            $"Tid: {DateTime.Parse(entry.Time).ToShortDateString()} {DateTime.Parse(entry.Time).TimeOfDay} " +
                            $"| Temp: {entry.Temp}°C " +
                            $"| Vind: {entry.Wind} m/s");
                    }
                }
            }
        }
    }
}

