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
    public string Timezone = String.Empty;

    [JsonProperty("timezone_abbreviation", NullValueHandling = NullValueHandling.Ignore)]
    public string TimezoneAbbreviation  = String.Empty;

    [JsonProperty("elevation", NullValueHandling = NullValueHandling.Ignore)]
    public double Elevation;

    [JsonProperty("current_units", NullValueHandling = NullValueHandling.Ignore)]
    public CurrentUnits CurrentUnits = new();

    [JsonProperty("current", NullValueHandling = NullValueHandling.Ignore)]
    public Current Current = new();

    [JsonProperty("hourly_units", NullValueHandling = NullValueHandling.Ignore)]
    public HourlyUnits HourlyUnits = new();

    [JsonProperty("hourly", NullValueHandling = NullValueHandling.Ignore)]
    public Hourly Hourly = new();

    [JsonProperty("daily_units", NullValueHandling = NullValueHandling.Ignore)]
    public DailyUnits DailyUnits = new();

    [JsonProperty("daily", NullValueHandling = NullValueHandling.Ignore)]
    public Daily Daily = new();

    [JsonProperty("location_id", NullValueHandling = NullValueHandling.Ignore)]
    public int? LocationId;
}