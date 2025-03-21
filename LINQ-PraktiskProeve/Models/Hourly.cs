using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class Hourly
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Time = new();

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> Temperature2M = new();

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> WindSpeed10M = new();
}