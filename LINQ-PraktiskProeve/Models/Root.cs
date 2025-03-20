using Newtonsoft.Json;

namespace LINQ_PraktiskProeve.Models;

public class Root
{
    [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
    public double Latitude;

    [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
    public double Longitude;

    [JsonProperty("generationtime_ms", NullValueHandling = NullValueHandling.Ignore)]
    public double GenerationtimeMs;

    [JsonProperty("utc_offset_seconds", NullValueHandling = NullValueHandling.Ignore)]
    public int UtcOffsetSeconds;

    [JsonProperty("timezone", NullValueHandling = NullValueHandling.Ignore)]
    public string Timezone;

    [JsonProperty("timezone_abbreviation", NullValueHandling = NullValueHandling.Ignore)]
    public string TimezoneAbbreviation;

    [JsonProperty("elevation", NullValueHandling = NullValueHandling.Ignore)]
    public double Elevation;

    [JsonProperty("current_units", NullValueHandling = NullValueHandling.Ignore)]
    public CurrentUnits CurrentUnits;

    [JsonProperty("current", NullValueHandling = NullValueHandling.Ignore)]
    public Current Current;

    [JsonProperty("hourly_units", NullValueHandling = NullValueHandling.Ignore)]
    public HourlyUnits HourlyUnits;

    [JsonProperty("hourly", NullValueHandling = NullValueHandling.Ignore)]
    public Hourly Hourly;

    [JsonProperty("daily_units", NullValueHandling = NullValueHandling.Ignore)]
    public DailyUnits DailyUnits;

    [JsonProperty("daily", NullValueHandling = NullValueHandling.Ignore)]
    public Daily Daily;

    [JsonProperty("location_id", NullValueHandling = NullValueHandling.Ignore)]
    public int? LocationId;
}