using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class DailyUnits
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time = string.Empty;

    [JsonProperty("rain_sum", NullValueHandling = NullValueHandling.Ignore)]
    public string RainSum = string.Empty;

    [JsonProperty("temperature_2m_max", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2MMax = string.Empty;

    [JsonProperty("temperature_2m_min", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature2MMin = string.Empty;

    [JsonProperty("wind_speed_10m_max", NullValueHandling = NullValueHandling.Ignore)]
    public string WindSpeed10mMax = string.Empty;
}