using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class CurrentUnits
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time;

    [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
    public string Interval;

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2m;

    [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
    public string Rain;

    [JsonProperty("is_day", NullValueHandling = NullValueHandling.Ignore)]
    public string IsDay;

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public string WindSpeed10m;
}