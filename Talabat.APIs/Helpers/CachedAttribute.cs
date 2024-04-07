using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.APIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int TimeToLiveInSeconds)
        {
            _timeToLiveInSeconds = TimeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheServices = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheServices>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await cacheServices.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = contentResult;

                return;
            }

            var executedEndPointContext =  await next.Invoke();

            if (executedEndPointContext.Result is ObjectResult objectResult)
            {
                await cacheServices.CacheResponseAsync(cacheKey , objectResult.Value , TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBulider = new StringBuilder();

            KeyBulider.Append(request.Path); 

            foreach (var (Key , Value ) in request.Query.OrderBy(x => x.Key) )
            {
               KeyBulider.Append($"|{Key}-{Value}");
            }
            return KeyBulider.ToString();
        }
    }
}