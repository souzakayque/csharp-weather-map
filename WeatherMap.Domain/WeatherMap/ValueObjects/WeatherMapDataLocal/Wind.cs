using WeatherMap.Shared.ValueObjects;

namespace WeatherMap.Domain.WeatherMap.ValueObjects
{
    public class Wind : ValueObject
    {
        public decimal Speed { get; set; }
        public int Deg { get; set; }
    }
}
