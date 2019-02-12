using System;

namespace Caching
{
    /// <summary>
    /// Describes cache items.
    /// </summary>
    public class CacheItem
    {
        private readonly object item;

        private readonly DateTime expirationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem"/> class. 
        /// </summary>
        /// <param name="obj">
        /// An object to store.
        /// </param>
        /// <param name="timeInSeconds">
        /// Storage time in seconds.
        /// </param>
        public CacheItem(object obj, int timeInSeconds)
        {
            this.item = obj;
            this.expirationTime = DateTime.UtcNow + new TimeSpan(0, 0, timeInSeconds);
        }

        /// <summary>
        /// Gets an object from the cache item.
        /// </summary>
        /// <returns>Cached object.</returns>
        public object GetItem()
        {
            if (DateTime.UtcNow > this.expirationTime)
            {
                throw new InvalidOperationException("Item expired.");
            }

            return this.item;
        }
    }
}
