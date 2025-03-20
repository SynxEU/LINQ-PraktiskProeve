using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class DailyUnits
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time;

    [JsonProperty("rain_sum", NullValueHandling = NullValueHandling.Ignore)]
    public string RainSum;

    [JsonProperty("temperature_2m_max", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2mMax;

    [JsonProperty("temperature_2m_min", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2mMin;

    [JsonProperty("wind_speed_10m_max", NullValueHandling = NullValueHandling.Ignore)]
    public string WindSpeed10mMax;
}