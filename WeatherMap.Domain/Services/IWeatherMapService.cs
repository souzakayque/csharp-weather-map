using System;
using WeatherMap.Domain.Services.Dtos;

namespace WeatherMap.Domain.Services
{
    public interface IWeatherMapService
    {
        WeatherMapDataLocationDto GetLocation(string cityName);
        WeatherMapHistoricalDto GetHistoricalWeather(decimal latitude, decimal longitude, long finalDate);
    }
}
