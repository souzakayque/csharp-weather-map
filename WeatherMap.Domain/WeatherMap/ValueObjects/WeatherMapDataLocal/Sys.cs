using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects
{
    public class Sys : ValueObject
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
