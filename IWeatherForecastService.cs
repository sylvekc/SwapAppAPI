using SwapApp;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get(int count, int minTemperature, int maxTemperature);
}