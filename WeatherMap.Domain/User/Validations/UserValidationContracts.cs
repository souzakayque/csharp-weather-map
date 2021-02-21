using Flunt.Validations;

namespace WeatherMap.Domain.User.Validations
{
    public static class UserValidationContracts
    {
        public static Contract Login(string username, string password)
        {
            var contract = new Contract()
                .Requires()
                .Join
                (
                    AuthenticationValidationContract(username, password)
                );

            return contract;
        }

        public static Contract AuthenticationValidationContract(string username, string password)
        {
            var contract = new Contract()
                .IsEmail(username, "username", "username must be a valid e-mail.")
                .IsNotNull(password, "password", "password cannot be null.");

            return contract;
        }
    }
}
