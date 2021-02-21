using System;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.WeatherMap.Command.Inputs
{
    public class WeatherMapCommand : ICommand
    {
        public string CityName { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public string Token { get; set; }

        public void SetToken(string token)
        {
            Token = token;
        }

        public void SetData(string cityName, DateTime initialDate, DateTime finalDate)
        {
            CityName = cityName;
            InitialDate = initialDate;
            FinalDate = finalDate;
        }
    }
}
