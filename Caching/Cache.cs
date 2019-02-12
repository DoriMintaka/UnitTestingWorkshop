namespace Caching
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods to cache objects for limited amount of time.
    /// </summary>
    public class Cache
    {
        private readonly IDictionary<string, CacheItem> cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache"/> class. 
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when dictionary is null.</exception>
        public Cache(IDictionary<string, CacheItem> dictionary)
        {
            this.cache = dictionary ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Adds object to the cache.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <param name="obj">An object to store.</param>
        /// <param name="timeInSeconds">Storage time in seconds.</param>
        /// <exception cref="InvalidOperationException">Thrown when object with such key already exists.</exception>
        public void AddItem(string key, object obj, int timeInSeconds)
        {
            if (this.cache.ContainsKey(key))
            {
                throw new InvalidOperationException("Object with this key already exists.");
            }

            this.cache.Add(key, new CacheItem(obj, timeInSeconds));
        }

        /// <summary>
        /// Retrieves an item from cache.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <exception cref="KeyNotFoundException">Thrown when key doesn't exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the object expired.</exception>
        /// <returns>Object represented by the key.</returns>
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
