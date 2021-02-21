using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects
{
    public class Weather : ValueObject
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
