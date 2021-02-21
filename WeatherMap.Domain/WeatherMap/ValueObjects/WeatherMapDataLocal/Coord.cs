using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects
{
    public class Coord : ValueObject
    {
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
    }
}
