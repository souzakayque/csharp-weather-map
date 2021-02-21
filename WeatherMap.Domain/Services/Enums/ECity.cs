using System.ComponentModel;

namespace WeatherMap.Domain.Services.Enums
{
    public enum ECity
    {
        [Description("São Paulo")]
        SaoPaulo = 0,

        [Description("Rio de Janeiro")]
        RiodeJaneiro = 1,
        
        [Description("Florianópolis")]
        Florianopolis = 2,

        NotFound = 3
    }
}
