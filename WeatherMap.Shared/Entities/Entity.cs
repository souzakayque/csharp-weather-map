using Flunt.Notifications;
using System;

namespace WeatherMap.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Guid Id { get; private set; }


        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
