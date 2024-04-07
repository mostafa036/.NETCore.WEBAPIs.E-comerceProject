using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponseCacheServices
    {
        Task CacheResponseAsync(string CacheKey, object Response, TimeSpan TimeSpan);
        Task<string> GetCacheResponseAsync(string CacheKey);
    }
}
