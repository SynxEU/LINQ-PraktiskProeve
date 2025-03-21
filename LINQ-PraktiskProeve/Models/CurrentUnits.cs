using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class CurrentUnits
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time = string.Empty;

    [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
    public string Interval = string.Empty;

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2M = string.Empty;

    [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
    public string Rain = string.Empty;

    [JsonProperty("is_day", NullValueHandling = NullValueHandling.Ignore)]
    public string IsDay = string.Empty;

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public string WindSpeed10m = string.Empty;
}