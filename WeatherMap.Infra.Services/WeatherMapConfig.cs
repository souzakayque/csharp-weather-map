namespace WeatherMap.Infra.Services
{
    public class WeatherMapConfig
    {
        public string Key { get; set; }
        public string WeatherUrlClient { get; set; }
        public string HistoricalUrlClient { get; set; }
        public string Historical { get; set; }
        public string Weather { get; set; }
        public string LatFilter { get; set; }
        public string LonFilter { get; set; }
        public string EndFilter { get; set; }
        public string AppIdFilter { get; set; }
    }
}
