using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class HourlyUnits
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time;

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2m;

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public string WindSpeed10m;
}