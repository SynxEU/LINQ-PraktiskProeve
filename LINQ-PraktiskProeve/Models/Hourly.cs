using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class Hourly
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Time;

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> Temperature2m;

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> WindSpeed10m;
}