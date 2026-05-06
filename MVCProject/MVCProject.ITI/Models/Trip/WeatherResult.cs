namespace MVCProject.ITI.Models.Trip
{
    // this class used to download the data from the OpenWeatherAPI to convert it to Parse to use it
    public class WeatherResult
    {
        public string CityName { get; set; } = string.Empty;
        public float TemperatureC { get; set; }
        public string Condition { get; set; } = string.Empty;
        // Type of Weather => "Clear" أو "Rain" أو "Clouds"
        public float WindSpeedMps { get; set; }
        public bool IsHot => TemperatureC > 35;
        // check if the Temerature if large than 35 then it hot
        public bool IsRainy => Condition.Contains("Rain",
            StringComparison.OrdinalIgnoreCase);
        // check if the condition has a word => Rain
        public bool IsWindy => WindSpeedMps > 10;
        // check if the wind large than 10 then it windy
    }
}
