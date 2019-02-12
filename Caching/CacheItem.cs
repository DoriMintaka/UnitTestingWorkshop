using System;

namespace Caching
{
    public class CacheItem
    {
        private readonly object item;

        private readonly DateTime expirationTime;

        public CacheItem(object obj, int timeInSeconds)
        {
            this.item = obj;
            this.expirationTime = DateTime.UtcNow + new TimeSpan(0, 0, timeInSeconds);
        }

        public object GetItem()
        {
            if (DateTime.UtcNow > this.expirationTime)
            {
                throw new InvalidOperationException("Item expired");
            }

            return this.item;
        }
    }
}
