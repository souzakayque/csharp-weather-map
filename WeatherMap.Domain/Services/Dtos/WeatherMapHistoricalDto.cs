using System.Collections.Generic;
using WeatherMap.Domain.WeatherMap.ValueObjects.WeatherMapHistorical;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.Services.Dtos
{
    public class WeatherMapHistoricalDto : ICommandResult
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Timezone { get; set; }
        public int Timezone_Offset { get; set; }
        public Current Current { get; set; }
        public List<Hourly> Hourly { get; set; }
    }
}
