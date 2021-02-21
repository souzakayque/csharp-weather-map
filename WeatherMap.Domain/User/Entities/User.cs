using WeatherMap.Domain.User.Validations;
using WeatherMap.Shared.Entities;

namespace WeatherMap.Domain.User.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        protected User() { }

        public User(string username, string password)
        {
            AddNotifications(UserValidationContracts.AuthenticationValidationContract(username, password));
            if (Invalid) return;

            Username = username;
            Password = password;
        }

        public string EncodePassword(string password)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
