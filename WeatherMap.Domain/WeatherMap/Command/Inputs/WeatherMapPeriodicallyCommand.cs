using System;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.WeatherMap.Command.Inputs
{
    public class WeatherMapPeriodicallyCommand : ICommand
    {
        public DateTime Date { get; set; }
        public string Token { get; set; }

        public void SetDate(DateTime date)
        {
            Date = date;
        }

        public void SetToken(string token)
        {
            Token = token;
        }

    }
}
