namespace Caching
{
    using System;
    using System.Collections.Generic;

    public class Cache
    {
        private Dictionary<string, CacheItem> cache;

        public Cache()
        {
            this.cache = new Dictionary<string, CacheItem>();
        }

        public void AddItem(string key, object obj, int timeInSeconds)
        {
            if (this.cache.ContainsKey(key))
            {
                throw new InvalidOperationException("Object with this name already exists.");
            }

            this.cache.Add(key, new CacheItem(obj, timeInSeconds));
        }

        public object GetItem(string key)
        {
            if (!this.cache.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            return this.cache[key].GetItem();
        }
    }
}
