using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using WeatherMap.Domain.Services;
using WeatherMap.Domain.Services.Enums;
using WeatherMap.Domain.WeatherMap.Command.Inputs;
using WeatherMap.Domain.WeatherMap.Common;
using WeatherMap.Domain.WeatherMap.Queries.Results;
using WeatherMap.Domain.WeatherMap.ValueObjects.WeatherMapEntity;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.WeatherMap.Command.Handlers
{
    public class WeatherMapCommandHandler : Notifiable, IWeatherMapCommandHandler
    {
        private readonly IWeatherMapService _service;

        public WeatherMapCommandHandler(IWeatherMapService service)
        {
            _service = service;
        }

        public ICommandResult Handle(WeatherMapCommand command, string id)
        {
            try
            {
                if (string.IsNullOrEmpty(command.CityName))
                {
                    AddNotification("CityName", "This value cannot be null.");
                    return null;
                }

                if (command.InitialDate >= command.FinalDate)
                {
                    AddNotification("Date", "Initial date cannot be greater then final date.");
                    return null;
                }

                if (command.FinalDate > DateTime.Today || command.FinalDate < DateTime.Today.AddDays(-5) || command.InitialDate > DateTime.Today || command.InitialDate < DateTime.Today.AddDays(-5))
                {
                    AddNotification("Date", "Initial and final date must be into a range of today and five days ago.");
                    return null;
                }

                Helper helper = new Helper();
                var treatedCityName = helper.TreatCityName(command.CityName);

                if (!Enum.IsDefined(typeof(ETreatedCity), treatedCityName))
                {
                    AddNotification("CityName", "This city is not valid.");
                    return null;
                }

                var enumCityName = helper.GetCityName((ETreatedCity)Enum.Parse(typeof(ETreatedCity), treatedCityName));

                if (enumCityName == ECity.NotFound)
                {
                    AddNotification("CityName", "This city is not valid.");
                    return null;
                }

                string city = enumCityName.GetType().GetMember(enumCityName.ToString()).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>().Description;

                var locationData = _service.GetLocation(city);

                if (string.IsNullOrEmpty(locationData.Name))
                {
                    AddNotification("Location", "Location not found.");
                    return null;
                }

                long finalDate = ((DateTimeOffset)command.FinalDate).ToUnixTimeSeconds();

                var latitude = locationData.Coord.Lat;
                var longitude = locationData.Coord.Lon;

                var historical = _service.GetHistoricalWeather(latitude, longitude, finalDate);

                if (string.IsNullOrEmpty(historical.Timezone))
                {
                    AddNotification("Location", "Weather historical location not found.");
                    return null;
                }

                var parsedDate = helper.UnixTimeStampToDateTime(historical.Current.Dt);
                var parsedTemp = helper.KelvinToCensius(historical.Current.Temp);
                var parsedFeelsLike = helper.KelvinToCensius(historical.Current.Feels_Like);
                WeatherMapInfo eachWeatherMap = new WeatherMapInfo(parsedDate, parsedTemp, parsedFeelsLike, historical.Current.Humidity, historical.Current.Weather.FirstOrDefault().Description, historical.Current.Weather.FirstOrDefault().Main);

                List<WeatherMapInfo> infoList = new List<WeatherMapInfo>();
                infoList.Add(eachWeatherMap);

                foreach (var h in historical.Hourly)
                {
                    parsedDate = helper.UnixTimeStampToDateTime(h.Dt);
                    parsedTemp = helper.KelvinToCensius(h.Temp);
                    parsedFeelsLike = helper.KelvinToCensius(h.Feels_Like);
                    eachWeatherMap = new WeatherMapInfo(parsedDate, parsedTemp, parsedFeelsLike, h.Humidity, h.Weather.FirstOrDefault().Description, h.Weather.FirstOrDefault().Main);
                    infoList.Add(eachWeatherMap);
                }

                infoList = infoList.Where(y => y.Date.Day >= command.InitialDate.Day && y.Date.Day <= command.FinalDate.Day).OrderBy(x => x.Date).ToList();
                Entities.WeatherMap weatherMapList = new Entities.WeatherMap(city, infoList);

                var result = new GetWeatherMapQueryResult(weatherMapList);

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult Handle(WeatherMapPeriodicallyCommand command, string id)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Date.ToString()))
                {
                    AddNotification("Date", "Date cannot be null.");
                    return null;
                }

                Helper helper = new Helper();
                var validCities = Enum.GetValues(typeof(ETreatedCity));

                Entities.WeatherMap weatherMap = null;
                List<Entities.WeatherMap> infoList = new List<Entities.WeatherMap>();

                List<WeatherMapInfo> weatherMapInfoList = new List<WeatherMapInfo>();
                foreach (var city in validCities)
                {
                    weatherMapInfoList = new List<WeatherMapInfo>();
                    var enumCityName = helper.GetCityName((ETreatedCity)Enum.Parse(typeof(ETreatedCity), city.ToString()));
                    string cityName = enumCityName.GetType().GetMember(enumCityName.ToString()).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>().Description;

                    var locationData = _service.GetLocation(cityName);

                    if (string.IsNullOrEmpty(locationData.Name))
                    {
                        AddNotification("Location", "Location not found.");
                        return null;
                    }

                    var parsedDate = helper.UnixTimeStampToDateTime(locationData.Dt);
                    var parsedTemp = helper.KelvinToCensius(locationData.Main.Temp);
                    var parsedFeelsLike = helper.KelvinToCensius(locationData.Main.Feels_Like);

                    WeatherMapInfo eachWeatherMap = new WeatherMapInfo(parsedDate, parsedTemp, parsedFeelsLike, locationData.Main.Humidity, locationData.Weather.FirstOrDefault().Description, locationData.Weather.FirstOrDefault().Main);
                    weatherMapInfoList.Add(eachWeatherMap);
                    
                    weatherMap = new Entities.WeatherMap(cityName, weatherMapInfoList);
                    infoList.Add(weatherMap);
                }
                
                var result = new GetWeathersMapQueryResult(infoList);

                return result;
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
