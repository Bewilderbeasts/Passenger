namespace Passenger.Infrastructure.Extensions
{
    public static class StringExtenstions
    {
        public static bool Empty(this string value)
            => string.IsNullOrWhiteSpace(value);
    }
}