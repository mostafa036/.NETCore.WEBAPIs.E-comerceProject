using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Sevices
{
    public class ResponseCacheService : IResponseCacheServices
    {
        private readonly IDatabase _databaseRedis;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _databaseRedis = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string CacheKey, object Response, TimeSpan TimeSpan)
        {
            if (Response == null) return;

            var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var SerilizedResponse = JsonSerializer.Serialize(Response , option);

            await _databaseRedis.StringSetAsync(CacheKey, SerilizedResponse, TimeSpan);
        }

        public async Task<string> GetCacheResponseAsync(string CacheKey)
        {
            var CacheResponse = await _databaseRedis.StringGetAsync(CacheKey);

            if(CacheResponse.IsNullOrEmpty) return null;

            return CacheResponse;
        }
    }
}
