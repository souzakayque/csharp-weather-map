using Flunt.Notifications;
using System.Collections.Generic;

namespace WeatherMap.API.Models
{
    public class RequestResult
    {
        public object Result { get; private set; }
        public IEnumerable<Notification> Notifications { get; private set; } = new List<Notification>();


        public RequestResult(object result, IEnumerable<Notification> notifications)
        {
            Result = result;
            Notifications = notifications;
        }

        public RequestResult(object result)
        {
            Result = result;
        }
    }
}
