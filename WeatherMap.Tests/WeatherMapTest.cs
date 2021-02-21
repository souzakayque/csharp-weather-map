using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherMap.Domain.Services;
using WeatherMap.Infra.Services;
using Xunit;

namespace WeatherMap.Tests
{
    public class WeatherMapTest
    {
        private WeatherMapService _service;

        public WeatherMapTest()
        {
            _service = new WeatherMapService(new Mock<IWeatherMapService>().Object, new Mock<IMapper>().Object);
        }

        [Fact]
        public void GetLocation_SendingEmptyValues()
        {
            var city = string.Empty;
            var exception = Assert.Throws<Exception>(() => _service.GetLocation(city));
        }

        [Fact]
        public void GetHistoricalWeather_SendingEmptyValues()
        {
            var latitude = 91;
            var longitude = 181;
            var date = long.MinValue;
            var exception = Assert.Throws<Exception>(() => _service.GetHistoricalWeather(latitude, longitude, date));
        }
    }
}
