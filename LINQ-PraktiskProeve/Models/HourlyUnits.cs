using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class HourlyUnits
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time = string.Empty;

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2M = string.Empty;

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public string WindSpeed10M = string.Empty;
}