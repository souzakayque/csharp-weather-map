using System;
using System.Globalization;
using System.Text;
using WeatherMap.Domain.Services.Enums;

namespace WeatherMap.Domain.WeatherMap.Common
{
    public class Helper
    {
        public string TreatCityName(string cityName)
        {
            StringBuilder result = new StringBuilder();
            var arrayText = cityName.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark && letter != ';')
                    result.Append(letter);
            }

            result.Replace(" ", "");

            return result.ToString().ToLower();
        }

        public ECity GetCityName(ETreatedCity treatedCityName)
        {
            switch (treatedCityName)
            {
                case ETreatedCity.saopaulo:
                    return ECity.SaoPaulo;

                case ETreatedCity.riodejaneiro:
                    return ECity.RiodeJaneiro;

                case ETreatedCity.florianopolis:
                    return ECity.Florianopolis;

                default:
                    return ECity.NotFound;
            }
        }

        public DateTime UnixTimeStampToDateTime(long unixDate)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            result = result.AddSeconds(unixDate).ToLocalTime();
            return result;
        }

        public decimal KelvinToCensius(decimal kelvin)
        {
            return kelvin - (decimal)273.15;
        }
        
    }
}
