using System.Collections.Generic;
using WeatherMap.Domain.WeatherMap.ValueObjects.WeatherMapEntity;
using WeatherMap.Shared.Entities;

namespace WeatherMap.Domain.WeatherMap.Entities
{
    public class WeatherMap : Entity
    {
        public string City { get; set; }
        public List<WeatherMapInfo> WeatherInfo { get; set; }

        protected WeatherMap() { }

        public WeatherMap(string city, List<WeatherMapInfo> weatherInfo)
        {
            City = city;
            WeatherInfo = weatherInfo;
        }
    }
}
