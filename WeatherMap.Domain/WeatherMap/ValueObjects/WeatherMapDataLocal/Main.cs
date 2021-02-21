using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects
{
    public class Main : ValueObject
    {
        public decimal Temp { get; set; }
        public decimal Feels_Like { get; set; }
        public decimal Temp_Min { get; set; }
        public decimal Temp_Max { get; set; }
        public decimal Pressure { get; set; }
        public int Humidity { get; set; }
    }
}
