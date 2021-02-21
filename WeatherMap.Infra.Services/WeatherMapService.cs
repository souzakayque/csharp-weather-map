using AutoMapper;
using Newtonsoft.Json;
using System;
using WeatherMap.Domain.Services;
using WeatherMap.Domain.Services.Dtos;
using WeatherMap.Domain.WeatherMap.Common;
using WeatherMap.Infra.Services.Handler;

namespace WeatherMap.Infra.Services
{
    public class WeatherMapService : IWeatherMapService
    {
        private readonly WeatherMapConfig _config;
        private readonly IWeatherMapService weatherMapService;
        private readonly IMapper mapper;
        private static Lazy<WeatherMapService> _instance;

        public static WeatherMapService Instance(WeatherMapConfig config)
        {
            try
            {
                if (_instance == null)
                    _instance = new Lazy<WeatherMapService>(() => new WeatherMapService(config));

                return _instance.Value;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private WeatherMapService(WeatherMapConfig config)
        {
            try
            {
                _config = config;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public WeatherMapService(IWeatherMapService weatherMapService, IMapper mapper)
        {
            this.weatherMapService = weatherMapService;
            this.mapper = mapper;
        }

        public WeatherMapHistoricalDto GetHistoricalWeather(decimal latitude, decimal longitude, long finalDate)
        {
            try
            {
                if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                    throw new Exception("Latitude and Longitude is out of valid range.");

                var jsonResult = new RequestHandler().Get(
                    _config.WeatherUrlClient, 
                    String.Format("{0}{1}{2}{3}{4}",
                        _config.Historical,
                        _config.LatFilter + latitude,
                        _config.LonFilter + longitude,
                        _config.EndFilter + finalDate,
                        _config.AppIdFilter + _config.Key)
                    );

                var result = JsonConvert.DeserializeObject<WeatherMapHistoricalDto>(jsonResult);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public WeatherMapDataLocationDto GetLocation(string cityName)
        {
            try
            {
                if (string.IsNullOrEmpty(cityName))
                    throw new Exception("cityName cannot be empty");

                var jsonResult = new RequestHandler().Get(
                    _config.WeatherUrlClient,
                    String.Format("{0}{1}",
                        _config.Weather + cityName,
                        _config.AppIdFilter + _config.Key));

                var result = JsonConvert.DeserializeObject<WeatherMapDataLocationDto>(jsonResult);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
