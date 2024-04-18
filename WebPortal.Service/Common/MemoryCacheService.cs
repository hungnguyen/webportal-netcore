using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Services.Common
{
    public class MemoryCacheService : ICacheService
    {
        private IMemoryCache memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public T Get<T>(object key) 
            => memoryCache.Get<T>(key);

        public void Remove(object key) 
            => memoryCache.Remove(key);

        public T Set<T>(object key, T value, TimeSpan absoluteExpirationRelativeToNow)
            => memoryCache.Set<T>(key, value, absoluteExpirationRelativeToNow);

        public bool TryGet<T>(object key, out T value)
            => memoryCache.TryGetValue(key, out value);
    }
}
