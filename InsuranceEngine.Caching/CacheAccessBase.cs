using System;

namespace InsuranceEngine.Caching
{
    public class CacheAccessBase<T>
    {

        private static T _instance = default(T);
        /// <summary>
        /// Provides Singleton access to the cache provider
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //create the manager
                    _instance = Activator.CreateInstance<T>();
                }

                return _instance;
            }
        }

        public static DateTime OverNightExpiryTime
        {
            get
            {
                DateTime expiry = DateTime.Now.AddDays(1);
                return new DateTime(expiry.Year, expiry.Month, expiry.Day, 01, 00, 00);
            }
        }

    }
}
