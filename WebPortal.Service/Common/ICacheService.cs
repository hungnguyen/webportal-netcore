using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Services.Common
{
    public interface ICacheService
    {
        T Get<T> (object key);
        T Set<T> (object key, T value, TimeSpan absoluteExpirationRelativeToNow);
        void Remove (object key);
        bool TryGet<T> (object key, out T value);
    }
}
