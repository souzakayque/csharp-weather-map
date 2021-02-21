using System;
using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects.WeatherMapEntity
{
    public class WeatherMapInfo : ValueObject
    {
        public DateTime Date { get; set; }
        public decimal Temperature { get; set; }
        public decimal FeelsLike { get; set; }
        public int Humidity { get; set; }
        public string TemperatureDescription { get; set; }
        public string MainDescription { get; set; }

        protected WeatherMapInfo() { }

        public WeatherMapInfo(DateTime date, decimal temperature, decimal feelsLike, int humidity, string temperatureDescription, string mainDescription)
        {
            Date = date;
            Temperature = temperature;
            FeelsLike = feelsLike;
            Humidity = humidity;
            TemperatureDescription = temperatureDescription;
            MainDescription = mainDescription;
        }
    }
}
