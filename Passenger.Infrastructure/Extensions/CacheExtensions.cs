using System;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt (this IMemoryCache cache, Guid tokenId, JwtDTO jwt)
            => cache.Set(GetJwtKey(tokenId), jwt, TimeSpan.FromSeconds(5));

        public static JwtDTO GetJwt(this IMemoryCache cache, Guid tokenId)
            => cache.Get<JwtDTO>(GetJwtKey(tokenId));

        private static string GetJwtKey(Guid tokenId)
            => $"jwt-{tokenId}";        
    }
}