using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Caching.Attributes
{
    public class CachingAttribute : Attribute, IAsyncActionFilter
    {
        private readonly IMemoryCache _cache;

        public CachingAttribute(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {

            var methodType = context.HttpContext.Request.Method;
            string cacheKey = context.HttpContext.Request.Path;

            if (methodType.Equals("GET"))
            {
                // before the action executes  
                var cacheValue = _cache.Get(cacheKey);
                if (cacheValue != null)
                {
                    context.Result = new ObjectResult(cacheValue);
                    return;
                }

                ActionExecutedContext resultContext = await next();
                //after the action executes  
                if (resultContext.Result != null)
                {
                    var result = (ObjectResult)resultContext.Result;
                    _cache.Set(cacheKey, result.Value, new MemoryCacheEntryOptions
                    {
                        //todo: Add the absolute expire Timespan to appsetting.json
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30),
                        //todo: Add the sliding expire Timespan to appsetting.json
                        SlidingExpiration = TimeSpan.FromDays(7)
                    });
                }
            }         
            else
            {
                await next();
                _cache.Remove(cacheKey);
                var routeValues = context.HttpContext.GetRouteData().Values;

                var id = routeValues["id"]?.ToString();
                if (id != null)
                {
                    cacheKey = cacheKey.Replace($"/{id}", string.Empty);
                    _cache.Remove(cacheKey);
                }
            }

        }


    }
}
