using MVCProject.ITI.Models.Trip;
namespace MVCProject.ITI.Serviceslayer.Trip
{
    public interface IWeatherService
    {
        // used Task to work without waiting the API to load.
        Task<WeatherResult> GetWeatherAsync(string cityName);
    }
}
