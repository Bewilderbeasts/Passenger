namespace Passenger.Infrastructure.Services
{
    public interface IEncrypter
    {
        string GetSalt (string value);
        string GetPassword (string salt, string password);
        string GetHash (string value, string salt);
         
    }
}