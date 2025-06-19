using Microsoft.Extensions.Caching.Memory;

namespace RaftAssesment.Models
{
    public class cacheKeys 
    {
        public const string UserCachePaginater = "usersPage";
        public const long UserCachePaginaterSpan = 50;
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public T ResponseObject { get; set; }
        public void SetErrorCode()
        {
            Success = false;
        }
    }

    public class InMemoryCaching
    {
        private readonly IMemoryCache _memoryCache;
        public InMemoryCaching(IMemoryCache memoryCache) { 
            _memoryCache = memoryCache;
        }

        public void setCache(string cacheKey, object Object, long timeSpan) {
            var  cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(timeSpan));
            _memoryCache.Set(cacheKey, Object, cacheEntryOptions);
        }
    }
}
