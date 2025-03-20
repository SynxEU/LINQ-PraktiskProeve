using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class Current
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time;

    [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
    public int Interval;

    [JsonProperty("temperature_2m", NullValueHandling = NullValueHandling.Ignore)]
    public double Temperature2m;

    [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
    public double Rain;

    [JsonProperty("is_day", NullValueHandling = NullValueHandling.Ignore)]
    public int IsDay;

    [JsonProperty("wind_speed_10m", NullValueHandling = NullValueHandling.Ignore)]
    public double WindSpeed10m;   
}