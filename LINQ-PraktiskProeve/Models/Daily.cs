using System.Collections.Generic;
using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class Daily
{
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Time;

    [JsonProperty("rain_sum", NullValueHandling = NullValueHandling.Ignore)]
    public List<double> RainSum;

    [JsonProperty("temperature_2m_max", NullValueHandling = NullValueHandling.Ignore)]
    public List<double> Temperature2mMax;

    [JsonProperty("temperature_2m_min", NullValueHandling = NullValueHandling.Ignore)]
    public List<double> Temperature2mMin;

    [JsonProperty("wind_speed_10m_max", NullValueHandling = NullValueHandling.Ignore)]
    public List<double> WindSpeed10mMax;
}