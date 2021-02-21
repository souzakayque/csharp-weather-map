using System.Collections.Generic;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.WeatherMap.Queries.Results
{
    public class GetWeatherMapQueryResult : ICommandResult
    {
        public Entities.WeatherMap Data { get; set; }

        protected GetWeatherMapQueryResult() { } 

        public GetWeatherMapQueryResult(Entities.WeatherMap data)
        {
            Data = data;
        }
    }

    public class GetWeathersMapQueryResult : ICommandResult
    {
        public List<Entities.WeatherMap> Data { get; set; }

        protected GetWeathersMapQueryResult() { }

        public GetWeathersMapQueryResult(List<Entities.WeatherMap> data)
        {
            Data = data;
        }
    }
}
