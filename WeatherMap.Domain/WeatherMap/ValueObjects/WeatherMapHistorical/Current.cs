using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects.WeatherMapHistorical
{
    public class Current : ValueObject
    {
        public long Dt { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public decimal Temp { get; set; }
        public decimal Feels_Like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public decimal Dew_Point { get; set; }
        public decimal Uvi { get; set; }
        public int Clouds { get; set; }
        public int Visibility { get; set; }
        public decimal Wind_Speed { get; set; }
        public int Wind_Deg { get; set; }
        public Weather[] Weather { get; set; }
    }
}
